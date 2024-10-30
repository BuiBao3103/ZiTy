import { RouterProvider } from 'react-router-dom'
import route from './routes/route'
import 'react-day-picker/dist/style.css'
import { useEffect } from 'react'
import Cookies from 'universal-cookie'
import {
  getUserInformation,
  useLazyGetCurrentUserQuery,
} from './features/user/userSlice'
import { useAppDispath } from './store'
import { userLoggedIn } from './features/auth/authSlice'
function App() {
  const cookie = new Cookies(null, { path: '/' })
  const dispatch = useAppDispath()
  const [getCurrentUser, { data: user }] = useLazyGetCurrentUserQuery()
  useEffect(() => {
    const handleGetCurrentUser = async () => {
      if (cookie.get('accessToken')) {
        dispatch(
          userLoggedIn({
            token: cookie.get('accessToken'),
            refreshToken: cookie.get('refreshToken'),
          }),
        )
        await getCurrentUser()
          .unwrap()
          .then((payload) => {
            dispatch(getUserInformation(payload))
          })
          .catch((error) => {
            throw new Error("can't get user information")
          })
      }
    }
    handleGetCurrentUser()
  }, [])

  return <RouterProvider router={route} />
}

export default App
