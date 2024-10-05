import { Badge } from '@/components/ui/badge'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table'
import {
  Tooltip,
  TooltipContent,
  TooltipTrigger,
} from '@/components/ui/tooltip'
import { Package } from '@/schema/package.validate'
import { Eye, Trash2 } from 'lucide-react'
import PackageForm from './package-form'
import AlertDelete from '@/components/alert/AlertDelete'
import { Button } from '@/components/ui/button'

interface PackageListProps {
  packages: Package[]
}

const PackageList = ({ packages }: PackageListProps) => {
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
          {packages.map((packagee) => (
            <TableRow key={packagee.id} className="font-medium cursor-pointer">
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
                  variant={`${packagee.is_received ? 'success' : 'error'}`}>
                  {packagee.is_received ? 'Collected' : 'Not Collected'}
                </Badge>
              </TableCell>
              <TableCell>
                <Tooltip>
                  <TooltipTrigger>
                    <PackageForm id="123">
                      <Button size={"icon"} variant={"ghost"}>
                        <Eye />
                      </Button>
                    </PackageForm>
                  </TooltipTrigger>
                  <TooltipContent>Detail</TooltipContent>
                </Tooltip>
              </TableCell>
              <TableCell>
                <Tooltip>
                  <TooltipTrigger>
                    <AlertDelete
                      description="package"
                      setAction={() => {}}
                      type="icon"
                      variants="ghost"
                    />
                  </TooltipTrigger>
                  <TooltipContent>Delete</TooltipContent>
                </Tooltip>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </>
  )
}

export default PackageList
