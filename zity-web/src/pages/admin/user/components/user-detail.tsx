import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar'
import { Button } from '@/components/ui/button'
import { X } from 'lucide-react'
import GridWallpaper from '@/assets/grid-wallpaper.jpg'
import { Separator } from '@/components/ui/separator'
import { Badge } from '@/components/ui/badge'
import { z } from 'zod'
import { UserPartialSchema } from '@/schema/user.validate'
import AlertDelete from '@/components/alert/AlertDelete'
import { useAppDispath } from '@/store'
import { editingUser } from '@/features/user/userSlice'
import { useForm } from 'react-hook-form'
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
} from '@/components/ui/form'
import { Input } from '@/components/ui/input'

interface UserDetailProps {
  user: z.infer<typeof UserPartialSchema> | null
  setShowDetail: (user: z.infer<typeof UserPartialSchema> | null) => void
}

const UserDetail = ({ user, setShowDetail }: UserDetailProps) => {
  const dispatch = useAppDispath()
  const form = useForm<z.infer<typeof UserPartialSchema>>({
    defaultValues: {},
  })
  const setAction = () => {
    console.log('delete')
  }

  const onSubmit = async (data: any) => {
    console.log(data)
  }

  return (
    <div
      className={`fixed top-0 ${
        user ? 'right-0 animate-in fade-in-0 duration-300' : 'right-full'
      } w-full h-screen z-50 flex justify-center items-center p-4`}>
      <div
        onClick={() => setShowDetail(null)}
        className="w-full h-screen absolute animate-in fade-in inset-0 bg-gradient-to-b from-black/20 to-black/60"></div>
      <Form {...form}>
        <form
          onClick={() => form.handleSubmit(onSubmit)}
          className="z-[99] min-[500px]:max-w-lg lg:max-w-2xl border-4 border-white/80 w-full animate-in slide-in-from-bottom-1 duration-500 relative bg-white rounded-lg overflow-hidden shadow-lg">
          <img
            src={GridWallpaper}
            alt="grid wallpaper"
            className="w-full h-[90px] object-cover absolute inset-0 border-b-4 border-white"
          />
          <Button
            className="absolute top-3 right-3"
            size={'icon'}
            onClick={() => setShowDetail(null)}
            variant={'ghost'}>
            <X />
          </Button>
          <div className="size-full flex flex-col space-y-4 px-4 pb-4 pt-10">
            <Avatar className="size-28 border-4 border-white shadow-lg">
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
            <div className="w-full h-5 flex gap-2 font-medium text-sm">
              <span>National ID</span>
              <Separator orientation='vertical' />
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
            <FormField
              control={form.control}
              name="username"
              render={({ field }) => (
                <FormItem className="flex gap-4 items-start space-y-0">
                  <FormLabel className="sm:w-[120px]">Username</FormLabel>
                  <FormControl>
                    <Input placeholder="Enter username..." {...field} />
                  </FormControl>
                </FormItem>
              )}
            />
            <Separator className="my-2" />
            <FormField
              control={form.control}
              name="password"
              render={({ field }) => (
                <FormItem className="flex gap-4 items-start space-y-0">
                  <FormLabel className="sm:w-[120px]">Password</FormLabel>
                  <FormControl>
                    <Input placeholder="Enter password..." {...field} />
                  </FormControl>
                </FormItem>
              )}
            />
            <Separator className="my-2" />
            <FormField
              control={form.control}
              name="avatar"
              render={({ field }) => (
                <FormItem className="flex gap-4 items-start space-y-0">
                  <FormLabel className="sm:w-[120px]">Avatar</FormLabel>
                  <FormControl>
                    <div className="flex gap-4">
                      <img
                        src={user?.avatar}
                        className="size-16 min-[450px]:size-20 rounded-full"
                        alt=""
                      />
                      <Button
                        type="button"
                        className="relative"
                        variant={'secondary'}>
                        Click here to replace
                        <Input
                          type="file"
                          accept="image/*"
                          className="absolute size-full inset-0 opacity-0"
                          {...field}
                        />
                      </Button>
                    </div>
                  </FormControl>
                </FormItem>
              )}
            />
            {/* <div className="text-sm font-medium text-gray-500">
              Username:{' '}
              <span className="text-black">
                {'username1234'.slice(0, -4) + '****'}
              </span>
            </div>
            <div className="text-sm font-medium text-gray-500 flex items-start">
              Password:
            </div> */}
            {/* <div className="text-sm font-medium text-gray-500">
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
          </div> */}
          </div>
          <Separator />
          <div className="w-full h-full flex justify-between items-center p-4">
            <AlertDelete description="user" setAction={setAction} />
            <div className="flex gap-2">
              <Button
                type="button"
                variant={'ghost'}
                onClick={() => dispatch(editingUser({ isEditingUser: false }))}>
                Cancel
              </Button>
              <Button
                type="submit"
                variant={'info'}
                onClick={() => dispatch(editingUser({ isEditingUser: false }))}>
                Submit
              </Button>
            </div>
          </div>
        </form>
      </Form>
    </div>
  )
}

export default UserDetail
