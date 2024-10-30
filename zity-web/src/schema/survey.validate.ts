import { z } from 'zod'
import { QuestionSchema } from './question.validate'

export const SurveySchema = z
  .object({
    id: z.number(),
    title: z.string(),
    startDate: z.coerce.date(),
    endDate: z.coerce.date(),
    totalQuestions: z.number().optional(),
    userCreateId: z.number().optional(),
    questions: z.array(QuestionSchema), // Assuming questions can be any structure, adjust if necessary
    userCreate: z.any().nullable(), // Adjust the type based on the actual structure
  })
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
export type ServiceFormSchema = z.infer<typeof SurveySchema>

export interface ISurvey extends ServiceFormSchema, BaseEntity {}
