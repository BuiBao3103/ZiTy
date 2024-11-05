import { useGetSurveyByIdQuery } from '@/features/survey/surveySlice'
import { useParams } from 'react-router-dom'
import SurveyForm from './components/survey-form'

const SurveyDoingForm = () => {
  const params = useParams()
  const {
    data: survey,
    isLoading,
    isFetching,
  } = useGetSurveyByIdQuery(params?.id as string, {
    skip: !params?.id,
  })
  console.log(survey)
  return (
    <div className="p-4 flex flex-col gap-4">
      <h1 className="text-xl font-semibold">{survey?.title}</h1>
      {isLoading || isFetching ? (
        <p>Loading...</p>
      ) : (
        <SurveyForm survey={survey} />
      )}
    </div>
  )
}

export default SurveyDoingForm
