import { Badge } from '@/components/ui/badge'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table'
import { Bill } from '@/schema/bill.validate'

interface BillListProps {
  bills: Bill[]
}

const BillList = ({ bills }: BillListProps) => {
  return (
    <>
      <Table className="mt-4 h-full">
        <TableHeader>
          <TableRow>
            <TableHead>ID</TableHead>
            <TableHead>Fullname - Apartment</TableHead>
            <TableHead>Total</TableHead>
            <TableHead>Date</TableHead>
            <TableHead>Status</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {bills.map((bill) => (
            <TableRow key={bill.id} className="font-medium cursor-pointer">
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
          ))}
        </TableBody>
      </Table>
    </>
  )
}

export default BillList
