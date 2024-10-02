import { Badge } from '@/components/ui/badge'
import { TableCell, TableRow } from '@/components/ui/table'
import { Bill } from '@/schema/bill.validate'
import { memo } from 'react'

interface BillItemProps {
  bill: Bill
  setShowDetail: (value: number | null) => void
}

const BillItem = ({ bill, setShowDetail }: BillItemProps) => {
  return (
    <TableRow
      onClick={() => setShowDetail(bill.id)}
      className="font-medium cursor-pointer">
      <TableCell className="py-3">{bill.id}</TableCell>
      <TableCell className="">
        <p className="">123</p>
      </TableCell>
      <TableCell>{bill.total_price}</TableCell>
      <TableCell>{new Date().toLocaleDateString()}</TableCell>
      <TableCell className="uppercase">
        <Badge
          variant={`${
            bill.status == 'UNPAID'
              ? 'warning'
              : bill.status == 'OVERDUE'
              ? 'error'
              : 'info'
          }`}>
          {bill.status}
        </Badge>
      </TableCell>
    </TableRow>
  )
}

export default memo(BillItem)
