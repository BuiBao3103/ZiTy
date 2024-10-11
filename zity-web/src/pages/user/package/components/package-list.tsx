import { Separator } from '@/components/ui/separator'
import { memo } from 'react'
import { useNavigate } from 'react-router-dom'

interface PackageListProps {
  id?: string
}

const PackageList = ({ id }: PackageListProps) => {
  const navigate = useNavigate()
  const date = new Date()
  const formattedDate =
    ('0' + date.getDate()).slice(-2) +
    '/' +
    ('0' + (date.getMonth() + 1)).slice(-2) +
    '/' +
    date.getFullYear()

  return (
    <div className="w-full flex flex-col gap-4 bg-white">
      {Array.from({ length: 10 }).map((_, index) => (
        <div
          key={index}
          onClick={() => navigate(`/package/${index}`)}
          className={`rounded-md w-full flex flex-col border-2 cursor-pointer hover:bg-zinc-50 transition-all ${
            parseInt(id ?? '-1') === index
              ? 'bg-zinc-100 border-primary'
              : 'bg-white'
          }`}>
          <div className="w-full flex flex-col p-4">
            <h1 className="text-lg font-medium">P-A10{index}-Sep-2024</h1>
            <div className="w-full grid grid-cols-[100px_auto] text-sm">
              <span className="text-muted-foreground">Owner:</span>
              <span className="">Bui Hong Bao</span>
            </div>
            <div className="w-full grid grid-cols-[100px_auto] text-sm">
              <span className="text-muted-foreground">Apartment:</span>
              <span className="">A.10{index}</span>
            </div>
            <div className="w-full grid grid-cols-[100px_auto] text-sm">
              <span className="text-muted-foreground">Date:</span>
              <span className="">{formattedDate}</span>
            </div>
            <div className="w-full grid grid-cols-[100px_auto] text-sm">
              <span className="text-muted-foreground">From:</span>
              <span className="">Bui Hong Bao</span>
            </div>
            <div className="w-full grid grid-cols-[100px_auto] text-sm">
              <span className="text-muted-foreground">To:</span>
              <span className="">Bui Hong Bao</span>
            </div>
            <div className="w-full grid grid-cols-[100px_auto] text-sm">
              <span className="text-muted-foreground">Description:</span>
              <span className="">Bui Hong Bao</span>
            </div>
          </div>
          <Separator className={`h-0.5 ${parseInt(id ?? '-1') === index ? "bg-primary" : "bg-border"}`} />
          <div className="w-full flex justify-end py-2 px-4">
            <span className="text-lg text-red-600 font-medium">
              Not Collected
            </span>
          </div>
        </div>
      ))}
    </div>
  )
}

export default memo(PackageList)
