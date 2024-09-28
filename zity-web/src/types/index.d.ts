export type RoleEnum = 'OWNER' | 'RESIDENT' | 'ADMIN'

export interface User {
  id: number
  avatar: string
  name: string
  room: string
  phoneNumber: string
  type: Exclude<RoleEnum, 'ADMIN'>
  status: string
}
