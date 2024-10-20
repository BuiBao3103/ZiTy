import ApartmentDetailSkeleton from '@/components/skeleton/ApartmentDetailSkeleton'
import { Button } from '@/components/ui/button'
import { useGetApartmentQuery } from '@/features/apartment/apartmentSlice'
import { ChevronLeft } from 'lucide-react'
import { useNavigate } from 'react-router-dom'
import ApartmentFormDetail from './components/apartment-form-detail'

interface IApartmentDetailProps {
  id: string
}

const ApartmentDetail = ({ id }: IApartmentDetailProps) => {
  const navigate = useNavigate()
  const { data, isLoading, isFetching } = useGetApartmentQuery(id, {
    skip: !id,
  })
  return (
    <div className="w-full h-full flex flex-col gap-4 relative">
      {isLoading || isFetching ? (
        <ApartmentDetailSkeleton />
      ) : (
        <>
          <div className="flex items-center">
            <Button
              onClick={() => navigate('/apartment')}
              size={'icon'}
              variant={'ghost'}>
              <ChevronLeft />
            </Button>
            <h1 className="text-xl font-medium">{data?.id}</h1>
          </div>
          <ApartmentFormDetail apartment={data} />
        </>
      )}
    </div>
  )
}

export default ApartmentDetail
