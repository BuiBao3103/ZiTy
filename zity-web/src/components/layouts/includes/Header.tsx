import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar'
import { Button } from '@/components/ui/button'
import { Separator } from '@/components/ui/separator'
import DefaultAvatar from '@/assets/default-avatar.jpeg'
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
import { ApartmentUserRole, UserRole } from '@/enums'
import { useCallback, useMemo, useState } from 'react'
import { useAppDispath, useAppSelector } from '@/store'
import { ROUTES } from '@/configs/endpoint'
import Cookies from 'universal-cookie'
import { userLoggedOut } from '@/features/auth/authSlice'
export interface SideBarProps {
  label: string
  icon: React.ReactNode
  to: string
  role?: UserRole[] | UserRole | ApartmentUserRole
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
      label: 'Packages',
      icon: <Package />,
      to: ROUTES.PACKAGES,
      role: 'RESIDENT',
    },
    {
      label: 'Surveys',
      icon: <NotebookText />,
      to: ROUTES.SURVEYS,
      role: 'RESIDENT',
    },
    {
      label: 'Reports',
      icon: <Flag />,
      to: ROUTES.REPORTS,
      role: 'RESIDENT',
    },
    {
      label: 'Bills',
      icon: <Receipt />,
      to: ROUTES.BILLS,
      role: ['RESIDENT'],
    },
    {
      label: 'Home',
      icon: <House />,
      to: ROUTES.ADMIN.HOME,
      role: ['ADMIN'],
    },
    {
      label: 'Apartments Management',
      icon: <TableCellsMerge />,
      to: ROUTES.ADMIN.APARTMENTS,
      role: ['ADMIN'],
    },
    {
      label: 'Users Management',
      icon: <UsersRound />,
      to: ROUTES.ADMIN.USERS,
      role: ['ADMIN'],
    },
    {
      label: 'Services Management',
      icon: <HandPlatter />,
      to: ROUTES.ADMIN.SERVICES,
      role: ['ADMIN'],
    },
    {
      label: 'Packages Management',
      icon: <Package />,
      to: ROUTES.ADMIN.PACKAGES,
      role: ['ADMIN'],
    },
    {
      label: 'Bills Management',
      icon: <Receipt />,
      to: ROUTES.ADMIN.BILLS,
      role: ['ADMIN'],
    },
    {
      label: 'Surveys Management',
      icon: <NotebookText />,
      to: ROUTES.ADMIN.SURVEYS,
      role: ['ADMIN'],
    },
    {
      label: 'Reports Management',
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
    {
      label: 'Chat',
      icon: <MessageCircleQuestion />,
      to: ROUTES.ADMIN.CHAT,
      role: ['ADMIN'],
    },
  ]

  const filteredSidebars = useMemo(() => {
    if (!user?.userType) return []

    return userSideBars.filter((sidebar) => {
      // If the user is a RESIDENT and has an OWNER role in relationships
      if (
        user.userType === 'RESIDENT' &&
        user?.relationships?.[0]?.role === 'OWNER'
      ) {
        return (
          sidebar.role === 'RESIDENT' ||
          (Array.isArray(sidebar.role) && sidebar.role.includes('RESIDENT'))
        )
      }
      // If the user is a RESIDENT but not an OWNER, only return the 'Bill' element
      if (
        user.userType === 'RESIDENT' &&
        user?.relationships?.[0]?.role !== 'OWNER'
      ) {
        return sidebar.label === 'Bills'
      }
      // Otherwise, filter normally based on role matching
      if (Array.isArray(sidebar.role)) {
        return sidebar.role.includes(user.userType)
      }
      return sidebar.role === user.userType
    })
  }, [user?.userType, user?.relationships?.[0]?.role])

  const handleLogout = () => {
    cookies.remove('accessToken')
    cookies.remove('refreshToken')
    dispatch(userLoggedOut())
    navigate('/login', { replace: true })
  }

  const isActiveRoute = (route: string) => {
    if (route === ROUTES.ADMIN.HOME) {
      return location.pathname === ROUTES.ADMIN.HOME
    }
    if (route === ROUTES.HOME) {
      return location.pathname === ROUTES.HOME
    }
    return (
      location.pathname.startsWith(route) &&
      route !== ROUTES.ADMIN.HOME &&
      route !== ROUTES.HOME
    )
  }

  return (
    <header
      className={`${
        panelRightOpen ? 'md:w-[60px]' : 'md:w-[300px]'
      } transition-all duration-300 w-full h-20 md:h-screen sticky top-0 z-40 flex md:flex-row flex-col bg-white`}>
      <div className="w-full h-full flex md:flex-col flex-row md:items-stretch items-center md:justify-start justify-between md:p-0 p-4">
        <div
          className={`md:w-full h-full md:h-[150px] p-1 md:p-3 md:order-none order-2 relative`}>
          <img
            src={panelRightOpen && width > 768 ? LogoMobile : Logo}
            onClick={() =>
              navigate(`${user?.userType === 'ADMIN' ? '/admin' : '/'}`)
            }
            loading="lazy"
            alt="Logo website"
            className={`size-full object-contain aspect-square cursor-pointer ${
              panelRightOpen && width > 768 ? 'mt-5' : ''
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
        {width <= 768 && (
          <MobileMenu sidebar={filteredSidebars} handleLogout={handleLogout} />
        )}
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
              } ${isActiveRoute(sideBar.to) ? 'bg-primary' : ''}`}>
              {panelRightOpen ? (
                <Tooltip>
                  <TooltipTrigger asChild>
                    <Link
                      to={sideBar.to}
                      className={`w-full p-2 flex justify-center items-center rounded-md hover:bg-zinc-100 transition-all ${
                        isActiveRoute(sideBar.to) ? 'bg-primary' : ''
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
            <AvatarImage src={user?.avatar ?? DefaultAvatar} />
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
                  onClick={() => handleLogout()}
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
