import BillItem from '@/pages/user/bill/components/bill-item'
import { Bill } from '@/schema/bill.validate'
import { useNavigate, useParams } from 'react-router-dom'

interface BillListProps {
  bills?: Bill[]
}

const BillList = ({ bills }: BillListProps) => {
  return (
    <div className="w-full flex flex-col gap-4 bg-white">
      {bills &&
        bills.map((bill, index) => <BillItem bill={bill} key={index} />)}
    </div>
  )
}

export default BillList
