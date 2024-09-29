import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar'
import { Button } from '@/components/ui/button'
import { Separator } from '@/components/ui/separator'
import {
  Tooltip,
  TooltipContent,
  TooltipTrigger,
} from '@/components/ui/tooltip'
import {
  Flag,
  House,
  LogOut,
  NotebookText,
  Package,
  Receipt,
  TableCellsMerge,
  UsersRound,
} from 'lucide-react'
import { Link, useLocation, useNavigate } from 'react-router-dom'
import { useWindowSize } from 'usehooks-ts'
import MobileMenu from './components/MobileMenu'
import Logo from '@/assets/logo.svg'
import { UserRole } from '@/enums'
export interface SideBarProps {
  label: string
  icon: React.ReactNode
  to: string
  role?: UserRole[] | UserRole
}

const Header = () => {
  const { width = 0 } = useWindowSize()
  const location = useLocation()
  const navigate = useNavigate()

  const userSideBars: SideBarProps[] = [
    {
      label: 'Home',
      icon: <House />,
      to: '/',
      role: 'RESIDENT',
    },
    {
      label: 'Package',
      icon: <Package />,
      to: '/package',
      role: 'RESIDENT',
    },
    {
      label: 'Survey',
      icon: <NotebookText />,
      to: '/survey',
      role: 'RESIDENT',
    },
    {
      label: 'Apartments',
      icon: <TableCellsMerge />,
      to: '/apartment',
      role: 'RESIDENT',
    },
    {
      label: 'Report',
      icon: <Flag />,
      to: '/report',
      role: 'RESIDENT',
    },
    {
      label: 'Bill',
      icon: <Receipt />,
      to: '/bill',
      role: ['ADMIN', 'RESIDENT'],
    },
    {
      label: 'User',
      icon: <UsersRound />,
      to: '/user',
      role: ['ADMIN'],
    },
  ]

	type Test<T> = T extends SideBarProps[] ? T : never

  return (
    <header className="w-full h-20 sm:h-screen sm:w-[300px] sticky top-0 z-40 flex sm:flex-row flex-col bg-white">
      <div className="w-full h-full flex sm:flex-col flex-row sm:items-stretch items-center sm:justify-start justify-between sm:p-0 p-4">
        <div className="sm:w-full h-full sm:h-[150px] sm:p-3 sm:order-none order-2">
          <img
            src={Logo}
            alt="Logo website"
            className="w-full h-full object-contain aspect-square"
          />
        </div>
        {width <= 640 && <MobileMenu sidebar={userSideBars} />}
        {width > 640 && <Separator />}
        <div className="w-full h-full hidden sm:flex flex-col gap-2 p-3">
          {userSideBars.map((sideBar, index) => (
            <Button
              asChild
              type="button"
              key={index}
              variant={'ghost'}
              size={'lg'}
              className={`gap-2 justify-start px-2 ${
                (sideBar.to === '/'
                  ? location.pathname === '/'
                  : location.pathname.startsWith(sideBar.to)) && 'bg-primary'
              }`}>
              <Link to={sideBar.to}>
                {sideBar.icon}
                {sideBar.label}
              </Link>
            </Button>
          ))}
        </div>
        {width > 640 && <Separator />}
        <div className="w-full p-3 hidden sm:flex items-center gap-2">
          <Avatar>
            <AvatarImage src="" />
            <AvatarFallback>CN</AvatarFallback>
          </Avatar>
          <div className="w-full flex flex-col">
            <span className="text-sm font-bold">User Name</span>
            <span className="text-xs">Role</span>
          </div>
          <div className="flex justify-end">
            <Tooltip>
              <TooltipTrigger asChild>
                <Button
                  onClick={() => navigate('/login', { replace: true })}
                  size={'icon'}
                  variant={'ghost'}>
                  <LogOut />
                </Button>
              </TooltipTrigger>
              <TooltipContent>Log out</TooltipContent>
            </Tooltip>
          </div>
        </div>
      </div>
      {width > 640 ? (
        <Separator orientation="vertical" />
      ) : (
        <Separator orientation="horizontal" />
      )}
    </header>
  )
}

export default Header
