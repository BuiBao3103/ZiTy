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
import { useForm } from 'react-hook-form'
import { z } from 'zod'
import { zodResolver } from '@hookform/resolvers/zod'

import { Separator } from '@/components/ui/separator'
import { Input } from '@/components/ui/input'
import React from 'react'
import { Label } from '@/components/ui/label'

interface ReportFormProps {
  children: React.ReactNode
}

const ReportForm = ({ children }: ReportFormProps) => {
  const form = useForm()

  const onSubmit = async (data: any) => {
    console.log(data)
  }

  return (
    <Dialog>
      <DialogTrigger asChild>{children}</DialogTrigger>
      <DialogContent className='lg:max-w-2xl' aria-describedby={undefined}>
        <DialogHeader>
          <DialogTitle className="text-2xl">New Report</DialogTitle>
        </DialogHeader>
        <Separator />
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
            <FormField
              control={form.control}
              name="reasons"
              render={({ field }) => (
                <FormItem className="w-full space-y-4">
                  <FormLabel className='text-base'>
                    Why do you make this report? (Select as least one)
                  </FormLabel>
                  <FormControl>
                    <div className="grid grid-cols-3 gap-4 items-center">
                      {[
                        'Environment',
                        'Noise',
                        'Unauthorized Pets',
                        'Property Damage',
                        'Maintenance Problems',
                        'Lease Violation',
                        'Unsanitary Conditions',
                        'Other',
                      ].map((reason) => (
                        <Label
                          key={reason}
                          className="text-base flex items-center font-medium">
                          <input
                            type="checkbox"
                            // checked={field.value.includes(reason)}
                            // onChange={() => {
                            //   const newValue = field.value.includes(reason)
                            //     ? field.value.filter((r) => r !== reason)
                            //     : [...field.value, reason]
                            //   field.onChange(newValue) // Update the field value
                            // }}
                            className="mr-2"
                          />
                          {reason}
                        </Label>
                      ))}
                    </div>
                  </FormControl>
                </FormItem>
              )}
            />
            <div className="w-full flex justify-end gap-4">
              <DialogClose asChild>
                <Button size={'lg'} variant={'ghost'}>
                  Cancel
                </Button>
              </DialogClose>
              <DialogClose asChild>
                <Button size={'lg'} variant={'default'}>
                  Send
                </Button>
              </DialogClose>
            </div>
          </form>
        </Form>
      </DialogContent>
    </Dialog>
  )
}

export default ReportForm
