import { z } from 'zod'

export const PackageSchema = z.object({
  id: z.number().int().positive(),
  image: z.string(),
  description: z.string(),
  is_received: z.boolean().default(false),
  user_id: z.number().nullable(),
})
export interface IPackage extends z.infer<typeof PackageSchema>, BaseEntity {}
