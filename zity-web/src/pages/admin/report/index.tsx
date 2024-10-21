import { useDocumentTitle } from 'usehooks-ts'
import ReportList from './components/report-list'
import { Input } from '@/components/ui/input'
import { Search } from 'lucide-react'
import BreadCrumb from '@/components/breadcrumb'
import { useGetReportsQuery } from '@/features/reports/reportSlice'
import { useState } from 'react'
import PaginationCustom from '@/components/pagination/PaginationCustom'
const Index = () => {
  const [currentPage, setCurrentPage] = useState<number>(1)
  const {
    data: reports,
    isLoading,
    isFetching,
  } = useGetReportsQuery(currentPage)
  useDocumentTitle('Report')

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
        <div className="bg-white w-full h-full rounded-md p-4 space-y-4 flex flex-col">
          <section className="w-full flex flex-col sm:flex-row sm:gap-0 gap-4 justify-between items-center">
            <div className="w-full lg:w-1/3 flex items-center border px-3 py-0.5 relative rounded-md focus-within:border-primary transition-all">
              <Search size={20} />
              <Input
                placeholder="Search something"
                className="border-none shadow-none focus-visible:ring-0"
              />
            </div>
          </section>
          <div className="w-full h-full">
            <ReportList
              reports={reports?.contents}
              isLoading={isLoading}
              isFetching={isFetching}
            />
          </div>
          <PaginationCustom
            currentPage={currentPage}
            onPageChange={setCurrentPage}
            totalPages={reports?.totalPages}
          />
        </div>
      </div>
    </div>
  )
}

export default Index
