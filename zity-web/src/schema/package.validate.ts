import { z } from 'zod'

export const PackageSchema = z.object({
	id: z.number().int().positive(),
	image: z.string(),
	description: z.string(),
	is_received: z.boolean().default(false),
	user_id: z.number().nullable(),
})
export type Package = z.infer<typeof PackageSchema>