import { z } from 'zod'

export const AnswerItemSchema = z.object({
  answer: z
    .string({ required_error: 'This field is required' })
    .min(1, 'Answer is required'),
})

export const QuestionItemSchema = z.object({
  question: z
    .string({ required_error: 'This field is required' })
    .min(1, 'Question is required'),
  description: z
    .string({ required_error: 'This field is required' })
    .min(1, 'Description is required'),
  answers: z.array(AnswerItemSchema),
})

export const QuestionFormSchema = z
  .object({
    title: z
      .string({ required_error: 'This field is required' })
      .min(1, 'Title is required'),
    description: z.string(),
    questions: z.array(QuestionItemSchema),
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
