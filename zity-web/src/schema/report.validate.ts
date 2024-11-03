import { ReportStatusSchema } from '@/enums'
import { z } from 'zod'
import { IRelationships } from './relationship.validate'

export const ReportSchema = z.object({
  id: z.number(),
  content: z.string(),
  title: z.string(),
  status: ReportStatusSchema,
  relationshipId: z.number().nullable().optional(),
})
export type ReportFormSchema = z.infer<typeof ReportSchema>

export const RejectionReasonSchema = z.object({
  id: z.number(),
  content: z.string(),
  reportId: z.number().nullable(),
})

export interface IRejectionReason extends z.infer<typeof RejectionReasonSchema> {}

export interface IReport extends BaseEntity, ReportFormSchema {
  rejectionReason: IRejectionReason,
	relationship?: IRelationships
}
