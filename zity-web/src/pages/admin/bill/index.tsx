import { Input } from '@/components/ui/input'
import { Droplets, Filter, Search } from 'lucide-react'
import { Button } from '@components/ui/button'
import BillList from './components/bill-list'
import { useDocumentTitle } from 'usehooks-ts'
import BreadCrumb from '@/components/breadcrumb'
import { useGetBillsQuery } from '@/features/bill/billSlice'
import { useState } from 'react'
import PaginationCustom from '@/components/pagination/PaginationCustom'
import PageSizeSelector from '@/components/table/page-size-selector'
import PaginationInfo from '@/components/table/page-info'
import { Tooltip, TooltipContent, TooltipTrigger } from '@/components/ui/tooltip'
import BillUpdateWaterReading from './components/bill-update-water-reading'
const Index = () => {
  useDocumentTitle('Bill')
  const [isOpenWaterReading, setIsOpenWaterReading] = useState<boolean>(false)
  const [pageSize, setPageSize] = useState<number>(10)
  const [currentPage, setCurrentPage] = useState<number>(1)
  const {
    data: bills,
    isLoading,
    isFetching,
  } = useGetBillsQuery({
    page: currentPage,
    includes: ['Relationship'],
    pageSize: pageSize,
  })

  return (
    <>
      <div className="w-full sm:h-screen flex flex-col bg-zinc-100">
        <BreadCrumb paths={[{ label: 'bill', to: '/bill' }]} />
        <div className="size-full p-4 overflow-hidden">
          <div className="size-full p-4 bg-white rounded-md flex flex-col space-y-2">
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
              <div className="flex gap-2">
                <Tooltip>
                  <TooltipTrigger asChild>
                    <Button
                      type="button"
                      onClick={() => setIsOpenWaterReading(true)}
                      size={'icon'}
                      variant={'outline'}>
                      <Droplets size={20} />
                    </Button>
                  </TooltipTrigger>
                  <TooltipContent>Update water reading</TooltipContent>
                </Tooltip>
              </div>
            </div>
            <div className="size-full overflow-y-auto">
              <BillList bills={bills?.contents} isFetching={isFetching} isLoading={isLoading} />
            </div>
            <div className="w-full flex justify-between items-center">
              <PageSizeSelector
                className="w-full"
                pageSize={pageSize}
                onPageSizeChange={setPageSize}
              />
              <div className="w-full">
                <PaginationCustom
                  currentPage={currentPage}
                  onPageChange={setCurrentPage}
                  totalPages={bills?.totalPages}
                />
              </div>
              <PaginationInfo
                className="w-full whitespace-nowrap"
                currentPage={currentPage}
                pageSize={pageSize}
                totalItems={bills?.totalItems}
                loading={isLoading || isFetching}
              />
            </div>
          </div>
        </div>
      </div>
      {isOpenWaterReading && <BillUpdateWaterReading setIsOpenWaterReading={setIsOpenWaterReading} />}
    </>
  )
}

export default Index
