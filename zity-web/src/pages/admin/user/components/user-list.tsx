import { Badge } from '@/components/ui/badge'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table'
import { User } from '@/types'
import UserDetail from './user-detail'
import { useState } from 'react'

interface UserListProps {
  users: User[]
}

const UserList = ({ users }: UserListProps) => {
  const [showDetail, setShowDetail] = useState<User | null>(null)

  return (
    <>
      <Table className="mt-4">
        <TableHeader>
          <TableRow>
            <TableHead className="">ID</TableHead>
            <TableHead className="sm:w-1/2">Username - Apartment</TableHead>
            <TableHead>Phone</TableHead>
            <TableHead>Account Type</TableHead>
            <TableHead>Status</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {users.map((user) => (
            <TableRow
              key={user.id}
              className="font-medium cursor-pointer"
              onClick={() => setShowDetail(user)}>
              <TableCell>{user.id}</TableCell>
              <TableCell className="">
                <div className="w-full flex gap-3">
                  <img
                    src={user.avatar}
                    alt="user avatar"
                    className="size-12 rounded-full object-cover hidden sm:inline-block"
                  />
                  <div className="flex flex-col">
                    <p className="">{user.name}</p>
                    <p className="text-xs">{user.room}</p>
                  </div>
                </div>
              </TableCell>
              <TableCell>{user.phoneNumber.slice(0, -4) + '****'}</TableCell>
              <TableCell>
                <Badge
                  variant={`${
                    user.type === 'OWNER'
                      ? 'success'
                      : user.type === 'RESIDENT'
                      ? 'info'
                      : 'destructive'
                  }`}>
                  {user.type}
                </Badge>
              </TableCell>
              <TableCell className="uppercase">
                <Badge
                  variant={`${
                    user.status === 'active'
                      ? 'success'
                      : user.status === 'pending'
                      ? 'warning'
                      : 'error'
                  }`}>
                  {user.status}
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
