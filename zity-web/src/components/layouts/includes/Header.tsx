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
  HandPlatter,
  House,
  LogOut,
  MessageCircleQuestion,
  NotebookText,
  Package,
  PanelRightClose,
  PanelRightOpen,
  Receipt,
  TableCellsMerge,
  UsersRound,
} from 'lucide-react'
import { Link, useLocation, useNavigate } from 'react-router-dom'
import { useWindowSize } from 'usehooks-ts'
import MobileMenu from './components/MobileMenu'
import Logo from '@/assets/logo.svg'
import LogoMobile from '@/assets/logoMobile.svg'
import { UserRole } from '@/enums'
import { useState } from 'react'
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
  const [panelRightOpen, setPanelRightOpen] = useState<boolean>(false)

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
      label: 'User Admin',
      icon: <UsersRound />,
      to: '/user',
      role: ['ADMIN'],
    },
    {
      label: 'Service Admin',
      icon: <HandPlatter />,
      to: '/service',
      role: ['ADMIN'],
    },
    {
      label: 'Package Admin',
      icon: <Package />,
      to: '/admin/package',
      role: ['ADMIN'],
    },
    {
      label: 'Bill Admin',
      icon: <Receipt />,
      to: '/admin/bill',
      role: ['ADMIN'],
    },
    {
      label: 'Survey Admin',
      icon: <NotebookText />,
      to: '/admin/survey',
      role: ['ADMIN'],
    },
    {
      label: 'Report Admin',
      icon: <Flag />,
      to: '/admin/report',
      role: ['ADMIN'],
    },
    {
      label: 'Ask For Support',
      icon: <MessageCircleQuestion />,
      to: '/chat',
      role: ['RESIDENT'],
    },
  ]

  // useEffect(() => {
  //   console.log(width)
  //   if (width <= 1024) {
  //     setPanelRightOpen(true)
  //   } else {
  //     setPanelRightOpen(false)
  //   }
  // }, [width])

  return (
    <header
      className={`${
        panelRightOpen ? 'sm:w-[60px]' : 'sm:w-[300px]'
      } transition-all duration-300 w-full h-20 sm:h-screen sticky top-0 z-40 flex sm:flex-row flex-col bg-white overflow-hidden`}>
      <div className="w-full h-full flex sm:flex-col flex-row sm:items-stretch items-center sm:justify-start justify-between sm:p-0 p-4">
        <div
          className={`sm:w-full h-full sm:h-[150px] sm:p-3 sm:order-none order-2 relative`}>
          <img
            src={panelRightOpen ? LogoMobile : Logo}
            onClick={() => navigate('/')}
            loading="lazy"
            alt="Logo website"
            className={`w-full h-full object-contain aspect-square cursor-pointer ${
              panelRightOpen && 'mt-5'
            }`}
          />
          {width > 640 && (
            <Button
              size={'icon'}
              variant={'secondary'}
              onClick={() => setPanelRightOpen(!panelRightOpen)}
              className="absolute top-3 right-3 z-10">
              <Tooltip>
                <TooltipTrigger asChild>
                  {panelRightOpen ? <PanelRightClose /> : <PanelRightOpen />}
                </TooltipTrigger>
                <TooltipContent side="right">
                  {panelRightOpen ? 'Close menu' : 'Open menu'}
                </TooltipContent>
              </Tooltip>
            </Button>
          )}
        </div>
        {width <= 640 && <MobileMenu sidebar={userSideBars} />}
        {width > 640 && <Separator />}
        <div
          className={`sidebar w-full h-full hidden sm:flex flex-col overflow-y-auto ${
            panelRightOpen ? 'gap-2 p-2' : 'gap-2 p-4'
          }`}>
          {userSideBars.map((sideBar, index) => (
            <Button
              asChild
              type="button"
              key={index}
              variant={'ghost'}
              size={`${panelRightOpen ? 'icon' : 'lg'}`}
              className={`${
                !panelRightOpen ? 'gap-2 justify-start px-2' : 'justify-center'
              } ${
                (sideBar.to === '/'
                  ? location.pathname === '/'
                  : location.pathname.startsWith(sideBar.to)) && 'bg-primary'
              }`}>
              {panelRightOpen ? (
                <Tooltip>
                  <TooltipTrigger asChild>
                    <Link
                      to={sideBar.to}
                      className={`w-full p-2 flex justify-center items-center rounded-md hover:bg-zinc-100 transition-all ${
                        (sideBar.to === '/'
                          ? location.pathname === '/'
                          : location.pathname.startsWith(sideBar.to)) &&
                        'bg-primary'
                      }`}>
                      {sideBar.icon}
                    </Link>
                  </TooltipTrigger>
                  <TooltipContent side="right">{sideBar.label}</TooltipContent>
                </Tooltip>
              ) : (
                <Link to={sideBar.to}>
                  {sideBar.icon}
                  <span>{sideBar.label}</span>
                </Link>
              )}
            </Button>
          ))}
        </div>
        {width > 640 && <Separator />}
        <div
          className={`w-full p-3 hidden ${
            panelRightOpen ? 'flex-col' : 'flex-row'
          } sm:flex items-center gap-2`}>
          <Avatar>
            <AvatarImage src="" />
            <AvatarFallback>CN</AvatarFallback>
          </Avatar>
          <div
            className={`w-full ${panelRightOpen ? 'hidden' : 'flex'} flex-col`}>
            <span className="text-sm font-bold">User Name</span>
            <span className="text-xs">Role</span>
          </div>
          <div className={`w-full flex justify-end`}>
            <Tooltip>
              <TooltipTrigger asChild>
                <Button
                  onClick={() => navigate('/login', { replace: true })}
                  size={'icon'}
                  variant={'ghost'}>
                  <LogOut />
                </Button>
              </TooltipTrigger>
              <TooltipContent side="right">Log out</TooltipContent>
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
