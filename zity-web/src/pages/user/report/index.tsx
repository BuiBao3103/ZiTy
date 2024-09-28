import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbPage,
  BreadcrumbSeparator,
} from '@/components/ui/breadcrumb'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { BadgePlus, Search } from 'lucide-react'
import { Link, useParams } from 'react-router-dom'
const Index = () => {
  const params = useParams()
  const date = new Date()
  const formattedDate =
    ('0' + date.getDate()).slice(-2) +
    '/' +
    ('0' + (date.getMonth() + 1)).slice(-2) +
    '/' +
    date.getFullYear()
  return (
    <div className="w-full sm:h-screen flex flex-col bg-zinc-100">
      <div className="w-full px-4 pt-4">
        <Breadcrumb className="p-4 font-medium bg-white rounded-md">
          <BreadcrumbList>
            <BreadcrumbItem>
              <BreadcrumbLink asChild>
                <Link to={'/'}>Home</Link>
              </BreadcrumbLink>
            </BreadcrumbItem>
            <BreadcrumbSeparator />
            {!params.id && (
              <BreadcrumbItem>
                <BreadcrumbPage>Report</BreadcrumbPage>
              </BreadcrumbItem>
            )}
            {params.id && (
              <>
                <BreadcrumbItem>
                  <BreadcrumbLink asChild>
                    <Link to={'/package'}>Report</Link>
                  </BreadcrumbLink>
                </BreadcrumbItem>
                <BreadcrumbSeparator />
                <BreadcrumbItem>
                  <BreadcrumbPage>{params.id}</BreadcrumbPage>
                </BreadcrumbItem>
              </>
            )}
          </BreadcrumbList>
        </Breadcrumb>
      </div>
      <div className="w-full h-full p-4 overflow-hidden">
        <div className="w-full h-full p-4 bg-white rounded-md flex flex-col gap-4">
          <div className="w-full flex items-center border px-4 py-1 relative rounded-md">
            <Search size={18} />
            <Input
              placeholder="Search something"
              className="border-none shadow-none focus-visible:ring-0"
            />
          </div>
          <div
            className={`w-full h-full grid grid-cols-1 lg:grid-cols-3 xl:grid-cols-5 gap-4 overflow-y-auto`}>
            <div className="p-4 w-full h-[300px] bg-white rounded-md flex flex-col justify-center items-center gap-2 border">
              <p className="text-xl font-medium">New Report</p>
              <BadgePlus size={50} />
            </div>
            {Array.from({ length: 10 }).map((_, index) => (
              <div
                key={index}
                className="p-4 w-full h-[300px] bg-white rounded-md flex flex-col gap-2 border">
                <div className="w-full flex justify-between items-center">
                  <span>{formattedDate}</span>
                  <Button variant={'default'} size={'sm'}>
                    Details
                  </Button>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  )
}

export default Index
