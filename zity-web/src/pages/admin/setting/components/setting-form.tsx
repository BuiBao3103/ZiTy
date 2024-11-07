import { Button } from '@/components/ui/button'
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
} from '@/components/ui/form'
import { Input } from '@/components/ui/input'
import { usePatchSettingMutation } from '@/features/setting/settingSlice'
import { SettingSchema } from '@/schema/setting.validate'
import { useForm } from 'react-hook-form'
import { z } from 'zod'

interface SettingFormProps {
  setting?: z.infer<typeof SettingSchema>
}

const SettingForm = () => {
  const [patchSetting, { isLoading }] = usePatchSettingMutation()

  const form = useForm<z.infer<typeof SettingSchema>>()

  const onSubmit = async (data: z.infer<typeof SettingSchema>) => {
    await patchSetting(data)
      .unwrap()
      .then((payload) => {
        console.log(payload)
      })
      .catch((error) => {
        console.log(error)
      })
  }

  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className="space-y-2 h-full overflow-hidden">
        <FormField
          control={form.control}
          name="currentMonthly"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Current Monthly</FormLabel>
              <FormControl>
                <Input {...field} className="md:w-1/2" />
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
                <Input {...field} className="md:w-1/2" />
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
                <Input {...field} className="md:w-1/2" />
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
                <Input {...field} className="md:w-1/2" />
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
                <Input {...field} className="md:w-1/2" />
              </FormControl>
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="currentMonthly"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Current Monthly</FormLabel>
              <FormControl>
                <Input {...field} className="md:w-1/2" />
              </FormControl>
            </FormItem>
          )}
        />
        <div className="pt-4">
          <Button type="submit" className="">
            Submit
          </Button>
        </div>
      </form>
    </Form>
  )
}

export default SettingForm
