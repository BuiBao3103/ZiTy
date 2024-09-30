import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbPage,
  BreadcrumbSeparator,
} from '@/components/ui/breadcrumb'
import { Link, useParams } from 'react-router-dom'
import { useWindowSize } from 'usehooks-ts'
import BillList from './components/bill-list'
import { Input } from '@/components/ui/input'
import { Search } from 'lucide-react'

const Index = () => {
  const params = useParams()
  const { width = 0 } = useWindowSize()

  return (
    <div className="w-full sm:h-screen flex flex-col bg-zinc-100 overflow-hidden">
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
                <BreadcrumbPage>Bill</BreadcrumbPage>
              </BreadcrumbItem>
            )}
            {params.id && (
              <>
                <BreadcrumbItem>
                  <BreadcrumbLink asChild>
                    <Link to={'/bill'}>Bill</Link>
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
      <div className="w-full h-full p-4 flex gap-4 overflow-hidden">
        <div className="w-full h-full flex flex-col p-4 bg-white rounded-md">
          <div className="w-full h-full overflow-y-auto flex flex-col gap-4">
            {params.id && width < 1024 ? (
              <div className="w-full h-full p-4 bg-white rounded-md">
                {params.id}
              </div>
            ) : (
              <>
                <div className="flex items-center border px-3 py-0.5 relative rounded-md focus-within:border-primary transition-all">
                  <Search size={20} />
                  <Input
                    placeholder="Search something"
                    className="border-none shadow-none focus-visible:ring-0"
                  />
                </div>
                <BillList id={params.id} />
              </>
            )}
          </div>
        </div>
        {params.id && width > 1024 && (
          <div className="w-full h-full p-4 bg-white rounded-md lg:block hidden">
            {params.id}
          </div>
        )}
      </div>
    </div>
  )
}

export default Index
