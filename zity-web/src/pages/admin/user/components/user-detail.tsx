import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar'
import { Button } from '@/components/ui/button'
import { X } from 'lucide-react'
import GridWallpaper from '@/assets/grid-wallpaper.jpg'
import { Separator } from '@/components/ui/separator'
import { Badge } from '@/components/ui/badge'
import { z } from 'zod'
import { UserPartialSchema } from '@/schema/user.validate'
import AlertDelete from '@/components/alert/AlertDelete'

interface UserDetailProps {
  user: z.infer<typeof UserPartialSchema> | null
  setShowDetail: (user: z.infer<typeof UserPartialSchema> | null) => void
}

const UserDetail = ({ user, setShowDetail }: UserDetailProps) => {
  const setAction = () => {
    console.log('delete')
  }

  return (
    <div
      className={`fixed top-0 ${
        user ? 'right-0 animate-in fade-in-0 duration-300' : 'right-full'
      } w-full h-screen z-50 flex justify-end`}>
      <div
        onClick={() => setShowDetail(null)}
        className="w-full h-screen absolute inset-0 bg-black/10"></div>
      <div className="z-[99] w-3/4 md:w-1/3 h-full animate-in slide-in-from-right-10 duration-500 relative bg-white border-l border-gray-300 px-6 pb-6 pt-10 md:pb-10 md:pt-24">
        <img
          src={GridWallpaper}
          alt="grid wallpaper"
          className="w-full h-[180px] object-cover absolute inset-0 border-b-4 border-white"
        />
        <Button
          className="absolute top-3 right-3"
          size={'icon'}
          onClick={() => setShowDetail(null)}
          variant={'ghost'}>
          <X />
        </Button>
        <div className="size-full flex flex-col gap-2.5">
          <Avatar className="size-32 border-4 border-white shadow-lg">
            <AvatarImage src={user?.avatar} alt="User avatar" />
            <AvatarFallback className="text-lg">
              {user?.full_name ??
                ''
                  .split(' ')
                  .map((item) => item.charAt(0))
                  .join('')}
            </AvatarFallback>
          </Avatar>
          <p className="text-lg font-medium">{user?.full_name}</p>
          <div className="w-full h-fit flex gap-2 font-medium text-sm">
            <span>National ID</span>
            <Separator orientation="vertical" />
            <span>Phone number</span>
            <Separator orientation="vertical" />
            <span>Gender</span>
            <Separator orientation="vertical" />
            <span>Date of birth</span>
          </div>
          <div className="flex gap-2 uppercase">
            <Badge
              variant={`${
                Array.isArray(user?.user_type)
                  ? 'info'
                  : user?.user_type === 'ADMIN'
                  ? 'success'
                  : 'info'
              }`}>
              {user?.user_type}
            </Badge>
          </div>
          <p className="text-gray-500 font-medium text-sm">
            Account Information
          </p>
          <p className="text-sm font-medium text-gray-500">
            Username:{' '}
            <span className="text-black">
              {'username123131'.slice(0, -4) + '****'}
            </span>
          </p>
          <p className="text-sm font-medium text-gray-500">
            Password: <span className="text-black">************</span>
          </p>
          <div className="text-sm font-medium text-gray-500">
            First Login:{' '}
            <Badge className="uppercase" variant={'error'}>
              False
            </Badge>
          </div>
          <div className="text-sm font-medium text-gray-500">
            Is Staying:{' '}
            <Badge className="uppercase" variant={'success'}>
              True
            </Badge>
          </div>
          <div className="w-full h-full flex items-end gap-2">
            <Button type="button" variant={'warning'}>
              Edit
            </Button>
            <AlertDelete description="user" setAction={setAction} />
          </div>
        </div>
      </div>
    </div>
  )
}

export default UserDetail
