interface ResponseDataType<T> {
  contents: T[]
  page: number
  pageSize: number
  totalItems: number
  totalPages: number
}

interface BaseEntity {
  createdAt: Date | string
  updatedAt: Date | string
  deletedAt?: Date | string
}