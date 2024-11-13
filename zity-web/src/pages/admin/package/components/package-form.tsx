import { Button } from '@/components/ui/button'
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
} from '@/components/ui/form'
import { Input } from '@/components/ui/input'
import { FieldErrors, useForm } from 'react-hook-form'
import { z } from 'zod'
import { Textarea } from '@/components/ui/textarea'
import { toast } from 'sonner'
import { zodResolver } from '@hookform/resolvers/zod'
import { IPackage, PackageSchema } from '@/schema/package.validate'
import { PlusCircle, X } from 'lucide-react'
import React, { useEffect, useState } from 'react'
import {
  useCreatePackageMutation,
  useUpdatePackageMutation,
} from '@/features/package/packageSlice'

interface PackageFormProps {
  packagee?: IPackage
  setOpen: (value: boolean) => void
}

const PackageForm = ({ packagee, setOpen }: PackageFormProps) => {
  const [selectedImage, setSelectedImage] = useState<string | null>(null)
  const [createPackage, { isLoading }] = useCreatePackageMutation()
  const [updatePackage, { isLoading: isUpdating }] = useUpdatePackageMutation()

  const form = useForm<z.infer<typeof PackageSchema>>({
    mode: 'onSubmit',
    defaultValues: packagee || {
      description: '',
      image: '',
      isReceived: false,
    },
    resolver: zodResolver(PackageSchema),
  })

  const onSubmit = async (data: z.infer<typeof PackageSchema>) => {
    const newData = {
      image: data.image,
      description: data.description,
      isReceived: data.isReceived,
    }
    if (packagee) {
      await updatePackage({ id: packagee.id, body: newData })
        .unwrap()
        .then(() => {
          toast.success('Package updated successfully')
          setOpen(false)
        })
        .catch((error) => {
          console.log(error)
        })
    } else {
      await createPackage(data)
        .unwrap()
        .then(() => {
          toast.success('Package created successfully')
          setOpen(false)
        })
        .catch((error) => {
          console.log(error)
          toast.error('Something went wrong')
        })
    }
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

  useEffect(() => {
    if (packagee) {
      form.reset(packagee)
    }
  }, [])

  return (
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
          <Button
            type="button"
            size={'lg'}
            variant={'ghost'}
            onClick={() => setOpen(false)}>
            Cancel
          </Button>
          <Button type="submit" size={'lg'} variant={'default'}>
            {isLoading || isUpdating ? 'Submitting...' : 'Submit'}
          </Button>
        </div>
      </form>
    </Form>
  )
}

export default PackageForm
