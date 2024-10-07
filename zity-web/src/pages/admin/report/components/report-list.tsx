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

interface ReportListProps {
  reports: Report[]
}

const ReportList = ({ reports }: ReportListProps) => {
  const [showDetail, setShowDetail] = useState<Report | null>(null)

  return (
    <>
      <Table className="mt-4">
        <TableHeader>
          <TableRow>
            <TableHead>ID</TableHead>
            <TableHead>Fullname - Apartment</TableHead>
            <TableHead>Title</TableHead>
            <TableHead>Sent Date</TableHead>
            <TableHead>Status</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {reports.map((report) => (
            <TableRow
              key={report?.id}
              className="font-medium cursor-pointer"
              onClick={() => setShowDetail(report)}>
              <TableCell>{report?.id}</TableCell>
              <TableCell className="">
                <div className="w-full flex items-center gap-3">
                  <Avatar>
                    <AvatarImage
                      src={report?.user?.avatar ?? DefaultAvatar}
                      className="size-12 rounded-full object-cover hidden sm:inline-block"
                    />
                    <AvatarFallback>
                      {report.user.full_name
                        ? report.user.full_name.slice(0, 2)
                        : 'CN'}
                    </AvatarFallback>
                  </Avatar>
                  <div className="flex flex-col">
                    <p className="">{report?.user?.full_name}</p>
                  </div>
                </div>
              </TableCell>
              <TableCell>{report.title}</TableCell>
              <TableCell>{report.created_at.toLocaleDateString()}</TableCell>
              <TableCell className="uppercase">
                <Badge
                  variant={`${report?.status === 'PENDING' ? 'info' : report.status === 'IN_PROGRESS' ? 'warning' : report.status === 'REJECTED' ? 'destructive' : 'success'}`}>
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
