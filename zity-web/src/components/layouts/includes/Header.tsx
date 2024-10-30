import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar'
import { Button } from '@/components/ui/button'
import { Separator } from '@/components/ui/separator'
import {
  Tooltip,
  TooltipContent,
  TooltipTrigger,
} from '@/components/ui/tooltip'
import {
  Cog,
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
import { useEffect, useMemo, useState } from 'react'
import { useAppDispath, useAppSelector } from '@/store'
import { ROUTES } from '@/configs/endpoint'
import Cookies from 'universal-cookie'
import { userLoggedOut } from '@/features/auth/authSlice'
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
  const cookies = new Cookies(null, { path: '/' })
  const user = useAppSelector((state) => state.userReducer.user)
  const dispatch = useAppDispath()
  const [panelRightOpen, setPanelRightOpen] = useState<boolean>(false)

  const userSideBars: SideBarProps[] = [
    {
      label: 'Home',
      icon: <House />,
      to: ROUTES.HOME,
      role: 'RESIDENT',
    },
    {
      label: 'Package',
      icon: <Package />,
      to: ROUTES.PACKAGES,
      role: 'RESIDENT',
    },
    {
      label: 'Survey',
      icon: <NotebookText />,
      to: ROUTES.SURVEYS,
      role: 'RESIDENT',
    },
    {
      label: 'Apartments',
      icon: <TableCellsMerge />,
      to: ROUTES.APARTMENTS,
      role: 'RESIDENT',
    },
    {
      label: 'Report',
      icon: <Flag />,
      to: ROUTES.REPORTS,
      role: 'RESIDENT',
    },
    {
      label: 'Bill',
      icon: <Receipt />,
      to: ROUTES.BILLS,
      role: ['RESIDENT'],
    },
    {
      label: 'User Admin',
      icon: <UsersRound />,
      to: ROUTES.ADMIN.USERS,
      role: ['ADMIN'],
    },
    {
      label: 'Service Admin',
      icon: <HandPlatter />,
      to: ROUTES.ADMIN.SERVICES,
      role: ['ADMIN'],
    },
    {
      label: 'Package Admin',
      icon: <Package />,
      to: ROUTES.ADMIN.PACKAGES,
      role: ['ADMIN'],
    },
    {
      label: 'Bill Admin',
      icon: <Receipt />,
      to: ROUTES.ADMIN.BILLS,
      role: ['ADMIN'],
    },
    {
      label: 'Survey Admin',
      icon: <NotebookText />,
      to: ROUTES.ADMIN.SURVEYS,
      role: ['ADMIN'],
    },
    {
      label: 'Report Admin',
      icon: <Flag />,
      to: ROUTES.ADMIN.REPORTS,
      role: ['ADMIN'],
    },
    {
      label: 'Ask For Support',
      icon: <MessageCircleQuestion />,
      to: ROUTES.CHAT,
      role: ['RESIDENT'],
    },
    {
      label: 'Setting Admin',
      icon: <Cog />,
      to: ROUTES.ADMIN.SETTINGS,
      role: ['ADMIN'],
    },
  ]

  const filteredSidebars = useMemo(() => {
    if (!user?.userType) return []

    return userSideBars.filter((sidebar) => {
      if (!sidebar.role) return true

      if (Array.isArray(sidebar.role)) {
        return sidebar.role.includes(user.userType)
      }

      return sidebar.role === user.userType
    })
  }, [user?.userType])

  const handleLogOut = () => {
    cookies.remove('accessToken')
    cookies.remove('refreshToken')
    dispatch(userLoggedOut())
		navigate('/login', { replace: true })
  }

  useEffect(() => {
    if (width >= 1024) {
      setPanelRightOpen(false)
    }
    if (width <= 768) {
      setPanelRightOpen(false)
    }
  }, [width])

  return (
    <header
      className={`${
        panelRightOpen ? 'md:w-[60px]' : 'md:w-[300px]'
      } transition-all duration-300 w-full h-20 md:h-screen sticky top-0 z-40 flex md:flex-row flex-col bg-white`}>
      <div className="w-full h-full flex md:flex-col flex-row md:items-stretch items-center md:justify-start justify-between md:p-0 p-4">
        <div
          className={`md:w-full h-full md:h-[150px] md:p-3 md:order-none order-2 relative`}>
          <img
            src={panelRightOpen ? LogoMobile : Logo}
            onClick={() => navigate(`${user?.userType === 'ADMIN' ? '/admin' : '/'}`)}
            loading="lazy"
            alt="Logo website"
            className={`w-full h-full object-contain aspect-square cursor-pointer ${
              panelRightOpen && 'mt-5'
            }`}
          />
          {width > 768 && (
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
                  {panelRightOpen ? 'Open menu' : 'Close menu'}
                </TooltipContent>
              </Tooltip>
            </Button>
          )}
        </div>
        {width <= 768 && <MobileMenu sidebar={filteredSidebars} handleLogout={handleLogOut} />}
        {width > 768 && <Separator />}
        <div
          className={`sidebar w-full h-full hidden md:flex flex-col overflow-y-auto ${
            panelRightOpen ? 'gap-2 p-2' : 'gap-2 p-4'
          }`}>
          {filteredSidebars.map((sideBar, index) => (
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
        {width > 768 && <Separator />}
        <div
          className={`w-full p-3 hidden ${
            panelRightOpen ? 'flex-col' : 'flex-row'
          } md:flex items-center gap-2`}>
          <Avatar>
            <AvatarImage src={user?.avatar} />
            <AvatarFallback>CN</AvatarFallback>
          </Avatar>
          <div
            className={`w-full ${panelRightOpen ? 'hidden' : 'flex'} flex-col`}>
            <span className="text-sm font-bold">{user?.fullName}</span>
            <span className="text-xs">{user?.userType}</span>
          </div>
          <div className={`flex justify-end`}>
            <Tooltip>
              <TooltipTrigger asChild>
                <Button
                  onClick={() => handleLogOut()}
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
      {width > 768 ? (
        <Separator orientation="vertical" />
      ) : (
        <Separator orientation="horizontal" />
      )}
    </header>
  )
}

export default Header
