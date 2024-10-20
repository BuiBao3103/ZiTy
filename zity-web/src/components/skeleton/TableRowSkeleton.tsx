import { TableCell } from '../ui/table'
import { Skeleton } from '../ui/skeleton'

const TableRowSkeleton = () => {
  return (
    <TableCell>
      <Skeleton className="h-9 w-full" />
    </TableCell>
  )
}

export default TableRowSkeleton
