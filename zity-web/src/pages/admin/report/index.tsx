import { useDocumentTitle } from 'usehooks-ts'
import ReportList from './components/report-list'
import { Input } from '@/components/ui/input'
import { Search } from 'lucide-react'
import BreadCrumb from '@/components/breadcrumb'
import { Report } from '@/schema/report.validate'
const Index = () => {
  useDocumentTitle('Report')

  const reports: Report[] = [
    {
      id: 1,
      title: 'Report 1',
      content: 'Description 1',
      status: 'PENDING',
      created_at: new Date(),
      user: {
        full_name: 'User 1',
        avatar: 'https://www.w3schools.com/howto/img_avatar.png',
      },
    },
  ]

  return (
    <div className="w-full h-full flex flex-col bg-zinc-100">
      <BreadCrumb
        paths={[
          {
            label: 'report',
            to: '/admin/report',
          },
        ]}
      />
      <div className="w-full h-full p-4">
        <div className="bg-white w-full h-full rounded-md p-4 space-y-4">
          <section className="w-full flex flex-col sm:flex-row sm:gap-0 gap-4 justify-between items-center">
            <div className="w-full lg:w-1/3 flex items-center border px-3 py-0.5 relative rounded-md focus-within:border-primary transition-all">
              <Search size={20} />
              <Input
                placeholder="Search something"
                className="border-none shadow-none focus-visible:ring-0"
              />
            </div>
          </section>
          <ReportList reports={reports} />
        </div>
      </div>
    </div>
  )
}

export default Index
