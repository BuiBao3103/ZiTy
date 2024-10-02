import { BillStatusSchema } from '@/enums'
import { z } from 'zod'

export const BillSchema = z.object({
  id: z.number(),
  monthly: z.number(),
  total_price: z.number(),
  old_water: z.number().nullable(),
  new_water: z.number().nullable(),
  status: BillStatusSchema,
  relationship_id: z.number(),
})
export type Bill = z.infer<typeof BillSchema>