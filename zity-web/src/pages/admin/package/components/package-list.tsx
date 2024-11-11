import { Badge } from '@/components/ui/badge'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table'
import { IPackage } from '@/schema/package.validate'
import { Eye } from 'lucide-react'
import PackageForm from './package-form'
import AlertDelete from '@/components/alert/AlertDelete'
import { Button } from '@/components/ui/button'
import TableRowSkeleton from '@/components/skeleton/TableRowSkeleton'

interface PackageListProps {
  packages?: IPackage[]
  isLoading?: boolean
  isFetching?: boolean
}

const PackageList = ({ packages, isLoading, isFetching }: PackageListProps) => {
  return (
    <>
      <Table className="mt-4 h-full">
        <TableHeader>
          <TableRow>
            <TableHead>ID</TableHead>
            <TableHead>Shipping to</TableHead>
            <TableHead>From</TableHead>
            <TableHead>Phone</TableHead>
            <TableHead>Delivery Date</TableHead>
            <TableHead>Status</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {(isLoading || isFetching) &&
            Array.from({ length: 10 }).map((_, index) => (
              <TableRow key={index} className="">
                  <TableRowSkeleton/>
                  <TableRowSkeleton/>
                  <TableRowSkeleton/>
                  <TableRowSkeleton/>
                  <TableRowSkeleton/>
                  <TableRowSkeleton/>
              </TableRow>
            ))}
          {packages &&
            packages.map((packagee) => (
              <TableRow
                key={packagee.id}
                className="font-medium cursor-pointer">
                <TableCell className="py-3">{packagee.id}</TableCell>
                <TableCell className="">
                  123
                  {/* <p className="">{packagee.}</p> */}
                </TableCell>
                <TableCell>123</TableCell>
                <TableCell>123</TableCell>
                <TableCell>123</TableCell>
                <TableCell className={`uppercase`}>
                  <Badge
                    variant={`${packagee.isReceived ? 'success' : 'error'}`}>
                    {packagee.isReceived ? 'Collected' : 'Not Collected'}
                  </Badge>
                </TableCell>
                <TableCell>
                  <PackageForm id="123">
                    <Button size={'icon'} variant={'ghost'}>
                      <Eye />
                    </Button>
                  </PackageForm>
                </TableCell>
                <TableCell>
                  <AlertDelete
                    description="package"
                    setAction={async () => {}}
                    type="icon"
                    variants="ghost"
                  />
                </TableCell>
              </TableRow>
            ))}
        </TableBody>
      </Table>
    </>
  )
}

export default PackageList
