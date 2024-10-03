import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbPage,
  BreadcrumbSeparator,
} from '@/components/ui/breadcrumb'
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
import { Link } from 'react-router-dom'
import BillList from './components/bill-list'
import { Bill } from '@/schema/bill.validate'
import { useDocumentTitle } from 'usehooks-ts'

const Index = () => {

	useDocumentTitle('Bill')

  const bills: Bill[] = [
    {
      id: 1,
      monthly: 1,
      total_price: 1,
      old_water: 1,
      new_water: 1,
      status: 'UNPAID',
      relationship_id: 1,
    },
    {
      id: 2,
      monthly: 2,
      total_price: 2,
      old_water: 2,
      new_water: 2,
      status: 'PAID',
      relationship_id: 2,
    },
    {
      id: 3,
      monthly: 3,
      total_price: 3,
      old_water: 3,
      new_water: 3,
      status: 'OVERDUE',
      relationship_id: 3,
    },
  ]

  return (
    <>
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
              <BreadcrumbItem>
                <BreadcrumbPage>Bill</BreadcrumbPage>
              </BreadcrumbItem>
            </BreadcrumbList>
          </Breadcrumb>
        </div>
        <div className="size-full p-4">
          <div className="size-full p-4 bg-white rounded-md">
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
            </div>
            <BillList bills={bills} />
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
