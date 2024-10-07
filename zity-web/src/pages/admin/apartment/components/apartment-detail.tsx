import { Button } from '@/components/ui/button'
import { ChevronLeft } from 'lucide-react'
import { useNavigate, useParams } from 'react-router-dom'

const ApartmentDetail = () => {
  const navigate = useNavigate()
	const params = useParams()

  return (
    <div className="w-full">
      <div className="flex gap-2 items-center">
        <Button
          onClick={() => navigate('/apartment')}
          size={'icon'}
          variant={'ghost'}>
          <ChevronLeft />
        </Button>
        <h1 className="text-xl font-medium">{params.id}</h1>
      </div>
			
    </div>
  )
}

export default ApartmentDetail
