import AlertDelete from '@/components/alert/AlertDelete'
import BreadCrumb from '@/components/breadcrumb'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import {
  createNewSurvey,
  useGetSurverysQuery,
} from '@/features/survey/surveySlice'
import { RootState, useAppDispath } from '@/store'
import { Filter, Search } from 'lucide-react'
import { useSelector } from 'react-redux'
import { useParams } from 'react-router-dom'
import { useDocumentTitle } from 'usehooks-ts'
import SurveyForm from './components/survey-form'
import { useState } from 'react'
import SurveyItem from './components/survey-item'
import { Skeleton } from '@/components/ui/skeleton'
import PaginationCustom from '@/components/pagination/PaginationCustom'
import SurveyDetail from './components/survey-detail'
import SurveyList from './components/survey-list'

const Index = () => {
  useDocumentTitle('Survey')
  const params = useParams()
  const dispatch = useAppDispath()
  const isCreateNewSurvey = useSelector(
    (state: RootState) => state.surveyReducer.isCreateNewSurvey,
  )
  const [currentPage, setCurrentPage] = useState<number>(1)
  const {
    data: surveys,
    isLoading,
    isFetching,
  } = useGetSurverysQuery(currentPage)

  return (
    <div
      className={`w-full ${
        isCreateNewSurvey || params.id ? 'h-full' : 'h-full sm:h-screen'
      } flex flex-col bg-zinc-100 `}>
      <BreadCrumb
        paths={[
          { label: 'survey', to: '/admin/survey' },
          ...(params.id ? [{ label: params.id }] : []),
          ...(isCreateNewSurvey ? [{ label: 'New Survey' }] : []),
        ]}
      />
      <div className="w-full h-full p-4 overflow-hidden">
        <div className="w-full h-full bg-white rounded-md p-4 flex flex-col	space-y-4">
          {isCreateNewSurvey ? (
            <SurveyForm mode="create" />
          ) : params?.id ? (
            <SurveyDetail surveyID={params?.id} />
          ) : (
            <>
              <div className="w-full h-auto flex lg:flex-row flex-col gap-4 justify-between items-center">
                <div className="w-full flex gap-4 items-center">
                  <div className="lg:w-1/4 flex items-center border px-3 py-0.5 relative rounded-md focus-within:border-primary transition-all">
                    <Search size={20} />
                    <Input
                      placeholder="Search something"
                      className="border-none shadow-none focus-visible:ring-0"
                    />
                  </div>
                  <Button className="gap-1" size={'lg'} variant={'secondary'}>
                    <Filter size={20} />
                    Filter
                  </Button>
                </div>
                <Button
                  onClick={() =>
                    dispatch(createNewSurvey({ isCreateNewSurvey: true }))
                  }
                  size={'lg'}
                  variant={'default'}>
                  Create Survey
                </Button>
              </div>
              <div className="size-full flex flex-col overflow-hidden">
                <SurveyList
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
            </>
          )}
        </div>
      </div>
    </div>
  )
}

export default Index
