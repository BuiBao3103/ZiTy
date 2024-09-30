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
import { Input } from '@/components/ui/input'
import { Separator } from '@/components/ui/separator'
import { useForm } from 'react-hook-form'
import { z } from 'zod'
import { ServiceFormSchema } from '@/schema/service.validate'
import { Textarea } from '@/components/ui/textarea'
import { toast } from 'sonner'
import { zodResolver } from '@hookform/resolvers/zod'

const ServiceForm = () => {
  const form = useForm<z.infer<typeof ServiceFormSchema>>({
		mode: 'onSubmit',
    defaultValues: {
      name: '',
      description: '',
      price: 0,
    },
		resolver: zodResolver(ServiceFormSchema)
  })

  const onSubmit = async (data: z.infer<typeof ServiceFormSchema>) => {
    console.log(data)
  }

	const onError = (error: any) => {
		console.log(error)
		if(error['name']){
			toast.error(error['name']?.message)
			return;
		}
		if(error['description']){
			toast.error(error['description']?.message)
			return;
		}
		if(error['price']){
			toast.error(error['price']?.message)
			return;
		}
	}

  return (
    <Dialog>
      <DialogTrigger asChild>
        <Button className="w-full sm:w-fit" variant={'default'} size={'lg'}>
          New Service
        </Button>
      </DialogTrigger>
      <DialogContent
        className="max-w-sm lg:max-w-lg xl:max-w-xl"
        aria-describedby={undefined}>
        <DialogHeader>
          <DialogTitle className="text-2xl">New Service</DialogTitle>
        </DialogHeader>
        <Separator />
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit,onError)} className="space-y-4">
            <div className="w-full flex flex-wrap md:flex-nowrap gap-2">
              <FormField
                control={form.control}
                name="name"
                render={({ field }) => (
                  <FormItem className="w-full">
                    <FormLabel>Name</FormLabel>
                    <FormControl>
                      <Input
                        placeholder="Type something"
                        {...field}
                        type="text"
                        className="focus-visible:ring-primary"
                      />
                    </FormControl>
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="price"
                render={({ field }) => (
                  <FormItem className="w-full">
                    <FormLabel>Price</FormLabel>
                    <FormControl>
                      <Input
                        placeholder="Type something"
                        {...field}
                        type="number"
                        className="focus-visible:ring-primary"
                      />
                    </FormControl>
                  </FormItem>
                )}
              />
            </div>
            <FormField
              control={form.control}
              name="description"
              render={({ field }) => (
                <FormItem className="w-full">
                  <FormLabel>Description</FormLabel>
                  <FormControl>
                    <Textarea {...field} placeholder="Type something"></Textarea>
                  </FormControl>
                </FormItem>
              )}
            />
            <div className="w-full flex justify-end gap-4">
              <DialogClose asChild>
                <Button type='button' size={'lg'} variant={'ghost'}>
                  Cancel
                </Button>
              </DialogClose>
              <Button type="submit" size={'lg'} variant={'default'}>
                Save
              </Button>
            </div>
          </form>
        </Form>
      </DialogContent>
    </Dialog>
  )
}

export default ServiceForm
