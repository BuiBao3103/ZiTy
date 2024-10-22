import DefaultAvatar from '@/assets/default-avatar.jpeg'
import { Avatar, AvatarImage } from '@/components/ui/avatar'
import { Badge } from '@/components/ui/badge'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table'
import { Report } from '@/schema/report.validate'
import { AvatarFallback } from '@radix-ui/react-avatar'
import { useState } from 'react'
import ReportDetail from './report-detail'
import { Skeleton } from '@/components/ui/skeleton'

interface ReportListProps {
  reports?: Report[]
  isLoading?: boolean
  isFetching?: boolean
}

const ReportList = ({ reports, isFetching, isLoading }: ReportListProps) => {
  const [showDetail, setShowDetail] = useState<number | string>("")
	console.log(reports)
  return (
    <>
      <Table className="mt-4 h-full">
        <TableHeader className='sticky top-0'>
          <TableRow>
            <TableHead>ID</TableHead>
            <TableHead>Fullname - Apartment</TableHead>
            <TableHead>Title</TableHead>
            <TableHead>Sent Date</TableHead>
						<TableHead>Last Update</TableHead>
            <TableHead>Status</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {isLoading || isFetching
            ? Array.from({ length: 10 }).map((_, index) => (
                <TableRow key={index} className="">
                  <TableCell>
                    <Skeleton className="h-9 w-full" />
                  </TableCell>
                  <TableCell>
                    <Skeleton className="h-9 w-full" />
                  </TableCell>
                  <TableCell>
                    <Skeleton className="h-9 w-full" />
                  </TableCell>
                  <TableCell>
                    <Skeleton className="h-9 w-full" />
                  </TableCell>
                  <TableCell>
                    <Skeleton className="h-9 w-full" />
                  </TableCell>
									<TableCell>
                    <Skeleton className="h-9 w-full" />
                  </TableCell>
                </TableRow>
              ))
            : reports &&
              reports.map((report) => (
                <TableRow
                  key={report?.id}
                  className="font-medium cursor-pointer"
                  onClick={() => setShowDetail(report?.id)}>
                  <TableCell className="py-3">{report?.id}</TableCell>
                  <TableCell className="">
                    <div className="w-full flex items-center gap-3">
                      123
                      {/* <Avatar>
                        <AvatarImage
                          src={report?.user?.avatar ?? DefaultAvatar}
                          className="size-12 rounded-full object-cover hidden sm:inline-block"
                        />
                        <AvatarFallback>
                          {report.user.fullName
                            ? report.user.fullName.slice(0, 2)
                            : 'CN'}
                        </AvatarFallback>
                      </Avatar>
                      <div className="flex flex-col">
                        <p className="">{report?.user?.fullName}</p>
                      </div> */}
                    </div>
                  </TableCell>
                  <TableCell>{report.title}</TableCell>
                  <TableCell>
                    {new Date(report.createdAt).toLocaleDateString()}
                  </TableCell>
									<TableCell>
										{
											report.updatedAt === null ? "N/A" : new Date(report.updatedAt).toLocaleDateString()
										}
                  </TableCell>
                  <TableCell className="uppercase">
                    <Badge
                      variant={`${
                        report?.status === 'PENDING'
                          ? 'info'
                          : report.status === 'IN_PROGRESS'
                          ? 'warning'
                          : report.status === 'REJECTED'
                          ? 'destructive'
                          : 'success'
                      }`}>
                      {report.status}
                    </Badge>
                  </TableCell>
                </TableRow>
              ))}
        </TableBody>
      </Table>
      {showDetail && (
        <ReportDetail report={showDetail} setShowDetail={setShowDetail} />
      )}
    </>
  )
}

export default ReportList
