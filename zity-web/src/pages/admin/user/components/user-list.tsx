import { Badge } from '@/components/ui/badge'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table'
import UserDetail from './user-detail'
import { useState } from 'react'
import { z } from 'zod'
import { UserPartialSchema } from '@/schema/user.validate'

interface UserListProps {
  users: z.infer<typeof UserPartialSchema>[]
}

const UserList = ({ users }: UserListProps) => {
  const [showDetail, setShowDetail] = useState<z.infer<
    typeof UserPartialSchema
  > | null>(null)

  return (
    <>
      <Table className="mt-4">
        <TableHeader>
          <TableRow>
            <TableHead className="">ID</TableHead>
            <TableHead className="sm:w-1/2">Username - Apartment</TableHead>
            <TableHead>Phone</TableHead>
            <TableHead>Account Type</TableHead>
            <TableHead>Is Staying</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {users.map((user) => (
            <TableRow
              key={user?.id}
              className="font-medium cursor-pointer"
              onClick={() => setShowDetail(user)}>
              <TableCell>{user?.id}</TableCell>
              <TableCell className="">
                <div className="w-full flex items-center gap-3">
                  <img
                    src={user?.avatar}
                    alt="user avatar"
                    className="size-12 rounded-full object-cover hidden sm:inline-block"
                  />
                  <div className="flex flex-col">
                    <p className="">{user?.full_name}</p>
                  </div>
                </div>
              </TableCell>
              <TableCell>{user?.phone ?? ''.slice(0, -4) + '****'}</TableCell>
              <TableCell>
                <Badge
                  variant={`${
                    user?.user_type?.includes('ADMIN') ? 'warning' : 'info'
                  }`}>
                  {user?.user_type}
                </Badge>
              </TableCell>
              <TableCell className="uppercase">
                <Badge
                  variant={`${
                    user?.is_staying === true ? 'success' : 'error'
                  }`}>
                  {user?.is_staying ? 'True' : 'False'}
                </Badge>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
      {showDetail && (
        <UserDetail user={showDetail} setShowDetail={setShowDetail} />
      )}
    </>
  )
}

export default UserList
