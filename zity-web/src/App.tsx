import { RouterProvider } from 'react-router-dom'
import { route } from './routes/route'
import 'react-day-picker/dist/style.css'
import { useEffect } from 'react'
import Cookies from 'universal-cookie'
import {
  getUserInformation,
  useGetCurrentUserQuery,
} from './features/user/userSlice'
import { useAppDispath } from './store'
function App() {
  // const cookie = new Cookies(null, { path: '/' })
  // const dispatch = useAppDispath()
  // useEffect(() => {
  //   if (cookie.get('accessToken')) {
  //     const { data: user } = useGetCurrentUserQuery()
  //     if (user) {
  //       dispatch(getUserInformation(user))
  //     }
  //   }
  // }, [])

  return <RouterProvider router={route} />
}

export default App
