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
import BillItem from './bill-item'
import BillForm from './bill-form'
import { useState } from 'react'

interface BillListProps {
  bills: Bill[]
}

const BillList = ({ bills }: BillListProps) => {
  const [showDetail, setShowDetail] = useState<number | null>(null)

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
          {bills.map((bill, index) => (
            <BillItem bill={bill} key={index} setShowDetail={setShowDetail} />
          ))}
        </TableBody>
      </Table>
      {showDetail && <BillForm setShowDetail={setShowDetail} />}
    </>
  )
}

export default BillList
