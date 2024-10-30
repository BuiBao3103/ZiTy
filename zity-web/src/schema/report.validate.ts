import { ReportStatusSchema } from '@/enums'
import { z } from 'zod'

export const ReportSchema = z.object({
  id: z.number(),
  content: z.string(),
  title: z.string(),
  status: ReportStatusSchema,
  relationshipId: z.number().nullable().optional(),
})
export type ReportFormSchema = z.infer<typeof ReportSchema>

export const RejectionReasonsSchema = z.object({
  id: z.number(),
  content: z.string(),
  reportId: z.number().nullable(),
})

export interface IRejectionReasons extends z.infer<typeof RejectionReasonsSchema> {}

export interface IReport extends BaseEntity, ReportFormSchema {
  rejectionReasons: IRejectionReasons[]
}
