import { formatDateWithSlash } from '@/utils/Generate'
import ReportForm from './report-form'
import { Button } from '@/components/ui/button'
import { Badge } from '@/components/ui/badge'
import { IReport } from '@/schema/report.validate'
import AlertDelete from '@/components/alert/AlertDelete'

interface ReportItemProps {
  report: IReport
}

const ReportItem = ({ report }: ReportItemProps) => {

  const handleDelete = async () => {
    console.log('delete')
  }

  return (
    <div className="p-4 w-full h-[310px] bg-white rounded-md flex flex-col gap-4 border">
      <div className="w-full flex justify-between items-center">
        <span className="font-medium">
          {formatDateWithSlash(new Date(report.createdAt))}
        </span>
        <ReportForm mode="update" report={report}>
          <Button variant={'default'} size={'sm'}>
            Details
          </Button>
        </ReportForm>
      </div>
      <div className="w-full h-full rounded-md bg-zinc-100 p-4 border border-zinc-300">
        <p className="line-clamp-4 font-medium">
          <span className="font-normal">Description:</span> {report.content}
        </p>
      </div>
      <div className="flex gap-2 uppercase">
        <Badge variant={'info'}>Other</Badge>
        <Badge variant={'error'}>Environment</Badge>
        <Badge variant={'warning'}>Noise</Badge>
      </div>
      <div className="w-full flex justify-between items-center">
        <div className="flex gap-2">
          <div className="flex flex-col">
            <p className="font-medium">
              <span>{}</span>
            </p>
            <span className="text-sm font-medium ">
              {report.relationship?.apartmentId}
            </span>
          </div>
        </div>
        <AlertDelete
          description="report"
          setAction={() => handleDelete()}
          variants="error"
        />
      </div>
    </div>
  )
}

export default ReportItem
