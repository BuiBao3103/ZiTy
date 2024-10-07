import { ApartmentStatusSchema } from '@/enums'
import { z } from 'zod'

export const ApartmentSchema = z.object({
	id: z.string(),
	area: z.number().positive(),
	description: z.string(),
	floor_number: z.number().positive(),
	apartment_number: z.number().positive(),
	status: ApartmentStatusSchema,
})
