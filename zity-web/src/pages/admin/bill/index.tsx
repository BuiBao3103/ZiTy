import { Input } from '@/components/ui/input'
import { Filter, Search } from 'lucide-react'
import { Button } from '@components/ui/button'
import BillList from './components/bill-list'
import { useDocumentTitle } from 'usehooks-ts'
import BreadCrumb from '@/components/breadcrumb'
import { useGetBillsQuery } from '@/features/bill/billSlice'
import { useState } from 'react'
import PaginationCustom from '@/components/pagination/PaginationCustom'
const Index = () => {
  useDocumentTitle('Bill')
  const [currentPage, setCurrentPage] = useState<number>(1)
  const {
    data: bills,
    isLoading,
    isFetching,
  } = useGetBillsQuery({ page: currentPage, includes: ['Relationship'] })

  return (
    <>
      <div className="w-full sm:h-screen flex flex-col bg-zinc-100">
        <BreadCrumb paths={[{ label: 'bill', to: '/bill' }]} />
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
            </div>
            <div className="size-full">
              <BillList
                bills={bills?.contents}
                isFetching={isFetching}
                isLoading={isLoading}
              />
            </div>
            <PaginationCustom
              onPageChange={setCurrentPage}
              currentPage={currentPage}
              totalPages={bills?.totalPages}
            />
          </div>
        </div>
      </div>
    </>
  )
}

export default Index
