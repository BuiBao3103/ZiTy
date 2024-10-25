import { z } from 'zod'
import { QuestionItem, QuestionSchema } from './question.validate'

const BaseEntitySchema = z.object({
  createdAt: z.union([z.date(), z.string()]),
  updatedAt: z.union([z.date(), z.string()]),
  deletedAt: z.union([z.date(), z.string()]).optional(),
})

export const SurveySchema = z
  .object({
    id: z.number(),
    title: z.string(),
    startDate: z.date(),
    endDate: z.date(),
    totalQuestions: z.number(),
    userCreatedId: z.number().nullable(),
    questions: z.array(QuestionItem), // Assuming questions can be any structure, adjust if necessary
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
