import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from '@/components/ui/dialog'
import { Separator } from '@/components/ui/separator'
import { Loader} from 'lucide-react'
import React, { useState } from 'react'
import { useGetPackageQuery } from '@/features/package/packageSlice'
import PackageForm from './package-form'

interface PackageDetailProps {
  children: React.ReactNode
  id?: number
  mode: 'create' | 'edit'
}

const PackageDetail = ({ children, id, mode }: PackageDetailProps) => {
  const [open, setOpen] = useState<boolean>(false)
  const { data: packagee, isLoading, isFetching } = useGetPackageQuery(id,{
		skip: mode === 'create' || !id,
	})

  const dialogTitle =
    mode === 'edit' ? (
      <>
        Package #<span className="text-primary">{packagee?.id}</span>
      </>
    ) : (
      'New Package'
    )

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger asChild>{children}</DialogTrigger>
      <DialogContent
        className="max-w-sm lg:max-w-lg xl:max-w-xl"
        aria-describedby={undefined}>
        <DialogHeader>
          <DialogTitle className="text-2xl">{dialogTitle}</DialogTitle>
        </DialogHeader>
        <Separator />
        {isLoading || isFetching ? (
          <div className="size-full flex justify-center items-center">
            <Loader size={40} className="animate-spin text-primary" />
          </div>
        ) : (
          <PackageForm packagee={packagee} setOpen={setOpen} />
        )}
      </DialogContent>
    </Dialog>
  )
}

export default PackageDetail
