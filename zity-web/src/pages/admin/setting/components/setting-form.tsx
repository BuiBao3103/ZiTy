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
import {
  usePatchSettingMutation,
  useUpdateTransitionDelinquentMutation,
  useUpdateTransitionOverdueMutation,
  useUpdateTransitionPaymentMutation,
  useUpdateTransitionPrepaymentMutation,
} from '@/features/setting/settingSlice'
import { SettingSchema } from '@/schema/setting.validate'
import { useEffect } from 'react'
import { useForm } from 'react-hook-form'
import { toast } from 'sonner'
import { z } from 'zod'

interface SettingFormProps {
  setting?: z.infer<typeof SettingSchema>
}

const SettingForm = ({ setting }: SettingFormProps) => {
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
        <div className="md:w-1/2 w-full">
          <FormField
            control={form.control}
            name="currentMonthly"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Current Monthly</FormLabel>
                <FormControl>
                  <Input {...field} type="month" />
                </FormControl>
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name="roomPricePerM2"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Room Price Per M2</FormLabel>
                <FormControl>
                  <Input {...field} />
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
                  <Input {...field} />
                </FormControl>
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name="waterVat"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Water VAT</FormLabel>
                <FormControl>
                  <Input {...field} />
                </FormControl>
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name="envProtectionTax"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Environment Protection Tax</FormLabel>
                <FormControl>
                  <Input {...field} />
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
        <div className="md:w-1/2 w-full flex flex-col space-y-4">
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
        </div>
      </form>
    </Form>
  )
}

export default SettingForm
