import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbPage,
  BreadcrumbSeparator,
} from '@/components/ui/breadcrumb'
import {
  Select,
  SelectContent,
  SelectGroup,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'
import { Separator } from '@/components/ui/separator'
import { Link, useNavigate, useParams } from 'react-router-dom'
import { useWindowSize } from 'usehooks-ts'
import PackageList from './components/package-list'
import { Search } from 'lucide-react'
import { Input } from '@/components/ui/input'

const Index = () => {
  const filterBar: string[] = ['All', 'Not Collected', 'Collected']
  const params = useParams()
  const { width = 0 } = useWindowSize()
  const navigate = useNavigate()
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
                <BreadcrumbPage>Package</BreadcrumbPage>
              </BreadcrumbItem>
            )}
            {params.id && (
              <>
                <BreadcrumbItem>
                  <BreadcrumbLink asChild>
                    <Link to={'/package'}>Package</Link>
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
                <div className="w-full h-fit flex flex-col sm:flex-row justify-between items-center sm:gap-0 gap-4">
                  <div className="w-full lg:w-1/2 flex items-center border px-3 py-0.5 relative rounded-md focus-within:border-primary transition-all">
                    <Search size={20} />
                    <Input
                      placeholder="Search something"
                      className="border-none shadow-none focus-visible:ring-0"
                    />
                  </div>
                  <div className="w-full sm:w-fit rounded-md flex bg-zinc-200 overflow-hidden">
                    {filterBar.map((item, index) => (
                      <span
                        key={index}
                        className="inline-block w-full text-nowrap text-center font-medium px-4 py-2 bg-transparent hover:bg-primary transition-all cursor-pointer">
                        {item}
                      </span>
                    ))}
                  </div>
                  {/* <Select defaultValue={'apple'}>
                    <SelectTrigger className="w-full sm:w-[140px] xl:w-[180px] h-full">
                      <SelectValue placeholder="Select a fruit" />
                    </SelectTrigger>
                    <SelectContent>
                      <SelectGroup>
                        <SelectItem value="apple">Apple</SelectItem>
                        <SelectItem value="banana">Banana</SelectItem>
                        <SelectItem value="blueberry">Blueberry</SelectItem>
                        <SelectItem value="grapes">Grapes</SelectItem>
                        <SelectItem value="pineapple">Pineapple</SelectItem>
                      </SelectGroup>
                    </SelectContent>
                  </Select> */}
                </div>
                <PackageList id={params.id} />
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
