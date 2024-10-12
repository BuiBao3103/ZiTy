import CreateSurveyForm from '@admin/survey/components/create-survey-form'
import { useDocumentTitle } from 'usehooks-ts'

const Index = () => {

	useDocumentTitle('Home')

  return (
    <div className="w-full h-full p-6">
      <CreateSurveyForm />
    </div>
  )
}

export default Index
