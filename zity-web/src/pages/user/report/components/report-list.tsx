import { Badge } from '@/components/ui/badge'
import ReportForm from './report-form'
import { Button } from '@/components/ui/button'
import { BadgePlus } from 'lucide-react'
import AlertDelete from '@/components/alert/AlertDelete'
import { IReport } from '@/schema/report.validate'
import { formatDateWithSlash } from '@/utils/Generate'
import ReportItem from './report-item'

interface ReportListProps {
  reports?: IReport[]
}

const ReportList = ({ reports }: ReportListProps) => {

  return (
    <div
      className={`w-full h-full grid grid-cols-1 lg:grid-cols-3 gap-4 overflow-y-auto`}>
      <ReportForm mode="create">
        <div className="p-4 w-full h-[310px] bg-white hover:bg-zinc-100 transition-all cursor-pointer rounded-md flex flex-col justify-center items-center gap-2 border">
          <p className="text-xl font-medium">New Report</p>
          <BadgePlus size={50} />
        </div>
      </ReportForm>
      {reports &&
        reports.map((report, index) => (
          <ReportItem report={report} key={index} />
        ))}
    </div>
  )
}

export default ReportList
