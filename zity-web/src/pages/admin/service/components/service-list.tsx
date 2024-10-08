import AlertDelete from '@/components/alert/AlertDelete'
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
import { Service } from '@/schema/service.validate'

interface ServiceListProps {
  services: Service[]
}

const ServiceList = ({ services }: ServiceListProps) => {
  return (
    <>
      <Table className="mt-4 h-full">
        <TableHeader>
          <TableRow>
            <TableHead>ID</TableHead>
            <TableHead>Name</TableHead>
            <TableHead>Price</TableHead>
            <TableHead>Created_At</TableHead>
            <TableHead>Description</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {services.map((service) => (
            <TableRow key={service.id} className="font-medium cursor-pointer">
              <TableCell className="py-3">{service.id}</TableCell>
              <TableCell className="">
                <p className="">{service.name}</p>
              </TableCell>
              <TableCell>{service.price}</TableCell>
              <TableCell>{service.created_at.toISOString()}</TableCell>
              <TableCell className="uppercase">{service.description}</TableCell>
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

export default ServiceList
