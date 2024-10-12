import { Badge } from '@/components/ui/badge'
import { Button } from '@/components/ui/button'
import { useNavigate } from 'react-router-dom'

const ApartmentList = () => {
  const navigate = useNavigate()

  return (
    <div className="w-full flex flex-col gap-4">
      <div className="w-full p-4 border rounded-md bg-zinc-100">
        <h1 className="text-xl font-bold">Floor 1</h1>
        <div className="w-full flex gap-4 mt-4 overflow-x-auto">
          {Array.from({ length: 6 }).map((_, index) => (
            <div
              key={index}
              className="p-4 min-w-full sm:min-w-[300px] flex flex-col gap-1.5 bg-white rounded-lg border">
              <div className="w-full h-full grid grid-cols-2">
                <span className="text-lg font-medium">A.10{index}</span>
                <Button
                  variant="default"
                  size="sm"
                  onClick={() => {
                    navigate(`/apartment/A.10${index}`)
                  }}
                  className="w-fit place-self-end">
                  Details
                </Button>
              </div>
              <div className="w-full h-full grid grid-cols-2">
                <span className="text-sm font-medium">
                  Status
                </span>
                <Badge className='w-fit place-self-end uppercase' variant={"error"}>
									disruption
								</Badge>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  )
}

export default ApartmentList
