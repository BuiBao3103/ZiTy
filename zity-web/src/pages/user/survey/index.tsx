import { useState } from 'react'
import BreadCrumb from '@/components/breadcrumb'
import { useParams } from 'react-router-dom'
import { useDocumentTitle } from 'usehooks-ts'
import { useGetSurverysQuery } from '@/features/survey/surveySlice'
import SurveyListUser from './components/survey-list'
import PaginationCustom from '@/components/pagination/PaginationCustom'
const Index = () => {
  useDocumentTitle('Survey')
  const params = useParams()
  const [currentPage, setCurrentPage] = useState<number>(1)
  const {
    data: surveys,
    isLoading,
    isFetching,
  } = useGetSurverysQuery(currentPage)
  return (
    <div className="w-full sm:h-screen flex flex-col bg-zinc-100">
      <BreadCrumb
        paths={[
          { label: 'survey', to: '/survey' },
          ...(params.id ? [{ label: params.id }] : []),
        ]}
      />
      <div className="size-full p-4 overflow-hidden">
        <div className="size-full flex flex-col bg-white rounded-md space-y-4 p-4">
          <SurveyListUser
            surveys={surveys?.contents}
            isFetching={isFetching}
            isLoading={isLoading}
          />
          <PaginationCustom
            currentPage={currentPage}
            onPageChange={setCurrentPage}
            totalPages={surveys?.totalPages}
          />
        </div>
      </div>
    </div>
  )
}

export default Index
