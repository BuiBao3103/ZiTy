import { Button } from '@/components/ui/button'
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
} from '@/components/ui/form'
import { RadioGroup, RadioGroupItem } from '@/components/ui/radio-group'
import { useCreateUserAnswerMutation } from '@/features/userAnswer/userAnswerSlice'
import { SurveySchema } from '@/schema/survey.validate'
import { useAppSelector } from '@/store'
import { useForm } from 'react-hook-form'
import { z } from 'zod'

interface SurveyFormProps {
  survey?: z.infer<typeof SurveySchema>
}

const SurveyForm = ({ survey }: SurveyFormProps) => {
  const [createUserAnswer, { isLoading }] = useCreateUserAnswerMutation()
  const user = useAppSelector((state) => state.authReducer.user)
  const form = useForm()

  const onSubmit = async (data: any) => {
    //call number of api based on the number of questions
    const promises = survey?.questions.map((question, index) => {
      const answer = data.answers[index]
      return createUserAnswer({
        userId: user?.id,
        answerId: answer,
      }).unwrap()
    })

    try {
      if (promises) {
        await Promise.all(promises)
      }
			console.log('success')
			console.log(promises)
      // Handle success (e.g., show a success message, redirect, etc.)
    } catch (error) {
      // Handle error (e.g., show an error message)
    }
  }

  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className="space-y-4 overflow-y-auto">
        {survey &&
          survey.questions.map((question, index) => (
            <FormField
              control={form.control}
              name={`answers.${index}` as const}
              key={index}
              render={({ field }) => (
                <FormItem className="space-y-4">
                  <FormLabel className="text-base">
                    {index + 1}. {question.content}
                  </FormLabel>
                  <RadioGroup
                    value={field.value}
                    onValueChange={field.onChange}
                    className="space-y-2">
                    {question.answers.map((answer, idx) => (
                      <FormItem
                        key={idx}
                        className="flex items-center space-y-0 space-x-2">
                        <FormControl>
                          <RadioGroupItem value={answer?.id + ''} />
                        </FormControl>
                        <FormLabel className="font-normal">
                          {answer.content}
                        </FormLabel>
                      </FormItem>
                    ))}
                  </RadioGroup>
                </FormItem>
              )}
            />
          ))}
        <Button type="submit" className="w-full">
          Submit
        </Button>
      </form>
    </Form>
  )
}

export default SurveyForm
