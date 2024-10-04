import { useParams } from 'react-router-dom'
import { useWindowSize } from 'usehooks-ts'
import BillList from './components/bill-list'
import { Input } from '@/components/ui/input'
import { Search } from 'lucide-react'
import BreadCrumb from '@/components/breadcrumb'
const Index = () => {
  const params = useParams()
  const { width = 0 } = useWindowSize()

  return (
    <div className="w-full sm:h-screen flex flex-col bg-zinc-100 overflow-hidden">
      <BreadCrumb
        paths={[
          { label: 'user', to: '/user' },
          ...(params.id ? [{ label: params.id }] : []),
        ]}
      />
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
