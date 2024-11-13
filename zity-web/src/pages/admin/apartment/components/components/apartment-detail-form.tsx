import AlertDelete from '@/components/alert/AlertDelete'
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
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'
import { Textarea } from '@/components/ui/textarea'
import { ApartmentStatus } from '@/enums'
import {
  useDeleteApartmentMutation,
  useUpdateApartmentMutation,
} from '@/features/apartment/apartmentSlice'
import { ApartmentSchema } from '@/schema/apartment.validate'
import { zodResolver } from '@hookform/resolvers/zod'
import { Loader } from 'lucide-react'
import { useForm } from 'react-hook-form'
import { useNavigate } from 'react-router-dom'
import { toast } from 'sonner'
import { z } from 'zod'

interface IApartmentFormDetailProps {
  apartment?: z.infer<typeof ApartmentSchema>
}

const ApartmentFormDetail = ({ apartment }: IApartmentFormDetailProps) => {
  const navigate = useNavigate()
  const form = useForm<z.infer<typeof ApartmentSchema>>({
    defaultValues: apartment,
    resolver: zodResolver(ApartmentSchema),
  })
  const [updateApartment, { isLoading }] = useUpdateApartmentMutation()
  const [deleteApartment, { isLoading: isLoadingDelete }] =
    useDeleteApartmentMutation()
  const onSubmit = async (data: z.infer<typeof ApartmentSchema>) => {
    try {
      const result = await updateApartment({ id: data.id, body: data })
      if (result.error) {
        throw new Error('Something went wrong')
      } else {
        toast.success('Apartment updated successfully')
        navigate('/admin/apartments')
      }
    } catch (error: any) {
      console.log(error)
      toast.error(error.message)
    }
  }

  const handleDelete = async () => {
    try {
      const result = await deleteApartment(apartment?.id)
      if (result.error) {
        throw new Error('Something went wrong')
      } else {
        toast.success('Apartment deleted successfully')
        navigate('/admin/apartments')
      }
    } catch (error: any) {
      console.log(error)
      toast.error(error.message)
    }
  }

  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className="flex flex-col h-full space-y-4">
        <div className="size-full flex lg:flex-row flex-col space-y-4 lg:space-x-4">
          <div className="size-full">
            <FormField
              control={form.control}
              name="floorNumber"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Floor Number</FormLabel>
                  <FormControl>
                    <Input
                      {...field}
                      type="text"
                      readOnly
                      className="read-only:bg-zinc-100"
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="area"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Area</FormLabel>
                  <FormControl>
                    <Input
                      {...field}
                      type="number"
                      onChange={(event) => field.onChange(+event.target.value)}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="description"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Description</FormLabel>
                  <FormControl>
                    <Textarea {...field} rows={5} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="status"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Status</FormLabel>
                  <FormControl>
                    <Select
                      value={field.value}
                      onValueChange={field.onChange}
                      // defaultValue={data?.status}
                    >
                      <SelectTrigger>
                        <SelectValue placeholder="Status" />
                      </SelectTrigger>
                      <SelectContent>
                        {['IN_USE', 'EMPTY', 'DISRUPTION'].map((status) => (
                          <SelectItem
                            key={status}
                            value={status as ApartmentStatus}>
                            {status}
                          </SelectItem>
                        ))}
                      </SelectContent>
                    </Select>
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
          </div>
          {/* <div className="size-full">
            <Table className="w-full h-full border">
              <TableHeader className="bg-primary">
                <TableRow className="[&>th]:text-black">
                  <TableHead>ID</TableHead>
                  <TableHead>Fullname</TableHead>
                  <TableHead>User Type</TableHead>
                  <TableHead>Created Date</TableHead>
                  <TableHead>Status</TableHead>
                </TableRow>
              </TableHeader>
              <TableBody>
                {Array.from({ length: 5 }).map((_, index) => (
                  <TableRow key={index}>
                    <TableCell>1</TableCell>
                    <TableCell>John Doe</TableCell>
                    <TableCell>Owner</TableCell>
                    <TableCell>2021-10-10</TableCell>
                    <TableCell>Active</TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </div> */}
        </div>
        <div className="w-full flex justify-between items-center">
          <AlertDelete
            description="apartment"
            setAction={() => handleDelete()}
            isLoading={isLoadingDelete}
          />
          <div className="flex space-x-4">
            <Button type="button" size={'lg'} variant="ghost">
              Cancel
            </Button>
            <Button type="submit" size={'lg'} variant="default">
              {isLoading ? (
                <Loader size={20} className="animate-spin" />
              ) : (
                'Save'
              )}
            </Button>
          </div>
        </div>
      </form>
    </Form>
  )
}

export default ApartmentFormDetail
