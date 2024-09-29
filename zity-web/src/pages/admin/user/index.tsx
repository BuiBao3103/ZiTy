import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbSeparator,
} from '@/components/ui/breadcrumb'
import {
  Pagination,
  PaginationContent,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from '@/components/ui/pagination'

import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { UserPartialSchema } from '@/types'
import { Filter, Search } from 'lucide-react'
import { Link } from 'react-router-dom'
import UserList from './components/user-list'
import { useDocumentTitle } from 'usehooks-ts'
import UserForm from './components/user-form'
import { z } from 'zod'

const Index = () => {
  useDocumentTitle('User')
  const users: z.infer<typeof UserPartialSchema>[] = [
    {
      id: 1,
      full_name: 'John Doe',
      phone: '0123456789',
      user_type: 'RESIDENT',
      is_staying: true,
      avatar: 'https://picsum.photos/id/2/200/300',
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
            </BreadcrumbList>
          </Breadcrumb>
        </div>
        <div className="size-full p-4">
          <div className="size-full p-4 bg-white rounded-md">
            <div className="w-full h-auto flex justify-between items-center">
              <div className="w-full flex gap-4 items-center">
                <div className="lg:w-1/4 flex items-center border px-3 py-0.5 relative rounded-md">
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
              <UserForm />
            </div>
            <UserList users={users} />
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
