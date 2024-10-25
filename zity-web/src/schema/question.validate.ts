import { z } from 'zod'

export const AnswerItemSchema = z.object({
  answer: z
    .string({ required_error: 'This field is required' })
    .min(1, 'Answer is required'),
})

export const QuestionSchema = z.object({
  id: z.number(),
  content: z.string(),
  surveyId: z.number(),
  answers: z.array(AnswerItemSchema),
  otherAnswers: z.array(z.any()),
  survey: z.any().nullable(), // Adjust the structure of 'survey' if needed
})

// Example usage:
type QuestionFormSchema = z.infer<typeof QuestionSchema>

export const QuestionItem = QuestionSchema.pick({
	content: true,
	answers: true,
}) 

export interface IQuestion extends QuestionFormSchema, BaseEntity {}

export const QuestionFormSchema = z
  .object({
    title: z
      .string({ required_error: 'This field is required' })
      .min(1, 'Title is required'),
    description: z.string(),
    questions: z.array(z.any()),
    startDate: z.date({ required_error: 'This field is required' }),
    endDate: z.date({ required_error: 'This field is required' }),
  })
  // Custom validation between startDate and endDate
  .refine((data) => data.startDate < data.endDate, {
    message: 'Start date must be before end date',
    path: ['startDate'], // Attach the error to the startDate or endDate
  })
  .refine((data) => data.startDate > new Date(), {
    message: 'Start date must be in the future',
    path: ['startDate'], // Attach the error to startDate
  })
  .refine((data) => data.endDate > new Date(), {
    message: 'End date must be in the future',
    path: ['endDate'], // Attach the error to endDate
  })
