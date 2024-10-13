import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { BadgePlus, Search } from 'lucide-react'
import { useDocumentTitle } from 'usehooks-ts'
import ReportForm from './components/report-form'
import { Badge } from '@/components/ui/badge'
import BreadCrumb from '@/components/breadcrumb'
import ReportList from './components/report-list'
const Index = () => {
  useDocumentTitle('Report')
  return (
    <div className="w-full sm:h-screen flex flex-col bg-zinc-100">
      <BreadCrumb paths={[{ label: 'report', to: '/report' }]} />
      <div className="w-full h-full p-4 overflow-hidden">
        <div className="w-full h-full p-4 bg-white rounded-md flex flex-col gap-4">
          <div className="w-full flex items-center border px-4 py-1 relative rounded-md focus-within:border-primary transition-all">
            <Search size={18} />
            <Input
              placeholder="Search something"
              className="border-none shadow-none focus-visible:ring-0"
            />
          </div>
          <ReportList />
        </div>
      </div>
    </div>
  )
}

export default Index
