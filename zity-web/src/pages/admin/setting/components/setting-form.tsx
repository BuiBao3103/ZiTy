import { Badge } from '@/components/ui/badge'
import { Button } from '@/components/ui/button'
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
} from '@/components/ui/form'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { Skeleton } from '@/components/ui/skeleton'
import {
  usePatchSettingMutation,
  useUpdateTransitionDelinquentMutation,
  useUpdateTransitionOverdueMutation,
  useUpdateTransitionPaymentMutation,
  useUpdateTransitionPrepaymentMutation,
} from '@/features/setting/settingSlice'
import { SettingSchema } from '@/schema/setting.validate'
import { useEffect, useState } from 'react'
import { useForm } from 'react-hook-form'
import { toast } from 'sonner'
import { useDebounceValue } from 'usehooks-ts'
import { z } from 'zod'

interface SettingFormProps {
  setting?: z.infer<typeof SettingSchema>
  isLoading?: boolean
  isFetching?: boolean
}

const SettingForm = ({ setting, isLoading, isFetching }: SettingFormProps) => {
  const [debounceCurrentMonthly, updateDebounceCurrentValue] = useDebounceValue(
    setting?.currentMonthly,
    700,
  )
  const [patchSetting, { isLoading: isUpdateSetting }] =
    usePatchSettingMutation()
  const [updateStatusPrepayment, { isLoading: isUpdatePrepayment }] =
    useUpdateTransitionPrepaymentMutation()
  const [updateStatusPayment, { isLoading: isUpdatePayment }] =
    useUpdateTransitionPaymentMutation()
  const [updateStatusOverdue, { isLoading: isUpdateOverdue }] =
    useUpdateTransitionOverdueMutation()
  const [updateStatusDelinquent, { isLoading: isUpdateDelinquent }] =
    useUpdateTransitionDelinquentMutation()

  const form = useForm<z.infer<typeof SettingSchema>>()

  const onSubmit = async (data: z.infer<typeof SettingSchema>) => {
    // console.log(data)
    await patchSetting(data)
      .unwrap()
      .then((payload) => {
        console.log(payload)
        toast.success('Update setting successfully')
      })
      .catch((error) => {
        console.log(error)
      })
  }

  const onSubmitMonthly = async (
    data: Pick<z.infer<typeof SettingSchema>, 'currentMonthly'>,
  ) => {
    await patchSetting(data)
      .unwrap()
      .then((payload) => {
        console.log(payload)
        toast.success('Update setting successfully')
      })
      .catch((error) => {
        console.log(error)
      })
  }
  useEffect(() => {
    if (setting) {
      form.reset(setting)
    }
  }, [setting])

  const handlePrepaymentTransition = async () => {
    try {
      await updateStatusPrepayment()
        .unwrap()
        .then(() => {
          toast.success('Updated to Prepayment status successfully')
        })
        .catch(() => {
          toast.error('Failed to update status')
        })
    } catch (error) {
      console.error('Failed to update to Prepayment status:', error)
      toast.error('Failed to update status')
    }
  }

  const handlePaymentTransition = async () => {
    try {
      await updateStatusPayment()
        .unwrap()
        .then(() => {
          toast.success('Updated to Payment status successfully')
        })
        .catch(() => {
          toast.error('Failed to update status')
        })
    } catch (error) {
      console.error('Failed to update to Payment status:', error)
      toast.error('Failed to update status')
    }
  }

  const handleOverdueTransition = async () => {
    try {
      await updateStatusOverdue()
        .unwrap()
        .then(() => {
          toast.success('Updated to Overdue status successfully')
        })
        .catch(() => {
          toast.error('Failed to update status')
        })
    } catch (error) {
      console.error('Failed to update to Overdue status:', error)
      toast.error('Failed to update status')
    }
  }

  const handleDelinquentTransition = async () => {
    try {
      await updateStatusDelinquent()
        .unwrap()
        .then(() => {
          toast.success('Updated to Delinquent status successfully')
        })
        .catch(() => {
          toast.error('Failed to update status')
        })
    } catch (error) {
      console.error('Failed to update to Delinquent status:', error)
      toast.error('Failed to update status')
    }
  }

  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className="space-y-0 h-full overflow-hidden flex space-x-4">
        <div className="md:w-1/2 w-full flex flex-col space-y-4">
          {isLoading || isFetching ? (
            <>
              <Skeleton className="w-full h-[144px]" />
              <Skeleton className="w-full h-[358px]" />
            </>
          ) : (
            <>
              <div className="border border-zinc-300 p-4 rounded-md flex flex-col space-y-3 shadow-md">
                <Label>Current Monthly</Label>
                <Input
                  type="month"
                  defaultValue={setting?.currentMonthly}
                  onChange={(e) => updateDebounceCurrentValue(e.target.value)}
                />
                <Button
                  type="button"
                  onClick={() =>
                    onSubmitMonthly({ currentMonthly: debounceCurrentMonthly })
                  }
                  className="w-fit">
                  {isUpdateSetting ? 'Loading...' : 'Submit'}
                </Button>
              </div>
              <div className="border border-zinc-300 p-4 rounded-md shadow-md">
                <FormField
                  control={form.control}
                  name="roomPricePerM2"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Room Price Per M2</FormLabel>
                      <FormControl>
                        <Input
                          type="number"
                          step={1000}
                          defaultValue={field.value}
                          onBlur={field.onBlur}
                          name={field.name}
                          ref={field.ref}
                          placeholder="Enter room price per m2"
                        />
                      </FormControl>
                    </FormItem>
                  )}
                />
                <FormField
                  control={form.control}
                  name="waterPricePerM3"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Water Price Per M3</FormLabel>
                      <FormControl>
                        <Input
                          type="number"
                          step={1000}
                          defaultValue={field.value}
                          onBlur={field.onBlur}
                          name={field.name}
                          ref={field.ref}
                          placeholder="Enter water price per m3"
                        />
                      </FormControl>
                    </FormItem>
                  )}
                />
                <FormField
                  control={form.control}
                  name="waterVat"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Water VAT (%)</FormLabel>
                      <FormControl>
                        <Input
                          type="number"
                          defaultValue={field.value}
                          onBlur={field.onBlur}
                          name={field.name}
                          ref={field.ref}
                          placeholder="Enter water VAT"
                        />
                      </FormControl>
                    </FormItem>
                  )}
                />
                <FormField
                  control={form.control}
                  name="envProtectionTax"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Environment Protection Tax (%)</FormLabel>
                      <FormControl>
                        <Input
                          type="number"
                          defaultValue={field.value}
                          onBlur={field.onBlur}
                          name={field.name}
                          ref={field.ref}
                          placeholder="Enter environment protection tax"
                        />
                      </FormControl>
                    </FormItem>
                  )}
                />
                <div className="pt-4">
                  <Button type="submit" className="">
                    {isUpdateSetting ? 'Loading...' : 'Submit'}
                  </Button>
                </div>
              </div>
            </>
          )}
        </div>
        <div className="md:w-1/2 w-full">
          {isLoading || isFetching ? (
            <Skeleton className="w-full h-[140px]" />
          ) : (
            <div className="flex flex-col space-y-4 p-4 border border-zinc-300 rounded-md h-fit shadow-md">
              <Label>System Status</Label>
              <div className="flex gap-4">
                <Button
                  variant="info"
                  onClick={handlePrepaymentTransition}
                  disabled={isUpdatePrepayment}>
                  {isUpdatePrepayment ? 'Updating...' : 'Prepayment'}
                </Button>
                <Button
                  variant="success"
                  onClick={handlePaymentTransition}
                  disabled={isUpdatePayment}>
                  {isUpdatePayment ? 'Updating...' : 'Payment'}
                </Button>
                <Button
                  variant="warning"
                  onClick={handleOverdueTransition}
                  disabled={isUpdateOverdue}>
                  {isUpdateOverdue ? 'Updating...' : 'Overdue'}
                </Button>
                <Button
                  variant="destructive"
                  onClick={handleDelinquentTransition}
                  disabled={isUpdateDelinquent}>
                  {isUpdateDelinquent ? 'Updating...' : 'Delinquent'}
                </Button>
              </div>
              <div className="flex gap-4">
                <p className="font-medium">Current System Status:</p>
                <Badge
                  variant={`${
                    setting?.systemStatus === 'DELINQUENT'
                      ? 'destructive'
                      : setting?.systemStatus === 'OVERDUE'
                      ? 'warning'
                      : setting?.systemStatus === 'PAYMENT'
                      ? 'success'
                      : 'info'
                  }`}>
                  {setting?.systemStatus}
                </Badge>
              </div>
            </div>
          )}
        </div>
      </form>
    </Form>
  )
}

export default SettingForm
