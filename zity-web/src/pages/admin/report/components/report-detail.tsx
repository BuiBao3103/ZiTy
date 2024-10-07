import { Button } from '@/components/ui/button'
import { Separator } from '@/components/ui/separator'
import AlertDelete from '@/components/alert/AlertDelete'
import { Report, ReportSchema } from '@/schema/report.validate'
import { FieldErrors, useForm } from 'react-hook-form'
import { z } from 'zod'
import { zodResolver } from '@hookform/resolvers/zod'
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
} from '@/components/ui/form'
import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar'
import DefaultAvatar from '@/assets/default-avatar.jpeg'
import { Textarea } from '@/components/ui/textarea'
import { RadioGroup, RadioGroupItem } from '@/components/ui/radio-group'
// import { Input } from '@/components/ui/input'
// import { ReportStatus } from '@/enums'
// import { Label } from '@/components/ui/label'

interface ReportDetailProps {
  report: Report | null
  setShowDetail: (value: Report | null) => void
}

const ReportDetail = ({ setShowDetail, report }: ReportDetailProps) => {
  const setAction = () => {
    console.log('delete')
  }

  const form = useForm<z.infer<typeof ReportSchema>>({
    defaultValues: {
      ...report,
    },
    resolver: zodResolver(ReportSchema),
  })

  const onSubmit = (data: z.infer<typeof ReportSchema>) => {
    console.log(data)
  }

  const onError = (error: FieldErrors<z.infer<typeof ReportSchema>>) => {
    console.log(error)
  }

  return (
    <div className="fixed w-full h-screen flex justify-center items-center inset-0 z-50">
      <div
        className="fixed inset-0 bg-black/20 animate-in fade-in"
        onClick={() => setShowDetail(null)}></div>
      <Form {...form}>
        <form
          onSubmit={form.handleSubmit(onSubmit, onError)}
          className="max-w-sm min-[550px]:max-w-lg w-full h-fit bg-white rounded-md relative z-[51] animate-in fade-in-95 zoom-in-95 shadow-lg">
          <div className="w-full flex justify-start items-center px-4 py-3 text-xl font-medium uppercase">
            Report #<span className="text-primary">{report?.id}</span>{' '}
          </div>
          <Separator />
          <div className="p-4 w-full flex flex-col space-y-4">
            <div className="w-full rounded-md border-2 border-zinc-200 flex flex-col space-y-2 p-4">
              <p>{report?.title}</p>
              <div className="w-full flex items-center gap-3">
                <Avatar>
                  <AvatarImage
                    src={report?.user?.avatar ?? DefaultAvatar}
                    className="size-12 rounded-full object-cover hidden sm:inline-block"
                  />
                  <AvatarFallback>
                    {report?.user.full_name
                      ? report?.user.full_name.slice(0, 2)
                      : 'CN'}
                  </AvatarFallback>
                </Avatar>
                <div className="flex flex-col">
                  <p className="font-medium">{report?.user?.full_name}</p>
                </div>
              </div>
              <p>{report?.content}</p>
            </div>
            <FormField
              control={form.control}
              name="content"
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
                    <RadioGroup
                      onValueChange={field.onChange}
                      defaultValue={field.value}
                      className="flex space-x-2">
                      {['PENDING', 'IN_PROGRESS', 'REJECTED', 'RESOLVED'].map(
                        (status) => (
                          <FormItem
                            key={status}
                            className="flex items-center space-x-2 space-y-0">
                            <FormControl>
                              <RadioGroupItem value={status} />
                            </FormControl>
                            <FormLabel className="font-normal">
                              {status}
                            </FormLabel>
                          </FormItem>
                        ),
                      )}
                    </RadioGroup>
                  </FormControl>
                </FormItem>
              )}
            />
          </div>
          <Separator />
          <div className="w-full flex justify-between items-center p-4">
            <AlertDelete description="report" setAction={setAction} />
            <div className="w-full flex justify-end gap-2">
              <Button
                onClick={() => setShowDetail(null)}
                type="button"
                variant={'ghost'}>
                Cancel
              </Button>
              <Button type="submit" variant={'default'}>
                Save
              </Button>
            </div>
          </div>
        </form>
      </Form>
    </div>
  )
}

export default ReportDetail
