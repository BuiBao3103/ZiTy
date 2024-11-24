import { useDocumentTitle } from 'usehooks-ts'
import BreadCrumb from '@/components/breadcrumb'
import ReportList from './components/report-list'
import { useState } from 'react'
import { useGetReportsQuery } from '@/features/reports/reportSlice'
import { useAppSelector } from '@/store'
const Index = () => {
  useDocumentTitle('Report')
  const [currentPage, setCurrentPage] = useState<number>(1)
  const user = useAppSelector((state) => state.userReducer.user)
  const { data, isLoading, isFetching } = useGetReportsQuery({
    page: currentPage,
    includes: ['relationship'],
    relationshipId: user?.relationships?.[0]?.id,
  })
  return (
    <div className="w-full sm:h-screen flex flex-col bg-zinc-100">
      <BreadCrumb paths={[{ label: 'report', to: '/reports' }]} />
      <div className="w-full h-full p-4 overflow-hidden">
        <div className="w-full h-full p-4 bg-white rounded-md flex flex-col gap-4">
          <ReportList reports={data?.contents} isLoading={isLoading} isFetching={isFetching} />
        </div>
      </div>
    </div>
  )
}

export default Index
