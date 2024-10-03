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
import QuestionItem from './question-item'
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from '@/components/ui/popover'
import { cn } from '@/lib/utils'
import { format } from 'date-fns'
import { CalendarIcon, ChevronLeft } from 'lucide-react'
import { Calendar } from '@/components/ui/calendar'
import { useAppDispath } from '@/store'
import { createNewSurvey } from '@/features/survey/surveySlice'
const CreateSurveyForm = () => {
  const dispatch = useAppDispath()
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
      startDate: new Date(),
      endDate: new Date(),
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
    if (error['startDate']) {
      toast.error(error['startDate']?.message)
      return
    }
    if (error['endDate']) {
      toast.error(error['endDate']?.message)
      return
    }
  }

  return (
    <>
      <Button
        variant={'ghost'}
        type="button"
        onClick={() => dispatch(createNewSurvey({ isCreateNewSurvey: false }))}
        className="w-fit px-0 ">
        <ChevronLeft /> Back to survey
      </Button>
      <Card className="h-full">
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
                <div className="w-full flex gap-4 justify-end">
                  <FormField
                    control={form.control}
                    name="startDate"
                    render={({ field }) => (
                      <FormItem className="flex flex-col">
                        <FormLabel>Start Date</FormLabel>
                        <Popover>
                          <PopoverTrigger asChild>
                            <FormControl>
                              <Button
                                variant={'outline'}
                                className={cn(
                                  'w-[240px] pl-3 text-left font-normal',
                                  !field.value && 'text-muted-foreground',
                                )}>
                                {field.value ? (
                                  format(field.value, 'PPP')
                                ) : (
                                  <span>Pick a date</span>
                                )}
                                <CalendarIcon className="ml-auto h-4 w-4 opacity-50" />
                              </Button>
                            </FormControl>
                          </PopoverTrigger>
                          <PopoverContent className="w-auto p-0" align="start">
                            <Calendar
                              mode="single"
                              selected={field.value}
                              onSelect={field.onChange}
                              disabled={(date) => date < new Date('1900-01-01')}
                              initialFocus
                            />
                          </PopoverContent>
                        </Popover>
                      </FormItem>
                    )}
                  />
                  <FormField
                    control={form.control}
                    name="endDate"
                    render={({ field }) => (
                      <FormItem className="flex flex-col">
                        <FormLabel>Due Date</FormLabel>
                        <Popover>
                          <PopoverTrigger asChild>
                            <FormControl>
                              <Button
                                variant={'outline'}
                                className={cn(
                                  'w-[240px] pl-3 text-left font-normal',
                                  !field.value && 'text-muted-foreground',
                                )}>
                                {field.value ? (
                                  format(field.value, 'PPP')
                                ) : (
                                  <span>Pick a date</span>
                                )}
                                <CalendarIcon className="ml-auto h-4 w-4 opacity-50" />
                              </Button>
                            </FormControl>
                          </PopoverTrigger>
                          <PopoverContent className="w-auto p-0" align="start">
                            <Calendar
                              mode="single"
                              selected={field.value}
                              onSelect={field.onChange}
                              disabled={(date) => date < new Date('1900-01-01')}
                              initialFocus
                            />
                          </PopoverContent>
                        </Popover>
                      </FormItem>
                    )}
                  />
                </div>
              </div>
            </CardHeader>
            <Separator />
            <CardContent className="pt-6 flex flex-col space-y-4">
              <div className={`w-full grid grid-cols-1 lg:grid-cols-2 gap-4`}>
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
    </>
  )
}

export default CreateSurveyForm
