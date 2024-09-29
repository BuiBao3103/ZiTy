import { SystemStatus } from '@/enums'

export interface Setting {
  id: number
  current_monthly: string
  system_status: SystemStatus
  room_price_per_m2: number
  water_price_per_m3: number
  room_vat: number
  water_vat: number
  env_protection_tax: number
}
