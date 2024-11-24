import { useDocumentTitle } from 'usehooks-ts'

import { useAppSelector } from '@/store'
import { useLazyGetApartmentQuery } from '@/features/apartment/apartmentSlice'
import { useEffect, useState } from 'react'
import { ApartmentFormSchema } from '@/schema/apartment.validate'
import FirstLogin from '../firstLogin'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'
import FunctionBoxList from './components/function-box-list'
import ColumnDisplayInformation from './components/column-display-information'
import ColumnDisplayTable from './components/column-display-table'
import TableRowSkeleton from '@/components/skeleton/TableRowSkeleton'
const Index = () => {
  useDocumentTitle('Home')
  const date = new Date() // Current date
  const [getApartment, { isLoading }] = useLazyGetApartmentQuery()
  const [apartmentData, setApartmentData] = useState<ApartmentFormSchema | undefined>(undefined)
  const [houses, setHouses] = useState<string[]>([])
  const [selectedHouse, setSelectedHouse] = useState<string>(houses[0])
  const user = useAppSelector((state) => state.userReducer.user)
  const options: Intl.DateTimeFormatOptions = {
    weekday: 'short',
    month: 'long',
    day: 'numeric',
  }
  const formattedDate = date.toLocaleDateString('en-US', options)
  useEffect(() => {
    if (user) {
      const housesList = user.relationships?.map((item) => item.apartmentId) as string[]
      setHouses(housesList)
      // Set initial selected house to the first house in the list
      if (housesList.length > 0 && !selectedHouse) {
        setSelectedHouse(housesList[0])
      }
    }
  }, [user])
  useEffect(() => {
    const handleGetApartment = async () => {
      if (user) {
        await getApartment({
          id: selectedHouse,
          includes: 'relationships.User',
        })
          .unwrap()
          .then((payload) => {
            setApartmentData(payload)
          })
          .catch(() => {})
      }
    }
    handleGetApartment()
  }, [user, selectedHouse])

  return (
    <>
      <div className="w-full h-full min-h-screen md:h-screen p-4 bg-zinc-100 flex flex-col space-y-4 overflow-hidden">
        <p className="font-medium">{formattedDate}</p>
        <div className="w-full flex justify-between items-center">
          <p className="text-3xl font-bold">Hello, {`${user ? user?.fullName : 'loading...'}`}</p>
          <Select value={selectedHouse} onValueChange={(value) => setSelectedHouse(value)}>
            <SelectTrigger className="w-[200px] bg-white">
              <SelectValue placeholder="Select house" />
            </SelectTrigger>
            <SelectContent>
              {houses.map((house, index) => (
                <SelectItem key={index} value={house}>
                  {house}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
        </div>
        <div className="size-full grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4">
          <div className="w-full lg:col-span-1 lg:row-span-3 lg:row-start-1 lg:col-start-1 bg-white rounded-md border border-zinc-200 p-4 flex">
            {isLoading ? (
              <div className="size-full bg-gray-100 animate-pulse">
                <div className="w-full h-9 bg-gray-200 rounded-md mb-4"></div>
                <div className="w-full h-9 bg-gray-200 rounded-md mb-4"></div>
                <div className="w-full h-9 bg-gray-200 rounded-md mb-4"></div>
                <div className="w-full h-9 bg-gray-200 rounded-md mb-4"></div>
                <div className="w-full h-9 bg-gray-200 rounded-md mb-4"></div>
              </div>
            ) : (
              <ColumnDisplayInformation
                user={user}
                apartmentData={apartmentData}
                selectedHouse={selectedHouse}
              />
            )}
          </div>
          <div className="w-full lg:col-span-3 lg:row-span-3 lg:row-start-1 lg:col-start-2 bg-white rounded-md border border-zinc-200 p-4 flex">
            {isLoading ? (
              <div className="size-full bg-gray-100 animate-pulse">
                <TableRowSkeleton />
                <TableRowSkeleton />
                <TableRowSkeleton />
                <TableRowSkeleton />
              </div>
            ) : (
              <ColumnDisplayTable apartmentData={apartmentData} />
            )}
          </div>
          <div className="w-full grid grid-cols-1 min-[600px]:grid-cols-2 md:grid-cols-4 gap-4 col-span-1 md:col-span-2 lg:col-span-4">
            <FunctionBoxList />
          </div>
        </div>
      </div>
      {user && user.isFirstLogin && <FirstLogin />}
    </>
  )
}

export default Index
