import { Input } from '@/components/ui/input'
import {
  Pagination,
  PaginationContent,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from '@/components/ui/pagination'
import { Filter, Search } from 'lucide-react'
import { Button } from '@components/ui/button'
import SettingForm from './components/setting-form'
import SettingList from './components/setting-list'
import BreadCrumb from '@/components/breadcrumb'
import { useDocumentTitle } from 'usehooks-ts'
import { Setting } from '@/schema/setting.validate'

const Index = () => {
  useDocumentTitle('Service')
  const settings: Setting[] = [
    {
      id: 1,
      current_monthly: '2022-09-01',
      system_status: 'OVERDUE',
      room_price_per_m2: 1000,
      water_price_per_m3: 1000,
      water_vat: 1000,
      env_protection_tax: 1000,
    },
    {
      id: 2,
      current_monthly: '2022-09-01',
      system_status: 'PAYMENT',
      room_price_per_m2: 1000,
      water_price_per_m3: 1000,
      water_vat: 1000,
      env_protection_tax: 1000,
    },
    {
      id: 3,
      current_monthly: '2022-09-01',
      system_status: 'PREPAYMENT',
      room_price_per_m2: 1000,
      water_price_per_m3: 1000,
      water_vat: 1000,
      env_protection_tax: 1000,
    },
    {
      id: 4,
      current_monthly: '2022-09-01',
      system_status: 'DELINQUENT',
      room_price_per_m2: 1000,
      water_price_per_m3: 1000,
      water_vat: 1000,
      env_protection_tax: 1000,
    },
    {
      id: 5,
      current_monthly: '2022-09-01',
      system_status: 'PAYMENT',
      room_price_per_m2: 1000,
      water_price_per_m3: 1000,
      water_vat: 1000,
      env_protection_tax: 1000,
    },
  ]

  return (
    <>
      <div className="w-full sm:h-screen flex flex-col bg-zinc-100">
        <BreadCrumb paths={[{ label: 'service', to: '/service' }]} />
        <div className="size-full p-4">
          <div className="size-full p-4 bg-white rounded-md flex flex-col">
            <div className="w-full h-auto flex justify-between items-center">
              <div className="w-full flex gap-4 items-center">
                <div className="lg:w-1/4 flex items-center border px-3 py-0.5 relative rounded-md focus-within:border-primary transition-all">
                  <Search size={20} />
                  <Input
                    placeholder="Search something"
                    className="border-none shadow-none focus-visible:ring-0"
                  />
                </div>
                <Button className="gap-1" size={'lg'} variant={'secondary'}>
                  <Filter size={20} />
                  Filter
                </Button>
              </div>
              <SettingForm />
            </div>
            <div className="size-full">
              <SettingList settings={settings} />
            </div>
            <Pagination className="mt-2">
              <PaginationContent>
                <PaginationItem>
                  <PaginationPrevious to="#" />
                </PaginationItem>
                {[1, 2, 3, 4, 5].map((page) => (
                  <PaginationItem
                    key={page}
                    className={`${page === 1 ? 'bg-primary rounded-md' : ''}`}>
                    <PaginationLink to="#">{page}</PaginationLink>
                  </PaginationItem>
                ))}
                <PaginationItem>
                  <PaginationNext to="#" />
                </PaginationItem>
              </PaginationContent>
            </Pagination>
          </div>
        </div>
      </div>
    </>
  )
}

export default Index
