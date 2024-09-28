import { Outlet, redirect } from 'react-router-dom'
import Cookies from 'universal-cookie'
import { toast } from 'sonner'
const PrivateRoute = () => {
  return <Outlet />
}

export default PrivateRoute

export const loader = () => {
  // const cookie = new Cookies(null, { path: '/' })
  // if (!cookie.get('accessToken')) {
  //   return redirect('/login')
  // }
  return null
}
