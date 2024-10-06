import { GenderSchema, UserRoleSchema } from '@/enums'
import { z } from 'zod'

// Zod schema for the User interface
export const UserSchema = z.object({
  id: z.number().int().positive(), // Must be a positive integer
  username: z
    .string()
    .min(3, 'Username must be at least 3 characters long')
    .max(20, 'Username must be at most 20 characters long')
    .regex(
      /^[a-zA-Z0-9_]+$/,
      'Username can only contain letters, numbers, and underscores',
    ),
  password: z
    .string()
    .min(6, 'Password must be at least 6 characters')
    .regex(/[A-Z]/, 'Password must contain at least one uppercase letter')
    .regex(/[a-z]/, 'Password must contain at least one lowercase letter')
    .regex(/[0-9]/, 'Password must contain at least one number')
    .regex(/[\W_]/, 'Password must contain at least one special character'),
  avatar: z
    .string()
    .url()
    .refine(
      (url) => /\.(jpg|jpeg|png|gif)$/i.test(url),
      'Avatar must be a valid image URL (.jpg, .jpeg, .png, or .gif)',
    ),
  is_first_login: z.boolean(),
  email: z.string().email('Invalid email address'),
  phone: z
    .string()
    .length(10, 'Phone number must be exactly 10 digits')
    .regex(/^\d{10}$/, 'Phone number must contain only digits'),
  gender: GenderSchema,
  nation_id: z
    .string()
    .min(12, 'Nation ID is required')
    .max(12, 'Nation ID is required'),
  full_name: z
    .string()
    .min(2, 'Full name must be at least 2 characters long')
    .max(50, 'Full name must be at most 50 characters long'),
  user_type: z
    .array(UserRoleSchema)
    .refine((value) => value.some((item) => item), {
      message: 'You have to select at least one item.',
    }),
  date_of_birth: z
    .date()
    .refine(
      (date) => date <= new Date(),
      'Date of birth cannot be in the future',
    )
    .refine((date) => {
      const age = new Date().getFullYear() - date.getFullYear()
      return age <= 100 // Check if age is 100 years or less
    }, 'You must be 100 years old or younger'),
  is_staying: z.boolean(),
})
// Example of schema for Nullable type
export const NullableUserSchema = UserSchema.partial().nullable()

// Zod schema for UserLogin type
export const UserLoginSchema = UserSchema.pick({
  username: true,
  password: true,
})

// Example Zod schema for UserPartial (Partial<User>)
export const UserPartialSchema = UserSchema.partial()


// Example Zod schema for UserApartment (which extends User)
export const UserApartmentSchema = UserSchema.extend({
  // Add any additional fields specific to UserApartment here
})
