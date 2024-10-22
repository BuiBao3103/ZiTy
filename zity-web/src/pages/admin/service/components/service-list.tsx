import {
  Table,
  TableBody,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table'

import { Service } from '@/schema/service.validate'
import ServiceItem from './service-item'
import TableRowSkeleton from '@/components/skeleton/TableRowSkeleton'

interface ServiceListProps {
  services?: Service[]
  isLoading?: boolean
  isFetching?: boolean
}

const ServiceList = ({ services, isFetching, isLoading }: ServiceListProps) => {
  return (
    <>
      <Table className="mt-4 h-full">
        <TableHeader>
          <TableRow>
            <TableHead>ID</TableHead>
            <TableHead>Name</TableHead>
            <TableHead>Price</TableHead>
            <TableHead>Last Update</TableHead>
            <TableHead>Description</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {isLoading || isFetching
            ? Array.from({ length: 10 }).map((_, index) => (
                <TableRow key={index} className="">
                  <TableRowSkeleton />
                  <TableRowSkeleton />
                  <TableRowSkeleton />
                  <TableRowSkeleton />
                  <TableRowSkeleton />
                </TableRow>
              ))
            : services?.map((service, index) => (
                <ServiceItem service={service} key={index} />
              ))}
        </TableBody>
      </Table>
    </>
  )
}

export default ServiceList
