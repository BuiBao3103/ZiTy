import { ReportStatusSchema } from '@/enums'
import { z } from 'zod'
import { ExtendedRelationshipsSchema } from './relationship.validate'
import { BaseEntitySchema } from './base.entity'

export const RejectionReasonSchema = z.object({
  id: z.number(),
  content: z.string(),
  reportId: z.number().nullable(),
})
export const ReportSchema = z.object({
  id: z.number(),
  content: z.string(),
  title: z.string(),
  status: ReportStatusSchema,
  relationshipId: z.number().nullable().optional(),
  rejectionReason: RejectionReasonSchema.nullable().optional(),
})
export type ReportFormSchema = z.infer<typeof ReportSchema>

export type ReportType = z.infer<typeof ReportSchema> & {
  relationship?: z.infer<typeof ExtendedRelationshipsSchema>[]
  createdAt?: Date  | string
  updatedAt?: Date | null | string
  deleteAt?: Date | null | string
}

export const ExtendedReportSchema: z.ZodType<ReportType> = ReportSchema.extend({
  relationships: z.lazy(() => ExtendedRelationshipsSchema.array()).optional(),
}).merge(BaseEntitySchema)
