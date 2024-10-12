import { Input } from '@/components/ui/input'
import {
  Pagination,
  PaginationContent,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from '@/components/ui/pagination'
import { Filter, Search } from 'lucide-react'
import { Button } from '@components/ui/button'
import ServiceForm from './components/service-form'
import ServiceList from './components/service-list'
import { Service } from '@/schema/service.validate'
import BreadCrumb from '@/components/breadcrumb'
import { useDocumentTitle } from 'usehooks-ts'

const Index = () => {

	useDocumentTitle('Service')
  const services: Service[] = [
    {
      id: 1,
      name: 'Service 1',
      price: 100,
      created_at: new Date(),
      description: 'Description 1',
    },
    {
      id: 2,
      name: 'Service 2',
      price: 200,
      created_at: new Date(),
      description: 'Description 2',
    },
    {
      id: 3,
      name: 'Service 3',
      price: 300,
      created_at: new Date(),
      description: 'Description 3',
    },
    {
      id: 4,
      name: 'Service 4',
      price: 400,
      created_at: new Date(),
      description: 'Description 4',
    },
    {
      id: 5,
      name: 'Service 5',
      price: 500,
      created_at: new Date(),
      description: 'Description 5',
    },
    {
      id: 6,
      name: 'Service 6',
      price: 600,
      created_at: new Date(),
      description: 'Description 6',
    },
  ]

  return (
    <>
      <div className="w-full sm:h-screen flex flex-col bg-zinc-100">
        <BreadCrumb paths={[{ label: 'service', to: '/service' }]} />
        <div className="size-full p-4">
          <div className="size-full p-4 bg-white rounded-md flex flex-col">
            <div className="w-full h-auto flex justify-between items-center">
              <div className="w-full flex gap-4 items-center">
                <div className="lg:w-1/4 flex items-center border px-3 py-0.5 relative rounded-md focus-within:border-primary transition-all">
                  <Search size={20} />
                  <Input
                    placeholder="Search something"
                    className="border-none shadow-none focus-visible:ring-0"
                  />
                </div>
                <Button className="gap-1" size={'lg'} variant={'secondary'}>
                  <Filter size={20} />
                  Filter
                </Button>
              </div>
              <ServiceForm />
            </div>
            <div className="size-full">
              <ServiceList services={services} />
            </div>
            <Pagination className="mt-2">
              <PaginationContent>
                <PaginationItem>
                  <PaginationPrevious to="#" />
                </PaginationItem>
                {[1, 2, 3, 4, 5].map((page) => (
                  <PaginationItem
                    key={page}
                    className={`${page === 1 ? 'bg-primary rounded-md' : ''}`}>
                    <PaginationLink to="#">{page}</PaginationLink>
                  </PaginationItem>
                ))}
                <PaginationItem>
                  <PaginationNext to="#" />
                </PaginationItem>
              </PaginationContent>
            </Pagination>
          </div>
        </div>
      </div>
    </>
  )
}

export default Index
