import BreadCrumb from '@/components/breadcrumb'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Filter, Search } from 'lucide-react'
import UserList from './components/user-list'
import { useDocumentTitle } from 'usehooks-ts'
import UserForm from './components/user-form'
import { useGetUserQuery } from '@/features/user/userSlice'
import { useState } from 'react'
import PaginationCustom from '@/components/pagination/PaginationCustom'

const Index = () => {
  useDocumentTitle('User')
  const [currentPage, setCurrentPage] = useState<number>(1)
  const { data: users, isLoading, isFetching } = useGetUserQuery(currentPage)
  return (
    <>
      <div className="w-full sm:h-screen flex flex-col bg-zinc-100">
        <BreadCrumb paths={[{ label: 'user', to: '/user' }]} />
        <div className="size-full p-4">
          <div className="size-full p-4 bg-white rounded-md flex flex-col">
            <div className="w-full h-auto flex lg:flex-row flex-col gap-4 justify-between items-center">
              <div className="w-full lg:w-1/4 flex items-center border px-3 py-0.5 relative rounded-md focus-within:border-primary transition-all">
                <Search size={20} />
                <Input
                  placeholder="Search something"
                  className="border-none shadow-none focus-visible:ring-0"
                />
              </div>
              <div className="w-full flex gap-4 lg:justify-between items-center">
                <Button
                  className="w-full lg:w-fit gap-1"
                  size={'lg'}
                  variant={'secondary'}>
                  <Filter size={20} />
                  Filter
                </Button>
                <UserForm />
              </div>
            </div>
            <div className="size-full">
              <UserList
                users={users?.contents}
                isFetching={isFetching}
                isLoading={isLoading}
              />
            </div>
            <PaginationCustom
              currentPage={currentPage}
              onPageChange={setCurrentPage}
              totalPages={users?.totalPages}
            />
          </div>
        </div>
      </div>
    </>
  )
}

export default Index
