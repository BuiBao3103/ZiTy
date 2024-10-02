import { Button } from '@/components/ui/button'
import { Label } from '@/components/ui/label'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'
import { Separator } from '@/components/ui/separator'
import {
  Table,
  TableBody,
  TableCell,
  TableFooter,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table'
import BillAlertDelete from './bill-alert-delete'

interface BillFormProps {
  setShowDetail: (value: number | null) => void
}

const BillForm = ({ setShowDetail }: BillFormProps) => {
  return (
    <div className="fixed w-full h-screen flex justify-center items-center inset-0 z-50">
      <div
        className="fixed inset-0 bg-black/20 animate-in fade-in"
        onClick={() => setShowDetail(null)}></div>
      <div className="max-w-sm min-[550px]:max-w-lg w-full h-fit bg-white rounded-md relative z-[51] animate-in fade-in-95 zoom-in-95 shadow-lg">
        <div className="w-full flex justify-between items-center px-4 py-3">
          <div className="flex flex-col">
            <p className="text-3xl font-medium">
              Bill <span className="text-zinc-400">#</span>
              <span className="text-primary">123</span>
            </p>
            <span className="uppercase text-sm font-medium text-zinc-400">
              water bill & electricity bill
            </span>
          </div>
        </div>
        <Separator />
        <div className="p-4 w-full flex flex-col space-y-4">
          <div className="">
            <Label className="text-zinc-400">Issued on:</Label>
            <p className="font-medium">
              {new Date().toLocaleDateString('en-US', {
                year: 'numeric',
                month: 'short',
                day: '2-digit',
              })}
            </p>
          </div>
          <div className="w-full flex justify-between items-center">
            <div className="w-full ">
              <Label className="text-zinc-400">From:</Label>
              <div className="flex flex-col">
                <span className="font-medium">Jack Phat</span>
                <p className="text-sm text-zinc-400">Zity Apartment Manager</p>
              </div>
            </div>
            <div className="w-full ">
              <Label className="text-zinc-400">To:</Label>
              <div className="flex flex-col">
                <span className="font-medium">Bui Hong Bao</span>
                <p className="text-sm text-zinc-400">Room A.101</p>
              </div>
            </div>
          </div>
          <div className="w-full flex flex-col gap-2">
            <Label className="">Invoice Items:</Label>
            <Table className="font-medium rounded-md">
              <TableHeader className="bg-zinc-100 border">
                <TableRow>
                  <TableHead className="w-3/4">Description</TableHead>
                  <TableHead className="text-right">Price</TableHead>
                </TableRow>
              </TableHeader>
              <TableBody className="border">
                <TableRow>
                  <TableCell>
                    <p>Water</p>
                  </TableCell>
                  <TableCell className="text-right">
                    <p>$1000</p>
                  </TableCell>
                </TableRow>
                <TableRow>
                  <TableCell>
                    <p>Electricity</p>
                  </TableCell>
                  <TableCell className="text-right">
                    <p>$1000</p>
                  </TableCell>
                </TableRow>
              </TableBody>
              <TableFooter className="border">
                <TableRow>
                  <TableCell colSpan={1}>Total</TableCell>
                  <TableCell className="text-right">$2,500.00</TableCell>
                </TableRow>
              </TableFooter>
            </Table>
          </div>
          <div className="w-full ">
            <Label className="text-zinc-400">Status:</Label>
            <Select defaultValue="UNPAID">
              <SelectTrigger className="w-[180px]">
                <SelectValue placeholder="Theme" />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value="UNPAID">UNPAID</SelectItem>
                <SelectItem value="PAID">PAID</SelectItem>
                <SelectItem value="OVERDUE">OVERDUE</SelectItem>
              </SelectContent>
            </Select>
          </div>
        </div>
        <Separator />
        <div className="w-full flex justify-between items-center p-4">
          <BillAlertDelete />
          <div className="w-full flex justify-end gap-2">
            <Button
              onClick={() => setShowDetail(null)}
              type="button"
              variant={'ghost'}>
              Cancel
            </Button>
            <Button type="submit" variant={'default'}>
              Save
            </Button>
          </div>
        </div>
      </div>
    </div>
  )
}

export default BillForm
