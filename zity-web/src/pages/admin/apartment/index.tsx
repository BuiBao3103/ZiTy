import { useDocumentTitle } from 'usehooks-ts'
import ApartmentList from './components/apartment-list'
import ApartmentForm from './components/apartment-form'
import { useParams } from 'react-router-dom'
import ApartmentDetail from './components/apartment-detail'
import { Input } from '@/components/ui/input'
import { Search } from 'lucide-react'
import BreadCrumb from '@/components/breadcrumb'
const Index = () => {
  const params = useParams()
  useDocumentTitle('Apartment')

  return (
    <div className="w-full h-full flex flex-col bg-zinc-100">
      <BreadCrumb
        paths={[
          {
            label: 'apartment',
            to: '/apartment',
          },
          ...(params.id ? [{ label: params.id, to: '/#' }] : []),
        ]}
      />
      <div className="w-full h-full p-4">
        <div className="bg-white w-full h-full rounded-md p-4 space-y-4">
          {!params.id && (
            <>
              <section className="w-full flex flex-col sm:flex-row sm:gap-0 gap-4 justify-between items-center">
                <div className="w-full lg:w-1/3 flex items-center border px-3 py-0.5 relative rounded-md focus-within:border-primary transition-all">
                  <Search size={20} />
                  <Input
                    placeholder="Search something"
                    className="border-none shadow-none focus-visible:ring-0"
                  />
                </div>
                <ApartmentForm textTrigger="New Apartment" />
              </section>
              <ApartmentList />
            </>
          )}
          {params.id && <ApartmentDetail />}
        </div>
      </div>
    </div>
  )
}

export default Index
