import { Outlet, redirect } from 'react-router-dom'
import Cookies from 'universal-cookie'
import { toast } from 'sonner'
import { Toaster } from '@components/ui/toaster'
import { Toaster as Sonner } from '@components/ui/sonner'
const PrivateRoute = () => {
  return (
    <>
      <Outlet />
      <Toaster />
      <Sonner
        richColors
        theme="light"
        toastOptions={{}}
        closeButton
        position="top-center"
        visibleToasts={4}
      />
    </>
  )
}

export default PrivateRoute

export const loader = () => {
  // const cookie = new Cookies(null, { path: '/' })
  // if (!cookie.get('accessToken')) {
  //   return redirect('/login')
  // }
  return null
}
