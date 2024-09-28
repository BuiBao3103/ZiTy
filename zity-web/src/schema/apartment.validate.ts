import { z } from 'zod'

export const ApartmentSchema = z.object({
	owner: z.string().min(3, 'Owner name must be at least 3 characters'),
	
})