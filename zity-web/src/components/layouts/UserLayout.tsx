import { useAppSelector } from '@/store'
import { useEffect } from 'react'
import { Outlet, useLocation, useNavigate } from 'react-router-dom'
import { toast } from 'sonner'

const UserLayout = () => {
  const token = useAppSelector((state) => state.authReducer.token)
  const user = useAppSelector((state) => state.userReducer.user)
  const location = useLocation()
  const navigate = useNavigate()

  useEffect(() => {
    if (token && !token) {
      navigate('/login', { replace: true })
    }
    if (user && user?.userType !== 'RESIDENT') {
			toast.error('You are not authorized to access this page')
      navigate(-1)
    }
  }, [navigate, token, user])

  return <Outlet />
}

export default UserLayout
