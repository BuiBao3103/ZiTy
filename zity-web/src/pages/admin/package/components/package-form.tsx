import { Button } from '@/components/ui/button'
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from '@/components/ui/form'
import { Input } from '@/components/ui/input'
import { useForm } from 'react-hook-form'
import { z } from 'zod'
import { Textarea } from '@/components/ui/textarea'
import { toast } from 'sonner'
import { zodResolver } from '@hookform/resolvers/zod'
import { IPackage, PackageSchema } from '@/schema/package.validate'
import { PlusCircle, X } from 'lucide-react'
import React, { useEffect, useState } from 'react'
import {
  useCreatePackageMutation,
  useUpdateImagePackageMutation,
  useUpdatePackageMutation,
} from '@/features/package/packageSlice'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'

interface PackageFormProps {
  packagee?: IPackage
  setOpen: (value: number | undefined) => void
}

const PackageForm = ({ packagee, setOpen }: PackageFormProps) => {
  const [selectedImage, setSelectedImage] = useState<string | null>(
    typeof packagee?.image === 'string' ? packagee.image : null,
  )
  const [createPackage, { isLoading }] = useCreatePackageMutation()
  const [updatePackage, { isLoading: isUpdating }] = useUpdatePackageMutation()
  const [updateImagePackage, { isLoading: isUpdatingImage }] =
    useUpdateImagePackageMutation()

  const form = useForm<z.infer<typeof PackageSchema>>({
    mode: 'onSubmit',
    defaultValues: packagee || {
      description: '',
      image: undefined,
      isReceive: false,
    },
    resolver: zodResolver(PackageSchema),
  })

  const onSubmit = async (data: z.infer<typeof PackageSchema>) => {
    try {
      const newData = {
        description: data.description,
        isReceive: data.isReceive,
      }
      if (packagee) {
        // Update existing package
        const updatePromises = []

        // Add package data update promise
        updatePromises.push(
          updatePackage({ id: packagee.id, body: newData }).unwrap(),
        )
        // Add image update promise if there's a new image
        if (typeof data.image !== 'string') {
          const formData = new FormData()
          formData.append('file', data.image)
          updatePromises.push(
            updateImagePackage({ id: packagee.id, image: formData }).unwrap(),
          )
        }

        // Wait for all updates to complete
        await Promise.all(updatePromises)
        toast.success('Package updated successfully')
        setOpen(undefined)
      } else {
        await createPackage(data).unwrap()
        toast.success('Package created successfully')
        setOpen(undefined)
      }
    } catch (error) {
      console.error(error)
      toast.error('Something went wrong')
    }
  }

  // Handle image selection
  const handleImageChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const files = e.target.files
    if (!files) return
    const file = files[0]

    // Validate file type
    const validTypes = ['image/jpeg', 'image/png', 'image/gif', 'image/webp']
    if (!validTypes.includes(file.type)) {
      toast.error('Please upload a valid image file (JPEG, PNG, GIF, or WebP)')
      return
    }

    // Validate file size (5MB)
    const maxSize = 5 * 1024 * 1024
    if (file.size > maxSize) {
      toast.error('File size must be less than 5MB')
      return
    }

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
        encType="multipart/form-data"
        onSubmit={form.handleSubmit(onSubmit)}
        className="space-y-4">
        <div className="w-full flex justify-center gap-4">
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
                      className="max-h-40"
                      placeholder="Type something"></Textarea>
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="isReceive"
              render={({ field }) => (
                <FormItem className="w-full">
                  <FormLabel>Status</FormLabel>
                  <Select
                    disabled={String(field.value) === 'true' ? true : false}
                    onValueChange={(value) => {
                      // Convert string value to boolean
                      field.onChange(value === 'true')
                    }}
                    value={String(field.value)} // Convert boolean to string for Select
                  >
                    <FormControl>
                      <SelectTrigger>
                        <SelectValue placeholder="Select package status" />
                      </SelectTrigger>
                    </FormControl>
                    <SelectContent>
                      <SelectItem value="true">Collected</SelectItem>
                      <SelectItem value="false">Not Collected</SelectItem>
                    </SelectContent>
                  </Select>
                  <FormMessage />
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
                            className="w-full h-full object-center object-contain rounded-md relative z-10"
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
                        className="absolute size-full opacity-0 cursor-pointer object-center"
                        placeholder="Type something"
                        onChange={(e) => {
                          if (e.target.files) {
                            field.onChange(e.target.files[0])
                          }
                          handleImageChange(e)
                        }}
                        onBlur={field.onBlur}
                        name={field.name}
                        ref={field.ref}
                      />
                    </div>
                  </FormControl>
                  <FormMessage />
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
            onClick={() => setOpen(undefined)}>
            Cancel
          </Button>
          <Button type="submit" size={'lg'} variant={'default'}>
            {isLoading || isUpdating || isUpdatingImage
              ? 'Submitting...'
              : 'Submit'}
          </Button>
        </div>
      </form>
    </Form>
  )
}

export default PackageForm
