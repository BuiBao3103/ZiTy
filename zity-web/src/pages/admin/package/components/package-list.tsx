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
import AlertDelete from '@/components/alert/AlertDelete'
import { Button } from '@/components/ui/button'
import TableRowSkeleton from '@/components/skeleton/TableRowSkeleton'
import PackageDetail from './package-detail'
import { formatDateWithSlash } from '@/utils/Generate'

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
            <TableHead>Phone</TableHead>
            <TableHead>Delivery Date</TableHead>
            <TableHead>Status</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {(isLoading || isFetching) &&
            Array.from({ length: 10 }).map((_, index) => (
              <TableRow key={index} className="">
                <TableRowSkeleton />
                <TableRowSkeleton />
                <TableRowSkeleton />
                <TableRowSkeleton />
                <TableRowSkeleton />
                <TableRowSkeleton />
              </TableRow>
            ))}
          {packages &&
            packages.map((packagee, index) => (
              <TableRow
                key={index}
                className="font-medium cursor-pointer">
                <TableCell className="w-[5%] py-3">{packagee.id}</TableCell>
                <TableCell className="w-[25%]">{packagee.user?.fullName}</TableCell>
                <TableCell className='w-[15%]'>{packagee.user?.phone}</TableCell>
                <TableCell className='w-[15%]'>{formatDateWithSlash(new Date(packagee.createdAt))}</TableCell>
                <TableCell className='w-[25%] uppercase'>
                  <Badge
                    variant={`${packagee.isReceive ? 'success' : 'error'}`}>
                    {packagee.isReceive ? 'Collected' : 'Not Collected'}
                  </Badge>
                </TableCell>
                <TableCell>
                  <PackageDetail mode="edit" id={packagee.id}>
                    <Button size={'icon'} variant={'ghost'}>
                      <Eye />
                    </Button>
                  </PackageDetail>
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
