import { useState } from 'react'
import Overlay from '@/components/overlay/Overlay'
import { Button } from '@/components/ui/button'
import { Checkbox } from '@/components/ui/checkbox'
import { Input } from '@/components/ui/input'
import { Separator } from '@/components/ui/separator'
import {
  useCreateRelationshipMutation,
  useDeleteRelationshipMutation,
} from '@/features/relationships/relationshipsSlice'
import { useGetUsersQuery } from '@/features/user/userSlice'
import { ExtendedRelationshipsSchema } from '@/schema/relationship.validate'
import { Loader2, Trash2, X } from 'lucide-react'
import { useDebounceCallback } from 'usehooks-ts'
import { z } from 'zod'
import { ApartmentUserRole } from '@/enums'
import { Label } from '@/components/ui/label'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'
import { toast } from 'sonner'

interface IApartmentDetailAddPeopleProps {
  relationships?: z.infer<typeof ExtendedRelationshipsSchema>[]
  setAddPeople: (value: boolean) => void
  apartmentId?: string
}

const ApartmentDetailAddPeople = ({
  relationships = [],
  setAddPeople,
  apartmentId,
}: IApartmentDetailAddPeopleProps) => {
  const [addPeople, { isLoading: isCreatePeople }] = useCreateRelationshipMutation()
  const [deleteRelationship] = useDeleteRelationshipMutation()
  const [search, setSearch] = useState<string>('')
  const [selectedUsers, setSelectedUsers] = useState<{ userId: number; role: ApartmentUserRole }[]>(
    [],
  )
  const debounced = useDebounceCallback(setSearch, 500)
  const {
    data: users,
    isLoading,
    isFetching,
  } = useGetUsersQuery({ page: 1, pageSize: 60, id: parseInt(search) })

  // Check if a user is already in relationships
  const isUserInRelationships = (userId: number) => {
    return relationships.some((relationship) => relationship.userId === userId)
  }

  // Get relationship ID for a user
  const getRelationshipId = (userId: number) => {
    return relationships.find((relationship) => relationship.userId === userId)?.id
  }

  const isOwnerInRelationships = () => {
    return relationships.some((relationship) => relationship.role === 'OWNER')
  }

  // Handle checkbox change
  const handleCheckboxChange = (userId: number, checked: boolean) => {
    if (isUserInRelationships(userId)) return // Prevent toggling existing relationships

    setSelectedUsers((prev) => {
      if (checked) {
        return [...prev, { userId, role: 'USER' }]
      } else {
        return prev.filter((user) => user.userId !== userId)
      }
    })
  }

  // Handle delete relationship
  const handleDeleteRelationship = async (userId: number) => {
    const relationshipId = getRelationshipId(userId)
    if (!relationshipId) return

    try {
      await deleteRelationship({ id: relationshipId, apartmentId: apartmentId })
        .unwrap()
        .then(() => {
          toast.success('Relationship deleted successfully')
        })
        .catch(() => {
          toast.error('Failed to delete relationship')
        })
    } catch (error) {
      console.error('Failed to delete relationship:', error)
      toast.error('Failed to delete relationship')
    }
  }

  // Handle role change
  const handleRoleChange = (userId: number, newRole: ApartmentUserRole) => {
    setSelectedUsers((prev) =>
      prev.map((user) => (user.userId === userId ? { ...user, role: newRole } : user)),
    )
  }

  // Check if user is selected
  const isUserSelected = (userId: number) => {
    return selectedUsers.some((user) => user.userId === userId)
  }

  // Get selected user's role
  const getUserRole = (userId: number) => {
    const existingRelationship = relationships.find((rel) => rel.userId === userId)
    if (existingRelationship) return existingRelationship.role
    return selectedUsers.find((user) => user.userId === userId)?.role || 'USER'
  }

  // Handle adding selected people
  const handleActionAddPeople = async () => {
    if (selectedUsers.some((user) => user.role === 'OWNER') && isOwnerInRelationships()) {
      toast.error('This apartment already has an owner.')
      return
    }
    try {
      if (selectedUsers.length === 0) return

      const promises = selectedUsers.map((user) =>
        addPeople({
          apartmentId: apartmentId,
          userId: user.userId,
          role: user.role,
        }).unwrap(),
      )

      await Promise.all(promises)
      toast.success('Add people to apartment successfully')
      // setAddPeople(false)
    } catch (error) {
      console.error('Failed to add people:', error)
    }
  }

  return (
    <Overlay>
      <div className="h-[500px] min-w-[600px] max-w-2xl bg-white rounded-md flex flex-col overflow-hidden">
        <div className="w-full flex justify-between items-center p-4">
          <p className="font-medium">Add new people to APARTMENT</p>
          <X className="cursor-pointer" onClick={() => setAddPeople(false)} />
        </div>
        <Separator />
        <div className="flex flex-col overflow-hidden">
          <div className="sticky top-0 z-10 p-4 bg-white">
            <Input
              placeholder="Search user..."
              defaultValue={search}
              onChange={(e) => debounced(e.target.value)}
              type="search"
            />
          </div>
          <Separator className="sticky top-[68px]" />
          {isLoading || isFetching ? (
            <div className="size-full h-[306px] flex justify-center items-center">
              <Loader2 size={14} className="text-primary animate-spin" />
            </div>
          ) : (
            <div className="flex flex-col space-y-4 px-4 py-2 overflow-y-auto">
              {users &&
                users.contents.map((user, index) => (
                  <div
                    key={user.id || index}
                    className="flex items-center justify-between gap-2 pr-4">
                    <Label className="flex items-center gap-2">
                      <div className="flex items-center gap-2">
                        <Checkbox
                          checked={isUserInRelationships(user.id) || isUserSelected(user.id)}
                          disabled={isUserInRelationships(user.id)}
                          onCheckedChange={(checked) =>
                            handleCheckboxChange(user.id, checked as boolean)
                          }
                        />
                        {isUserInRelationships(user.id) && (
                          <Button
                            onClick={() => handleDeleteRelationship(user.id)}
                            type="button"
                            size={'icon'}
                            variant={'ghost'}>
                            <Trash2
                              size={16}
                              className="text-red-500 cursor-pointer hover:text-red-600"
                            />
                          </Button>
                        )}
                      </div>
                      <span>
                        {user.id} - {user.fullName}
                      </span>
                    </Label>
                    <Select
                      value={getUserRole(user.id)}
                      disabled={isUserInRelationships(user.id)}
                      onValueChange={(value) =>
                        handleRoleChange(user.id, value as ApartmentUserRole)
                      }>
                      <SelectTrigger className="w-32">
                        <SelectValue />
                      </SelectTrigger>
                      <SelectContent>
                        <SelectItem value="USER">User</SelectItem>
                        <SelectItem value="OWNER">Owner</SelectItem>
                      </SelectContent>
                    </Select>
                  </div>
                ))}
            </div>
          )}
          <div className="p-4">
            <Button
              className="w-full"
              type="button"
              onClick={handleActionAddPeople}
              disabled={selectedUsers.length === 0 || isCreatePeople}>
              {isCreatePeople ? (
                <Loader2 size={14} className="animate-spin" />
              ) : (
                `Add ${selectedUsers.length ? `(${selectedUsers.length})` : ''}`
              )}
            </Button>
          </div>
        </div>
      </div>
    </Overlay>
  )
}

export default ApartmentDetailAddPeople
