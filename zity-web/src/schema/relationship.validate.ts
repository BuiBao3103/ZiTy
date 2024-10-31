import { ApartmentUserRoleSchema } from '@/enums'
import { z } from 'zod'
import { BaseEntitySchema } from './question.validate'

export const relationshipsSchema = z
  .object({
    id: z.number(),
    role: ApartmentUserRoleSchema, // adjust enums based on all possible roles
    userId: z.number(),
    apartmentId: z.string(),
    user: z.any().nullable(), // adjust type if `user` structure is known
    apartment: z.any().nullable(), // adjust type if `apartment` structure is known
    bills: z.array(z.any()), // adjust type if `bills` structure is known
    reports: z.array(z.any()), // adjust type if `reports` structure is known
  })
export type RelationshipsTypeSchema = z.infer<typeof relationshipsSchema>

export interface IRelationships extends RelationshipsTypeSchema, BaseEntity {}
