//cannot export enum from .d.ts file

export enum UserRole {
  ADMIN = 'ADMIN',
  RESIDENT = 'RESIDENT',
}

export enum Gender {
  MALE = 'MALE',
  FEMALE = 'FEMALE',
}

export enum ApartmentUserRole {
  OWNER = 'OWNER',
  USER = 'USER',
}

export enum ReportStatus {
  PENDING = 'PENDING',
  IN_PROGRESS = 'IN_PROGRESS',
  RESOLVED = 'RESOLVED',
  REJECTED = 'REJECTED',
}

export enum ApartmentStatus {
  IN_USE = 'IN_USE',
  EMPTY = 'EMPTY',
  DISRUPTION = 'DISRUPTION',
}

export enum BillStatus {
  UNPAID = 'UNPAID',
  PAID = 'PAID',
  OVERDUE = 'OVERDUE',
}

export enum SystemStatus {
  PREPAYMENT = 'PREPAYMENT',
  PAYMENT = 'PAYMENT',
  OVERDUE = 'OVERDUE',
  DELINQUENT = 'DELINQUENT',
}
