import { useAppSelector } from '@/store'
import { useEffect } from 'react'
import { Outlet, useNavigate } from 'react-router-dom'
import { toast } from 'sonner'

const UserLayout = () => {
  const token = useAppSelector((state) => state.authReducer.token)
  const user = useAppSelector((state) => state.userReducer.user)
  const navigate = useNavigate()

  useEffect(() => {
    if (user && user?.userType !== 'RESIDENT') {
      if (user?.userType === 'ADMIN') {
        navigate('/admin')
      } else {
        navigate('/')
      }
      toast.error('You are not authorized to access this page')
    }
  }, [navigate, token, user])

  return <Outlet />
}

export default UserLayout
