import { Button } from '@/components/ui/button'
import { Card, CardContent, CardHeader } from '@/components/ui/card'
import { Input } from '@/components/ui/input'
import { Separator } from '@/components/ui/separator'
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
} from '@/components/ui/form'
import { useFieldArray, useForm } from 'react-hook-form'
import { z } from 'zod'
import { QuestionFormSchema } from '@schema/question.validate'
import { zodResolver } from '@hookform/resolvers/zod'
import { toast } from 'sonner'
import { DatePickerWithRange } from '@/components/widgets/date-picker-with-range'
import QuestionItem from './question-item'
const CreateSurveyForm = () => {
  const form = useForm<z.infer<typeof QuestionFormSchema>>({
    defaultValues: {
      title: '',
      description: '',
      questions: [
        {
          question: '',
          description: '',
          answers: [
            { answer: '' },
            { answer: '' },
            { answer: '' },
            { answer: '' },
          ],
        },
      ],
    },
    resolver: zodResolver(QuestionFormSchema),
  })

  const { fields, append, remove } = useFieldArray({
    name: 'questions',
    control: form.control,
  })

  const appendQuestion = () => {
    append({
      question: '',
      description: '',
      answers: [{ answer: '' }, { answer: '' }, { answer: '' }, { answer: '' }],
    })
  }

  const onSubmit = (data: z.infer<typeof QuestionFormSchema>) => {
    console.log(data)
    toast.success('Survey created successfully')
    form.reset()
  }

  const onError = (error: any) => {
    if (error['title']) {
      toast.error(error['title']?.message)
      return
    }
    if (error['description']) {
      toast.error(error['title']?.message)
      return
    }
  }

  return (
    <Card className="h-fit">
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit, onError)}>
          <CardHeader className="space-y-2">
            <div className="w-full flex justify-between items-center">
              <div className="w-full flex flex-col space-y-2">
                <FormField
                  control={form.control}
                  name="title"
                  render={({ field }) => (
                    <FormItem className="space-y-0">
                      <FormLabel className="text-base">Title</FormLabel>
                      <FormControl>
                        <Input
                          placeholder="Type here"
                          autoFocus
                          {...field}
                          className="border-none shadow-none focus-visible:ring-0 p-0 text-xl font-bold"
                        />
                      </FormControl>
                    </FormItem>
                  )}
                />
                <FormField
                  control={form.control}
                  name="description"
                  render={({ field }) => (
                    <FormItem className="space-y-0">
                      <FormLabel className="text-base">Description</FormLabel>
                      <FormControl>
                        <Input
                          {...field}
                          className="border-none shadow-none focus-visible:ring-0 p-0 text-base text-muted-foreground"
                          placeholder="+ Add Description"
                        />
                      </FormControl>
                    </FormItem>
                  )}
                />
              </div>
              <div className="w-full flex flex-col items-end">
                <DatePickerWithRange />
              </div>
            </div>
          </CardHeader>
          <Separator />
          <CardContent className="pt-6 flex flex-col space-y-4">
            <div className={`w-full grid grid-cols-2 gap-4`}>
              {fields.map((field, index) => (
                <QuestionItem
                  key={field.id}
                  control={form.control}
                  questionIndex={index}
                  removeQuestion={remove}
                />
              ))}
            </div>
            <div className="w-full flex justify-between items-center">
              <Button
                type="button"
                variant={'default'}
                className="w-fit"
                onClick={() => appendQuestion()}>
                + Add new question
              </Button>
              <Button type="submit" variant={'destructive'} className="w-fit">
                Save survey
              </Button>
            </div>
          </CardContent>
        </form>
      </Form>
    </Card>
  )
}

export default CreateSurveyForm
