import { z } from 'zod'
export const ServiceSchema = z.object({
  id: z.number(),
  name: z
    .string()
    .min(10, 'Name is required')
    .max(100, 'Name must be less than 100 characters long'),
  description: z
    .string()
    .min(10, 'Description must be at least 10 characters long')
    .max(500, 'Description must be less than 500 characters long'),
  price: z
    .number()
    .positive('Price must be a positive number') // Must be positive
    .min(1, 'Price must be at least 1'),
  created_at: z.date(),
})

export const ServiceFormSchema = ServiceSchema.omit({
  id: true,
  created_at: true,
})

export type Service = z.infer<typeof ServiceSchema>