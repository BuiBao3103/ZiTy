import BreadCrumb from '@/components/breadcrumb'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import {
  Pagination,
  PaginationContent,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from '@/components/ui/pagination'
import { Clock, Filter, Search, AlarmClock } from 'lucide-react'
import { useParams } from 'react-router-dom'

const Index = () => {
  const params = useParams()

  return (
    <div className="w-full sm:h-screen flex flex-col bg-zinc-100">
      <BreadCrumb
        paths={[
          { label: 'survey', to: '/survey' },
          ...(params.id ? [{ label: params.id }] : []),
        ]}
      />
      <div className="w-full h-full p-4 overflow-hidden">
        <div className="w-full h-full bg-white rounded-md p-4 flex flex-col	space-y-4">
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
            <Button size={'lg'} variant={'default'}>
              Create Survey
            </Button>
          </div>
          <div className="w-full h-full overflow-y-auto flex flex-col gap-2">
            {Array.from({ length: 10 }).map((_, index) => (
              <div
                key={index}
                className="w-full p-4 rounded-md border flex flex-col gap-4">
                <div className="w-full flex justify-between items-center">
                  <div className="w-full">
                    <h1 className="text-xl font-medium">Title</h1>
                    <p className="text-sm font-medium text-zinc-500">
                      Enim commodo eu ullamco aliqua.Esse esse magna Lorem id ad
                      irure nisi velit.
                    </p>
                  </div>
                  <div className="w-full flex gap-4 justify-end items-center">
                    <section className="flex gap-2">
                      <span className="w-16 inline-flex rounded-sm bg-zinc-300 justify-center items-center">
                        <Clock />
                      </span>
                      <div className="w-full flex flex-col">
                        <p className="text-sm font-medium text-zinc-500">
                          Start Date
                        </p>
                        <p className="text-sm font-medium">12/12/2021</p>
                      </div>
                    </section>
                    <section className="flex gap-2">
                      <span className="w-16 inline-flex rounded-sm bg-zinc-300 justify-center items-center">
                        <AlarmClock />
                      </span>
                      <div className="w-full flex flex-col">
                        <p className="text-sm font-medium text-zinc-500">
                          Due Date
                        </p>
                        <p className="text-sm font-medium">12/12/2021</p>
                      </div>
                    </section>
                  </div>
                </div>
                <div className="w-full flex gap-4">
                  <Button size={'sm'} variant={'warning'}>
                    Edit
                  </Button>
                  <Button size={'sm'} variant={'error'}>
                    Delete
                  </Button>
                </div>
              </div>
            ))}
          </div>
          {/* <Pagination className="mt-2">
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
          </Pagination> */}
        </div>
      </div>
    </div>
  )
}

export default Index
