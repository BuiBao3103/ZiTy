import AlertDelete from '@/components/alert/AlertDelete'
import { Button } from '@/components/ui/button'
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
} from '@/components/ui/form'
import { Input } from '@/components/ui/input'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'
import { Separator } from '@/components/ui/separator'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table'
import { Textarea } from '@/components/ui/textarea'
import { ApartmentStatus } from '@/enums'
import { ApartmentSchema } from '@/schema/apartment.validate'
import { zodResolver } from '@hookform/resolvers/zod'
import { ChevronLeft } from 'lucide-react'
import { useForm } from 'react-hook-form'
import { useNavigate, useParams } from 'react-router-dom'
import { z } from 'zod'

const ApartmentDetail = () => {
  const navigate = useNavigate()
  const params = useParams()
  const form = useForm<z.infer<typeof ApartmentSchema>>({
    defaultValues: {},
    resolver: zodResolver(ApartmentSchema),
  })

  const onSubmit = async (data: z.infer<typeof ApartmentSchema>) => {
    console.log(data)
  }
  return (
    <div className="w-full h-full flex flex-col gap-4">
      <div className="flex  items-center">
        <Button
          onClick={() => navigate('/apartment')}
          size={'icon'}
          variant={'ghost'}>
          <ChevronLeft />
        </Button>
        <h1 className="text-xl font-medium">{params.id}</h1>
      </div>
      <Form {...form}>
        <form
          onSubmit={form.handleSubmit(onSubmit)}
          className="flex flex-col h-full space-y-4">
          <div className="size-full flex lg:flex-row flex-col space-y-4 lg:space-x-4">
            <div className="size-full">
              <FormField
                control={form.control}
                name="floor_number"
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
                      <Input {...field} type="text" />
                    </FormControl>
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
                        defaultValue="IN_USE">
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
                  </FormItem>
                )}
              />
            </div>
            <div className="size-full">
              <Table className="w-full h-full border">
                <TableHeader className="bg-zinc-200">
                  <TableRow>
                    <TableHead>ID</TableHead>
                    <TableHead>Fullname</TableHead>
                    <TableHead>User Type</TableHead>
                    <TableHead>Created Date</TableHead>
                    <TableHead>Status</TableHead>
                  </TableRow>
                </TableHeader>
                <TableBody>
                  {Array.from({ length: 5 }).map((_, index) => (
                    <TableRow>
                      <TableCell>1</TableCell>
                      <TableCell>John Doe</TableCell>
                      <TableCell>Owner</TableCell>
                      <TableCell>2021-10-10</TableCell>
                      <TableCell>Active</TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </div>
          </div>
          <div className="w-full flex justify-between items-center">
            <AlertDelete description="apartment" setAction={() => {}} />
            <div className="flex space-x-4">
              <Button variant="ghost">Cancel</Button>
              <Button type="submit" variant="default">
                Save
              </Button>
            </div>
          </div>
        </form>
      </Form>
    </div>
  )
}

export default ApartmentDetail
