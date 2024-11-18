import { z } from 'zod'

export const BaseEntitySchema = z.object({
  createdAt: z.union([z.date(), z.string()]).optional(),
  updatedAt: z.union([z.date(), z.string()]).optional(),
  deletedAt: z.union([z.date(), z.string()]).optional(),
})

export const AnswerItemSchema = z.object({
  id: z.number().optional(),
  content: z.string().optional(),
  question: z.string().nullable().optional(),
  questionId: z.number().optional(),
})

export type AnswerItem = z.infer<typeof AnswerItemSchema>

export const OtherAnswersSchema = z
  .object({
    id: z.number().optional(),
    content: z
      .string({ required_error: 'Password is required' })
      .trim()
      .min(10, 'Content must not be empty'),
    questionId: z.number(),
    userId: z.number(),
  })
  .merge(BaseEntitySchema)

export const QuestionSchema = z.object({
  id: z.number().optional(),
  content: z.string(),
  surveyId: z.number().optional(),
  answers: z.array(AnswerItemSchema),
  otherAnswers: z.array(OtherAnswersSchema).optional(),
  survey: z.any().nullable(), // Adjust the structure of 'survey' if needed
})

// Example usage:
type QuestionFormSchema = z.infer<typeof QuestionSchema>

export const QuestionItem = QuestionSchema.pick({
  content: true,
  answers: true,
})

export interface IQuestion extends QuestionFormSchema, BaseEntity {}
