import { Button } from '@/components/ui/button'
import {
  Dialog,
  DialogClose,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from '@/components/ui/dialog'

import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
} from '@/components/ui/form'
import { Input } from '@/components/ui/input'
import { Separator } from '@/components/ui/separator'
import { FieldErrors, useForm } from 'react-hook-form'
import { z } from 'zod'
import { Textarea } from '@/components/ui/textarea'
import { toast } from 'sonner'
import { zodResolver } from '@hookform/resolvers/zod'
import { PackageSchema } from '@/schema/package.validate'
import { PlusCircle, X } from 'lucide-react'
import React, { useState } from 'react'

interface PackageFormProps {
  children: React.ReactNode
  id?: string
}

const PackageForm = ({ children, id }: PackageFormProps) => {
  const [selectedImage, setSelectedImage] = useState<string | null>(null)

  const form = useForm<z.infer<typeof PackageSchema>>({
    mode: 'onSubmit',
    defaultValues: {
      description: '',
      image: '',
      isReceived: false,
    },
    resolver: zodResolver(PackageSchema),
  })

  const onSubmit = async (data: z.infer<typeof PackageSchema>) => {
    console.log(data)
  }

  const onError = (errors: FieldErrors<z.infer<typeof PackageSchema>>) => {
    console.log(errors)
    // if (error['name']) {
    //   toast.error(error['']?.message)
    //   return
    // }
    // if (error['description']) {
    //   toast.error(error['description']?.message)
    //   return
    // }
    // if (error['price']) {
    //   toast.error(error['price']?.message)
    //   return
    // }
  }

  // Handle image selection
  const handleImageChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const files = e.target.files
    if (!files) return
    const file = files[0]
    if (file) {
      setSelectedImage(URL.createObjectURL(file))
    }
  }

  // Handle image removal
  const removeImage = () => {
    setSelectedImage(null)
  }

  return (
    <Dialog>
      <DialogTrigger asChild>{children}</DialogTrigger>
      <DialogContent
        className="max-w-sm lg:max-w-lg xl:max-w-xl"
        aria-describedby={undefined}>
        <DialogHeader>
          <DialogTitle className="text-2xl">
            {id ? `Package #123` : 'New Package'}
          </DialogTitle>
        </DialogHeader>
        <Separator />
        <Form {...form}>
          <form
            onSubmit={form.handleSubmit(onSubmit, onError)}
            className="space-y-4">
            <div className="w-full flex justify-center items-center gap-4">
              <div className="w-full space-y-4">
                <FormField
                  control={form.control}
                  name="description"
                  render={({ field }) => (
                    <FormItem className="w-full">
                      <FormLabel>Description</FormLabel>
                      <FormControl>
                        <Textarea
                          {...field}
                          placeholder="Type something"></Textarea>
                      </FormControl>
                    </FormItem>
                  )}
                />
                <FormField
                  control={form.control}
                  name="description"
                  render={({ field }) => (
                    <FormItem className="w-full">
                      <FormLabel>Description</FormLabel>
                      <FormControl>
                        <Textarea
                          {...field}
                          placeholder="Type something"></Textarea>
                      </FormControl>
                    </FormItem>
                  )}
                />
                <FormField
                  control={form.control}
                  name="isReceived"
                  render={({ field }) => (
                    <FormItem className="w-full">
                      <FormLabel>Status</FormLabel>
                      <FormControl>
                        <Input
                          type="text"
                          readOnly
                          value={field.value ? 'Collected' : 'Not Collected'}
                          className="bg-gray-300"
                        />
                      </FormControl>
                    </FormItem>
                  )}
                />
              </div>
              <div className="w-full h-full">
                <FormField
                  control={form.control}
                  name="image"
                  render={({ field }) => (
                    <FormItem className="w-full h-full">
                      <FormLabel>Image</FormLabel>
                      <FormControl>
                        <div className="w-full h-[300px] border-2 rounded-md relative flex flex-col justify-center items-center gap-2">
                          {selectedImage ? (
                            <>
                              <img
                                src={selectedImage}
                                loading="lazy"
                                alt="Preview"
                                className="w-full h-full object-cover rounded-md"
                              />
                              <Button
                                size={'icon'}
                                type="button"
                                onClick={removeImage}
                                variant={'destructive'}
                                className="absolute top-2 right-2 z-10">
                                <X />
                              </Button>
                            </>
                          ) : (
                            <>
                              <span className="text-zinc-400 font-medium">
                                Add image{' '}
                              </span>
                              <PlusCircle size={35} className="text-zinc-400" />
                            </>
                          )}
                          <Input
                            type="file"
                            accept="image/*"
                            className="absolute size-full opacity-0 cursor-pointer"
                            placeholder="Type something"
                            {...field}
                            onChange={(e) => {
                              field.onChange(e)
                              handleImageChange(e)
                            }}
                          />
                        </div>
                      </FormControl>
                    </FormItem>
                  )}
                />
              </div>
            </div>
            <div className="w-full flex justify-end gap-4">
              <DialogClose asChild>
                <Button type="button" size={'lg'} variant={'ghost'}>
                  Cancel
                </Button>
              </DialogClose>
              <Button type="submit" size={'lg'} variant={'default'}>
                Confirm
              </Button>
            </div>
          </form>
        </Form>
      </DialogContent>
    </Dialog>
  )
}

export default PackageForm
