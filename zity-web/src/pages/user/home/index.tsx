import { useDocumentTitle } from 'usehooks-ts'
import {
  Flag,
  House,
  LayoutPanelTop,
  NotebookText,
  Package,
  Receipt,
} from 'lucide-react'
import { Separator } from '@/components/ui/separator'
import FunctionBox, { FunctionBoxProps } from './components/FunctionBox'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table'
import { useAppSelector } from '@/store'
import { ROUTES } from '@/configs/endpoint'
import { useLazyGetApartmentQuery } from '@/features/apartment/apartmentSlice'
import { useEffect, useState } from 'react'
import { IApartment } from '@/schema/apartment.validate'
import FirstLogin from '../firstLogin'
const Index = () => {
  useDocumentTitle('Home')
  const date = new Date() // Current date
  const [getApartment, { isLoading }] = useLazyGetApartmentQuery()
  const [apartmentData, setApartmentData] = useState<IApartment | undefined>(
    undefined,
  )
  const user = useAppSelector((state) => state.userReducer.user)
  const options: Intl.DateTimeFormatOptions = {
    weekday: 'short',
    month: 'long',
    day: 'numeric',
  }
  const formattedDate = date.toLocaleDateString('en-US', options)

  const functionLists: FunctionBoxProps[] = [
    {
      icon: <Package size={50} className="text-primary" />,
      to: ROUTES.PACKAGES,
      title: 'Packages',
      description: 'Manage your packages',
    },
    {
      icon: <NotebookText size={50} className="text-primary" />,
      to: ROUTES.SURVEYS,
      title: 'Survey',
      description: 'Take a survey',
    },
    {
      icon: <Flag size={50} className="text-primary" />,
      to: ROUTES.REPORTS,
      title: 'Report',
      description: 'Report an issue',
    },
    {
      icon: <Receipt size={50} className="text-primary" />,
      to: ROUTES.BILLS,
      title: 'Bills',
      description: 'Manage your payment',
    },
  ]

  useEffect(() => {
    const handleGetApartment = async () => {
      if (user) {
        await getApartment(user?.relationships?.[0].apartmentId)
          .unwrap()
          .then((payload) => {
            setApartmentData(payload)
          })
          .catch(() => {
          })
      }
    }
    handleGetApartment()
  }, [user])

  return (
    <>
      <div className="w-full h-full min-h-screen md:h-screen p-4 bg-zinc-100 flex flex-col space-y-4 overflow-hidden">
        <p className="font-medium">{formattedDate}</p>
        <p className="text-3xl font-bold">
          Hello, {`${user ? user?.fullName : 'loading...'}`}
        </p>
        <div className="size-full grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4">
          <div className="w-full lg:col-span-1 lg:row-span-3 lg:row-start-1 lg:col-start-1 bg-white rounded-md border border-zinc-200 p-4 flex flex-col space-y-2">
            <p className="flex items-center gap-2 text-lg font-medium uppercase">
              <span>
                <LayoutPanelTop />
              </span>
              Apartment{' '}
              <span className="text-primary">
                {user?.relationships?.[0].apartmentId}
              </span>
            </p>
            <Separator />
            <p>
              <span className="font-medium">Floor:</span>{' '}
              {apartmentData?.floorNumber}
            </p>
            <p>
              <span className="font-medium">Owner:</span>{' '}
              {user ? user?.fullName : 'loading...'}
            </p>
            <p>
              <span className="font-medium">Area:</span> {apartmentData?.area}x
              {apartmentData?.area} (m<sup>2</sup>)
            </p>
            <p>
              <span className="font-medium">Description: </span>
              {apartmentData?.description}
            </p>
          </div>
          <div className="w-full lg:col-span-3 lg:row-span-3 lg:row-start-1 lg:col-start-2 bg-white rounded-md border border-zinc-200 p-4 flex flex-col space-y-2">
            <p className="flex items-center gap-2 text-lg font-medium uppercase">
              <span>
                <House />
              </span>
              Members List
            </p>
            <Separator />
            <div className="w-full">
              <Table className="w-full border">
                <TableHeader className="bg-primary">
                  <TableRow>
                    <TableHead className="text-white">ID123</TableHead>
                    <TableHead className="text-white">Fullname</TableHead>
                    <TableHead className="text-white">User Type</TableHead>
                    <TableHead className="text-white">Created Date</TableHead>
                    <TableHead className="text-white">Status</TableHead>
                  </TableRow>
                </TableHeader>
                <TableBody>
                  {Array.from({ length: 5 }).map((_, index) => (
                    <TableRow key={index}>
                      <TableCell>1</TableCell>
                      <TableCell>John Doe</TableCell>
                      <TableCell>Owner</TableCell>
                      <TableCell>2021-10-10</TableCell>
                      <TableCell>Active</TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </div>
          </div>
          <div className="w-full grid grid-cols-1 min-[600px]:grid-cols-2 md:grid-cols-4 gap-4 col-span-1 md:col-span-2 lg:col-span-4">
            {functionLists.map((item, index) => (
              <div key={index} className={`size-full`}>
                <FunctionBox
                  description={item.description}
                  icon={item.icon}
                  title={item.title}
                  to={item.to}
                />
              </div>
            ))}
          </div>
        </div>
      </div>
      {user && user.isFirstLogin && <FirstLogin />}
    </>
  )
}

export default Index
