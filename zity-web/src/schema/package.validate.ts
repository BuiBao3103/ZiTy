import { z } from 'zod'

export const PackageSchema = z.object({
  id: z.number().int().positive(),
  image: z.string(),
  description: z.string(),
  isReceived: z.boolean().default(false),
  userId: z.number().nullable(),
})
export interface IPackage extends z.infer<typeof PackageSchema>, BaseEntity {}
