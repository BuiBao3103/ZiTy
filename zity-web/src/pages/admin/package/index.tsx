import { Input } from '@/components/ui/input'
import { Filter, Search } from 'lucide-react'
import { Button } from '@components/ui/button'
import PackageList from './components/package-list'
import BreadCrumb from '@/components/breadcrumb'
import { useDocumentTitle } from 'usehooks-ts'
import { useGetPackagesQuery } from '@/features/package/packageSlice'
import PaginationCustom from '@/components/pagination/PaginationCustom'
import { useState } from 'react'
import PackageDetail from './components/package-detail'
const Index = () => {
  useDocumentTitle('Package')
  const [showDetail, setShowDetail] = useState<number | undefined>(undefined)
  const [currentPage, setCurrentPage] = useState<number>(1)
  const {
    data: packages,
    isLoading,
    isFetching,
  } = useGetPackagesQuery({ page: currentPage, includes: ['user'] })

  return (
    <>
      <div className="w-full sm:h-screen flex flex-col bg-zinc-100">
        <BreadCrumb paths={[{ label: 'package', to: '/package' }]} />
        <div className="size-full p-4">
          <div className="size-full p-4 bg-white rounded-md flex flex-col">
            <div className="w-full h-auto flex lg:flex-row flex-col gap-4 justify-between items-center">
              <div className="w-full flex gap-4 items-center">
                <div className="lg:w-1/4 w-full flex items-center border px-3 py-0.5 relative rounded-md focus-within:border-primary transition-all">
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
              <Button
                type="button"
                onClick={() => {
                  setShowDetail(-1)
                }}
                className="w-full sm:w-[160px]"
                variant={'default'}
                size={'lg'}>
                New Package
              </Button>
            </div>
            <div className="size-full">
              <PackageList
                packages={packages?.contents}
                isFetching={isFetching}
                isLoading={isLoading}
              />
            </div>
            <PaginationCustom
              onPageChange={setCurrentPage}
              currentPage={currentPage}
              totalPages={packages?.totalPages}
            />
          </div>
        </div>
      </div>
      {showDetail === -1 && (
        <PackageDetail mode="create" setShowDetail={setShowDetail} />
      )}
    </>
  )
}

export default Index
