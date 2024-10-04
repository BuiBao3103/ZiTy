import AlertDelete from '@/components/alert/AlertDelete'
import BreadCrumb from '@/components/breadcrumb'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { createNewSurvey } from '@/features/survey/surveySlice'
import { RootState, useAppDispath } from '@/store'
import { Clock, Filter, Search, AlarmClock } from 'lucide-react'
import { useSelector } from 'react-redux'
import { useParams } from 'react-router-dom'
import { useDocumentTitle } from 'usehooks-ts'
import CreateSurveyForm from './components/create-survey-form'

const Index = () => {
  useDocumentTitle('Survey')
  const params = useParams()
  const isCreateNewSurvey = useSelector(
    (state: RootState) => state.surveyReducer.isCreateNewSurvey,
  )
  const dispatch = useAppDispath()

  const setAction = () => {
    console.log('delete')
  }

  return (
    <div className={`w-full ${isCreateNewSurvey ? "h-full" : "sm:h-screen"} flex flex-col bg-zinc-100`}>
      <BreadCrumb
        paths={[
          { label: 'survey', to: '/admin/survey' },
          ...(params.id ? [{ label: params.id }] : []),
					...(isCreateNewSurvey ? [{label: 'New Survey'}] : []),
        ]}
      />
      <div className="w-full h-full p-4 overflow-hidden">
        <div className="w-full h-full bg-white rounded-md p-4 flex flex-col	space-y-4">
          {isCreateNewSurvey ? (
            <CreateSurveyForm />
          ) : (
            <>
              <div className="w-full h-auto flex justify-between items-center">
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
              <div className="w-full h-full overflow-y-auto grid grid-cols-2 gap-4">
                {Array.from({ length: 10 }).map((_, index) => (
                  <div
                    key={index}
                    className="w-full p-4 rounded-md border flex flex-col gap-4">
                    <div className="w-full flex justify-between items-center">
                      <div className="w-full space-y-0.5 font-medium">
                        <h1 className="text-xl">Title</h1>
                        <p className="text-sm text-zinc-500">
                          Enim commodo eu ullamco aliqua.Esse esse magna Lorem
                          id ad irure nisi velit.
                        </p>
                        <p>
                          Total Questions: <span>5</span>
                        </p>
                      </div>
                      <div className="w-full flex gap-4 justify-end items-center">
                        <section className="flex gap-2">
                          <span className="w-16 inline-flex rounded-sm bg-zinc-100 justify-center items-center">
                            <Clock />
                          </span>
                          <div className="w-full flex flex-col">
                            <p className="text-sm font-medium text-zinc-500">
                              Start Date
                            </p>
                            <p className="text-sm font-medium">12/12/2021</p>
                          </div>
                        </section>
                        <section className="flex gap-2">
                          <span className="w-16 inline-flex rounded-sm bg-zinc-100 justify-center items-center">
                            <AlarmClock />
                          </span>
                          <div className="w-full flex flex-col">
                            <p className="text-sm font-medium text-zinc-500">
                              Due Date
                            </p>
                            <p className="text-sm font-medium">12/12/2021</p>
                          </div>
                        </section>
                      </div>
                    </div>
                    <div className="w-full flex gap-4">
                      <Button variant={'warning'}>Edit</Button>
                      <AlertDelete
                        description="survey"
                        setAction={setAction}
                        buttonType="error"
                      />
                    </div>
                  </div>
                ))}
              </div>
            </>
          )}
        </div>
      </div>
    </div>
  )
}

export default Index
