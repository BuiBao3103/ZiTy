import { ROUTES } from '@/configs/endpoint'
import { ApartmentUserRole } from '@/enums'
import { useAppSelector } from '@/store'
import { useEffect } from 'react'
import { Outlet, useNavigate } from 'react-router-dom'
import { toast } from 'sonner'

const UserLayout = () => {
  const token = useAppSelector((state) => state.authReducer.token)
  const user = useAppSelector((state) => state.userReducer.user)
  const navigate = useNavigate()

  useEffect(() => {
    if (!token) return

    if (user) {
      const { userType, relationships } = user

      // Helper function to check if any relationship has a specific role
      const hasRole = (role: ApartmentUserRole) =>
        relationships?.some((relationship) => relationship.role === role)

      // Check if the user is not a regular user
      if (userType !== 'RESIDENT') {
        navigate(ROUTES.ADMIN.HOME)
        toast.error('You are not authorized to access this page')
        return
      }

      // Check if a non-owner user is trying to access non-bills pages
      if (relationships && !hasRole('OWNER') && location.pathname !== ROUTES.BILLS) {
        navigate(ROUTES.BILLS)
        toast.error('You are not authorized to access this page')
      }
    }
  }, [navigate, user, location.pathname, token])

  return <Outlet />
}

export default UserLayout
