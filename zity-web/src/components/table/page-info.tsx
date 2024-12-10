import { cn } from '@/lib/utils'
import { memo } from 'react'

interface PaginationInfoProps {
  currentPage: number
  pageSize: number
  totalItems?: number
  className?: string
  loading?: boolean
}

const PaginationInfo = memo(
  ({
    currentPage,
    pageSize,
    totalItems = 0,
    className = '',
    loading = false,
  }: PaginationInfoProps) => {
    const startItem = (currentPage - 1) * pageSize + 1
    const endItem = Math.min(currentPage * pageSize, totalItems)

    if (loading) {
      return (
        <div className={cn('flex justify-end', className)}>
          <p className="text-sm text-gray-500">Loading...</p>
        </div>
      )
    }

    if (totalItems === 0) {
      return (
        <div className={cn('flex justify-end', className)}>
          <p className="text-sm text-gray-500">No items to display</p>
        </div>
      )
    }

    return (
      <div className={cn('flex justify-end', className)}>
        <p className="text-sm text-gray-600">
          Showing <span className="font-medium">{startItem}</span> to{' '}
          <span className="font-medium">{endItem}</span> of{' '}
          <span className="font-medium">{totalItems}</span> entries
        </p>
      </div>
    )
  },
)

PaginationInfo.displayName = 'PaginationInfo'

export default PaginationInfo
