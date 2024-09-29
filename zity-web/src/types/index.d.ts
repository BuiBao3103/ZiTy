import { ApartmentUserRole, Gender, UserRole } from '@/enums'

export interface User {
  id: number,
	username: string,
	password: string,
  avatar: string,
	is_first_login: boolean,
	email: string,
  phone: string,
	gender: Gender,
	national_id: string,
  full_name: string,
  user_type: UserRole,
	date_of_birth: Date,
  is_staying: boolean,
}

export type Nullable<T> = {
  [P in keyof T]: T[P] | null;
};

export type UserLogin = Pick<User, 'username' | 'password'>  

export type UserDisplayTable = Pick<User, 'id' | 'full_name' | 'phone' | 'user_type' | 'is_staying' | 'avatar'>

export type UserPartial = Partial<User>

export interface UserApartment extends User {
	
}