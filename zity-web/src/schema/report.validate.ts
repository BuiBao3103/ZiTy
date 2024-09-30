import { ReportStatusSchema } from '@/enums'
import { z } from 'zod'

export const ReportSchema = z.object({
  id: z.number(),
  content: z.string(),
  title: z.string(),
  status: ReportStatusSchema,
  relationship_id: z.number().nullable(),
})

export const RejectionReasonsSchema = z.object({
  id: z.number(),
  content: z.string(),
  report_id: z.number().nullable(),
})
