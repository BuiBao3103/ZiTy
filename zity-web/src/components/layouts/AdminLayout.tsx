import { useAppSelector } from '@/store'
import { useEffect } from 'react'
import { Outlet, useNavigate } from 'react-router-dom'
import { toast } from 'sonner'

const AdminLayout = () => {
  const token = useAppSelector((state) => state.authReducer.token)
  const user = useAppSelector((state) => state.userReducer.user)
  const navigate = useNavigate()

  useEffect(() => {
    if (user && user?.userType !== 'ADMIN') {
      if (user?.userType === 'RESIDENT') {
        navigate('/')
      } else {
        navigate('/admin')
      }
      toast.error('You are not authorized to access this page')
    }
  }, [navigate, token, user])

  return <Outlet />
}

export default AdminLayout
