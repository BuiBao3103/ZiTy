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
  FormMessage,
} from '@/components/ui/form'
import { useForm } from 'react-hook-form'
import { z } from 'zod'
import { zodResolver } from '@hookform/resolvers/zod'

import { Separator } from '@/components/ui/separator'
import { Input } from '@/components/ui/input'
import React, { useEffect, useState } from 'react'
import { Textarea } from '@/components/ui/textarea'
import { IReport, ReportSchema } from '@/schema/report.validate'
import { formatDateWithSlash } from '@/utils/Generate'
import { useCreateReportMutation } from '@/features/reports/reportSlice'
import { useAppSelector } from '@/store'
import { toast } from 'sonner'

interface ReportFormProps {
  children: React.ReactNode
  report?: IReport
  mode: 'create' | 'update'
}

const ReportForm = ({ children, report, mode = 'create' }: ReportFormProps) => {
  const [open, setOpen] = useState<boolean>(false)
  const user = useAppSelector((state) => state.userReducer.user)
  const form = useForm<z.infer<typeof ReportSchema>>({})
  const [createReport, { isLoading }] = useCreateReportMutation()

  useEffect(() => {
    if (report) {
      form.reset({
        title: report.title,
        content: report.content,
      })
    }
  }, [])

  const onSubmit = async (data: z.infer<typeof ReportSchema>) => {
    if (mode === 'create') {
      await createReport({
        title: data.title,
        content: data.content,
        relationshipId: user?.relationships?.[0].id,
        status: 'PENDING',
      })
        .unwrap()
        .then((payload) => {
          console.log(payload)
          toast.success('Send report to admin successfully')
          setOpen(false)
        })
        .catch((error) => {
          console.log(error)
        })
    }
  }

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger asChild>{children}</DialogTrigger>
      <DialogContent
        className="max-w-sm min-[500px]:max-w-md sm:max-w-lg lg:max-w-2xl"
        aria-describedby={undefined}>
        <DialogHeader>
          <DialogTitle className="text-2xl">
            {report?.id
              ? `Report - ${formatDateWithSlash(new Date(report?.createdAt))}`
              : 'New Report'}
          </DialogTitle>
        </DialogHeader>
        <Separator />
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
            <FormField
              control={form.control}
              name="title"
              render={({ field }) => (
                <FormItem className="w-full space-y-4">
                  <FormLabel className="text-base">Title</FormLabel>
                  <FormControl>
                    <Input {...field} placeholder="Title" />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <Separator />
            <FormField
              control={form.control}
              name="content"
              render={({ field }) => (
                <FormItem className="w-full space-y-4">
                  <FormLabel className="text-base">
                    What do you think about that problem? (Write something)
                  </FormLabel>
                  <FormControl>
                    <Textarea
                      rows={5}
                      {...field}
                      placeholder="Write something..."></Textarea>
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            {mode === 'create' && (
              <div className="w-full flex justify-end gap-4">
                <DialogClose asChild>
                  <Button size={'lg'} type="button" variant={'ghost'}>
                    Cancel
                  </Button>
                </DialogClose>
                <Button size={'lg'} type="submit" variant={'default'}>
                  {isLoading ? 'Submitting...' : 'Submit'}
                </Button>
              </div>
            )}
          </form>
        </Form>
      </DialogContent>
    </Dialog>
  )
}

export default ReportForm
