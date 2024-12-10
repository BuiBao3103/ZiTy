import { ApartmentUserRoleSchema } from '@/enums'
import { z } from 'zod'
import { ExtendedApartmentSchema } from './apartment.validate'
import { BillSchema } from './bill.validate'
import { ReportSchema } from './report.validate'
import { UserSchema } from './user.validate'
import { BaseEntitySchema } from './base.entity'
export const RelationshipsSchema = z.object({
  id: z.number(),
  role: ApartmentUserRoleSchema, // adjust enums based on all possible roles
  userId: z.number(),
  apartmentId: z.string(),
})

type RelationshipsType = z.infer<typeof RelationshipsSchema> & {
  user?: z.infer<typeof UserSchema> | null
  apartment?: z.infer<typeof ExtendedApartmentSchema>[]
  bills?: z.infer<typeof BillSchema>[]
  reports?: z.infer<typeof ReportSchema>[]
  createdAt?: Date | null | string
  updatedAt?: Date | null | string
  deleteAt?: Date | null | string
}

export const ExtendedRelationshipsSchema: z.ZodType<RelationshipsType> = RelationshipsSchema.extend(
  {
    user: UserSchema.nullable(), // adjust type if `user` structure is known
    apartment: z.lazy(() => ExtendedApartmentSchema.array()).optional(), // adjust type if `apartment` structure is known
    bills: z.array(BillSchema).optional(), // adjust type if `bills` structure is known
    reports: z.array(ReportSchema).optional(), // adjust type if `reports` structure is known
  },
).merge(BaseEntitySchema)

export type RelationshipsTypeSchema = z.infer<typeof ExtendedRelationshipsSchema>
