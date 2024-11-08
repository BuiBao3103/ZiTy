import { ApartmentStatusSchema } from '@/enums'
import { z } from 'zod'

export const ApartmentSchema = z.object({
	id: z.string(),
	area: z.number().positive(),
	description: z.string(),
	floorNumber: z.number().positive(),
	apartmentNumber: z.number().positive(),
	status: ApartmentStatusSchema,
})
export type ApartmentFromSchema = z.infer<typeof ApartmentSchema>

export interface IApartment extends BaseEntity, ApartmentFromSchema {}