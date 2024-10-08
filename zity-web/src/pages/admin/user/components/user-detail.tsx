import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar'
import { Button } from '@/components/ui/button'
import { CalendarIcon, X } from 'lucide-react'
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
import { format } from 'date-fns'
import { cn } from '@/lib/utils'
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from '@/components/ui/popover'
import { Calendar } from '@/components/ui/calendar'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'
import { useState } from 'react'

interface UserDetailProps {
  user: z.infer<typeof UserPartialSchema> | null
  setShowDetail: (user: z.infer<typeof UserPartialSchema> | null) => void
}

const UserDetail = ({ user, setShowDetail }: UserDetailProps) => {
  const dispatch = useAppDispath()
  const [selectedImage, setSelectedImage] = useState<string | undefined>(
    undefined,
  )
  const form = useForm<z.infer<typeof UserPartialSchema>>({
    defaultValues: {
      id: user?.id,
      username: user?.username,
      password: user?.password,
      avatar: user?.avatar,
      is_first_login: user?.is_first_login,
      email: user?.email,
      phone: user?.phone,
      date_of_birth: user?.date_of_birth,
      full_name: user?.full_name,
      user_type: user?.user_type,
      nation_id: user?.nation_id,
      gender: user?.gender,
      is_staying: user?.is_staying,
    },
  })
  const setAction = () => {
    console.log('delete')
  }

  const onSubmit = async (data: any) => {
    console.log(data)
  }

  return (
    <div
      className={`fixed top-0 left-0 w-full h-screen z-50 flex justify-center items-center p-4`}>
      <div
        onClick={() => setShowDetail(null)}
        className="w-full h-screen absolute animate-in fade-in inset-0 bg-gradient-to-b from-black/20 to-black/60"></div>
      <Form {...form}>
        <form
          onClick={() => form.handleSubmit(onSubmit)}
          className="z-[99] min-[500px]:max-w-lg lg:max-w-2xl border-4 border-white/80 w-full animate-in fade-in slide-in-from-bottom-2 duration-300 relative bg-white rounded-lg overflow-hidden shadow-lg">
          <img
            src={GridWallpaper}
            alt="grid wallpaper"
            className="w-full h-[140px] object-cover absolute inset-0 border-b-4 border-white"
          />
          <Button
            className="absolute top-3 right-3"
            size={'icon'}
            onClick={() => setShowDetail(null)}
            variant={'ghost'}>
            <X />
          </Button>
          <div className="size-full flex flex-col space-y-4 px-4 pb-4 pt-10">
            <FormField
              control={form.control}
              name="avatar"
              render={({ field }) => (
                <FormItem className="w-full">
                  <FormLabel>Avatar</FormLabel>
                  <FormControl>
                    <div className="relative size-32 overflow-hidden rounded-full shadow-lg group/selectedImage">
                      {/* Display current image */}
                      <img
                        src={field.value ?? selectedImage}
                        alt="Avatar preview"
                        className="size-full object-cover border-4 rounded-full border-zinc-100"
                      />
                      {selectedImage && (
                        <div className="absolute size-full inset-0 flex justify-center items-center transition-all opacity-0 group-hover/selectedImage:opacity-100 group-hover/selectedImage:z-10 bg-gray-200">
                          <Button onClick={() => {
														form.setValue('avatar', undefined)
														setSelectedImage(undefined)
													}} type='button' size={'icon'} variant={"ghost"}>
                            <X />
                          </Button>
                        </div>
                      )}
                      {/* File upload option */}
                      <Input
                        type="file"
                        accept="image/*"
                        className="size-full absolute inset-0 opacity-0 cursor-pointer"
                        onChange={(e) => {
                          const file = e.target.files?.[0]
                          if (file) {
                            const reader = new FileReader()
                            reader.onload = () => {
                              if (typeof reader.result === 'string') {
                                form.setValue('avatar', reader.result) // Set image URL as base64 string
                                setSelectedImage(reader.result) // Set image URL as base64 string
                              }
                            }
                            reader.readAsDataURL(file)
                          }
                        }}
                      />
                    </div>
                  </FormControl>
                </FormItem>
              )}
            />
            <div className="uppercase">
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
            <FormField
              control={form.control}
              name="full_name"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Full name</FormLabel>
                  <FormControl>
                    <Input
                      placeholder="Type your full name"
                      {...field}
                      className="focus-visible:ring-primary"
                    />
                  </FormControl>
                </FormItem>
              )}
            />
            <div className="w-full flex flex-wrap gap-2">
              <FormField
                control={form.control}
                name="nation_id"
                render={({ field }) => (
                  <FormItem className="w-full flex-[1_1_150px]">
                    <FormLabel>National ID</FormLabel>
                    <FormControl>
                      <Input
                        placeholder="Type your national ID"
                        {...field}
                        type="number"
                        minLength={12}
                        maxLength={12}
                        className="focus-visible:ring-primary"
                      />
                    </FormControl>
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="gender"
                render={({ field }) => (
                  <FormItem className="w-full flex-[1_1_150px]">
                    <FormLabel>Gender</FormLabel>
                    <FormControl>
                      <Select
                        onValueChange={field.onChange}
                        defaultValue={field.value}>
                        <SelectTrigger>
                          <SelectValue placeholder="Select gender" />
                        </SelectTrigger>
                        <SelectContent>
                          <SelectItem value="MALE">Male</SelectItem>
                          <SelectItem value="FEMALE">Female</SelectItem>
                        </SelectContent>
                      </Select>
                    </FormControl>
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="date_of_birth"
                render={({ field }) => (
                  <FormItem className="w-full flex-[1_1_150px]">
                    <FormLabel>Date Of Birth</FormLabel>
                    <Popover>
                      <PopoverTrigger asChild>
                        <FormControl>
                          <Button
                            variant={'outline'}
                            className={cn(
                              'w-full pl-3 text-left font-normal',
                              !field.value && 'text-muted-foreground',
                            )}>
                            {field.value ? (
                              format(field.value, 'PPP')
                            ) : (
                              <span>Pick a date</span>
                            )}
                            <CalendarIcon className="ml-auto h-4 w-4 opacity-50" />
                          </Button>
                        </FormControl>
                      </PopoverTrigger>
                      <PopoverContent className="w-auto p-0" align="start">
                        <Calendar
                          mode="single"
													fromYear={1924}
													toYear={new Date().getFullYear()}
                          selected={field.value}
                          onSelect={field.onChange}
                          disabled={(date) =>
                            date > new Date() || date < new Date('1900-01-01')
                          }
                          initialFocus
                        />
                      </PopoverContent>
                    </Popover>
                  </FormItem>
                )}
              />
            </div>
            <p className="text-gray-500 font-medium text-sm">
              Account Information
            </p>
            <div className="w-full flex gap-4">
              <FormField
                control={form.control}
                name="email"
                render={({ field }) => (
                  <FormItem className="w-full">
                    <FormLabel>Email</FormLabel>
                    <FormControl>
                      <Input
                        placeholder="Enter email..."
                        type="email"
                        {...field}
                      />
                    </FormControl>
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="phone"
                render={({ field }) => (
                  <FormItem className="w-full">
                    <FormLabel>Phone</FormLabel>
                    <FormControl>
                      <Input
                        placeholder="Enter phone..."
                        type="tel"
                        {...field}
                      />
                    </FormControl>
                  </FormItem>
                )}
              />
            </div>
            <div className="w-full flex gap-4">
              <FormField
                control={form.control}
                name="username"
                render={({ field }) => (
                  <FormItem className="w-full">
                    <FormLabel>Username</FormLabel>
                    <FormControl>
                      <Input placeholder="Enter username..." {...field} />
                    </FormControl>
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="password"
                render={({ field }) => (
                  <FormItem className="w-full">
                    <FormLabel>Password</FormLabel>
                    <FormControl>
                      <Input
                        placeholder="Enter password..."
                        type="password"
                        {...field}
                      />
                    </FormControl>
                  </FormItem>
                )}
              />
            </div>
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
