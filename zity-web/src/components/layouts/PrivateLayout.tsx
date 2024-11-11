import { Outlet, Navigate, useLocation } from 'react-router-dom'
import Cookies from 'universal-cookie'
import { Toaster } from '@components/ui/toaster'
const PrivateRoute = () => {
  const cookies = new Cookies(null, { path: '/' })
  const token = cookies.get('accessToken')
  const location = useLocation()

  return (
    <>
      {!token ? (
        <Navigate to="/login" state={{ from: location.pathname }} />
      ) : (
        <Outlet />
      )}
      <Toaster />
      
    </>
  )
}

export default PrivateRoute
