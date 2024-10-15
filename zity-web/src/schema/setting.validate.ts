import { SystemStatusSchema } from '@/enums'
import {z} from 'zod'

export const SettingSchema = z.object({
	id: z.number(),
	current_monthly: z.string(),
	system_status: SystemStatusSchema,
	room_price_per_m2: z.number(),
	water_price_per_m3: z.number(),
	water_vat: z.number(),
	env_protection_tax: z.number()
})

export type Setting = z.infer<typeof SettingSchema>