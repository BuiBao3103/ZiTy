import { Outlet, Navigate, useLocation } from 'react-router-dom'
import Cookies from 'universal-cookie'
import { Toaster } from '@components/ui/toaster'
import { Toaster as Sonner } from '@components/ui/sonner'
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
      <Sonner
        richColors
        theme="light"
        toastOptions={{}}
        closeButton
        visibleToasts={4}
      />
    </>
  )
}

export default PrivateRoute
