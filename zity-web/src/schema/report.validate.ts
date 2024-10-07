import { ReportStatusSchema } from '@/enums'
import { z } from 'zod'
import { UserSchema } from './user.validate'

export const ReportSchema = z.object({
  id: z.number(),
  content: z.string(),
  title: z.string(),
  status: ReportStatusSchema,
  user: UserSchema.pick({ full_name: true, avatar: true }).partial(),
  created_at: z.date(),
  relationship_id: z.number().nullable().optional(),
})
export type Report = z.infer<typeof ReportSchema>

export const RejectionReasonsSchema = z.object({
  id: z.number(),
  content: z.string(),
  report_id: z.number().nullable(),
})

export type RejectionReasons = z.infer<typeof RejectionReasonsSchema>