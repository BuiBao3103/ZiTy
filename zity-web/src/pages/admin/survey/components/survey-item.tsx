import AlertDelete from '@/components/alert/AlertDelete'
import { Button } from '@/components/ui/button'
import { useDeleteSurveyMutation } from '@/features/survey/surveySlice'
import { ISurvey } from '@/schema/survey.validate'
import { AlarmClock, Clock } from 'lucide-react'
import { memo } from 'react'
import { useNavigate } from 'react-router-dom'

interface SurveyItemProps {
  survey: ISurvey
}

const SurveyItem = ({ survey }: SurveyItemProps) => {
  const [deleteSurvey, { isLoading }] = useDeleteSurveyMutation()
  const navigate = useNavigate()
  const handleDelete = async () => {
    await deleteSurvey(survey.id)
      .unwrap()
      .then(() => {
        console.log('delete')
      })
      .catch((err) => {})
  }

  return (
    <div className="w-full p-4 rounded-md border flex flex-col gap-4">
      <div className="w-full flex justify-between items-center">
        <section className="w-full space-y-0.5 font-medium">
          <h1>{survey.title}</h1>
          <p className="text-sm text-muted-foreground">
            Total Questions: <span>{survey.totalQuestions}</span>
          </p>
        </section>
        <div className="w-full flex gap-4 justify-end items-center">
          <section className="w-full flex gap-2">
            <span className="w-16 inline-flex rounded-sm bg-zinc-100 justify-center items-center">
              <Clock />
            </span>
            <div className="w-full flex flex-col">
              <p className="text-sm font-medium text-zinc-500">Start Date</p>
              <p className="w-full text-sm font-medium">
                {new Date(survey.startDate).toLocaleDateString('vi-VN', {
                  day: '2-digit',
                  month: '2-digit',
                  year: 'numeric',
									hour: '2-digit',
									minute: '2-digit',
									second: '2-digit',
									hour12: false
                })}
              </p>
            </div>
          </section>
          <section className="w-full flex gap-2">
            <span className="w-16 inline-flex rounded-sm bg-zinc-100 justify-center items-center">
              <AlarmClock />
            </span>
            <div className="w-full flex flex-col">
              <p className="text-sm font-medium text-zinc-500">Due Date</p>
              <p className="w-full text-sm font-medium">
                {new Date(survey.endDate).toLocaleDateString('vi-VN', {
                  day: '2-digit',
                  month: '2-digit',
                  year: 'numeric',
									hour: '2-digit',
									minute: '2-digit',
									second: '2-digit',
									hour12: false
                })}
              </p>
            </div>
          </section>
        </div>
      </div>
      <div className="w-full flex gap-4">
        <Button
          onClick={() => navigate(`/admin/survey/${survey.id}`)}
          type="button"
          variant={'warning'}>
          Edit
        </Button>
        <AlertDelete
          description="survey"
          setAction={() => handleDelete()}
          isLoading={isLoading}
          variants="error"
        />
      </div>
    </div>
  )
}

export default memo(SurveyItem)
