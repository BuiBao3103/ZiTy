import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbPage,
  BreadcrumbSeparator,
} from '@/components/ui/breadcrumb'
import {
  Pagination,
  PaginationContent,
  PaginationEllipsis,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from '@/components/ui/pagination'

import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { User } from '@/types'
import { Filter, Search } from 'lucide-react'
import { Link } from 'react-router-dom'
import UserList from './components/user-list'
import { useDocumentTitle } from 'usehooks-ts'

const Index = () => {
  useDocumentTitle('User')


  const users: User[] = [
    {
      id: 1,
      avatar: 'https://picsum.photos/id/1/200/300',
      name: 'Alice Johnson',
      room: 'Room B',
      phoneNumber: '+1234567891',
      type: 'OWNER',
      status: 'active',
    },
    {
      id: 2,
      avatar: 'https://picsum.photos/id/2/200/300',
      name: 'Bob Smith',
      room: 'Room C',
      phoneNumber: '+1234567892',
      type: 'RESIDENT',
      status: 'inactive',
    },
    {
      id: 3,
      avatar: 'https://picsum.photos/id/3/200/300',
      name: 'Charlie Davis',
      room: 'Room A',
      phoneNumber: '+1234567893',
      type: 'RESIDENT',
      status: 'pending',
    },
    {
      id: 4,
      avatar: 'https://picsum.photos/id/4/200/300',
      name: 'Diana Evans',
      room: 'Room D',
      phoneNumber: '+1234567894',
      type: 'RESIDENT',
      status: 'active',
    },
    {
      id: 5,
      avatar: 'https://picsum.photos/id/5/200/300',
      name: 'Ethan Ford',
      room: 'Room E',
      phoneNumber: '+1234567895',
      type: 'OWNER',
      status: 'inactive',
    },
    {
      id: 6,
      avatar: 'https://picsum.photos/id/6/200/300',
      name: 'Fiona Green',
      room: 'Room F',
      phoneNumber: '+1234567896',
      type: 'OWNER',
      status: 'active',
    },
    {
      id: 7,
      avatar: 'https://picsum.photos/id/7/200/300',
      name: 'George Hall',
      room: 'Room G',
      phoneNumber: '+1234567897',
      type: 'RESIDENT',
      status: 'inactive',
    },
    {
      id: 8,
      avatar: 'https://picsum.photos/id/8/200/300',
      name: 'Hannah White',
      room: 'Room H',
      phoneNumber: '+1234567898',
      type: 'RESIDENT',
      status: 'pending',
    },
    {
      id: 9,
      avatar: 'https://picsum.photos/id/9/200/300',
      name: 'Isaac King',
      room: 'Room I',
      phoneNumber: '+1234567899',
      type: 'RESIDENT',
      status: 'active',
    },
    {
      id: 10,
      avatar: 'https://picsum.photos/id/10/200/300',
      name: 'Jack Lee',
      room: 'Room J',
      phoneNumber: '+1234567800',
      type: 'RESIDENT',
      status: 'inactive',
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
              <Button variant={'default'} size={'lg'}>
                New User
              </Button>
            </div>
            <UserList users={users} />
            <Pagination>
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
