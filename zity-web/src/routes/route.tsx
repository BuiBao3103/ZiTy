import DefaultLayout from '@/components/layouts/DefaultLayout'
import PrivateLayout, {
  loader as PrivateLoader,
} from '@/components/layouts/PrivateLayout'
import { createBrowserRouter } from 'react-router-dom'

//Auth Pages
import AuthLayout from '@pages/auth'
import Login from '@pages/auth/login'

//Home Page
import Home from '@pages/home'

//Admin Page
import Apartment from '@admin/apartment'
import User from '@admin/user'

//User Page
import Package from '@user/package'
import Report from '@user/report'

//Error page
import NotFound from '@pages/404'
import ToastLayout from '@/components/layouts/ToastLayout'
export const route = createBrowserRouter([
  {
    path: '/',
    element: <ToastLayout />,
    errorElement: <NotFound />,
    children: [
      {
        element: <PrivateLayout />,
        loader: PrivateLoader,
        children: [
          {
            element: <DefaultLayout />,
            children: [
              {
                index: true,
                element: <Home />,
              },
              {
                path: '/apartment',
                element: <Apartment />,
                children: [
                  {
                    path: ':id',
                    element: <Apartment />,
                  },
                ],
              },
              {
                path: '/package',
                element: <Package />,
                children: [
                  {
                    path: ':id',
                    element: <Package />,
                  },
                ],
              },
              {
                path: '/report',
                element: <Report />,
                children: [
                  {
                    path: ':id',
                    element: <Report />,
                  },
                ],
              },
              {
                path: '/user',
                element: <User />,
                children: [
                  {
                    path: ':id',
                    element: <User />,
                  },
                ],
              },
            ],
          },
        ],
      },
      {
        path: '/login',
        element: <AuthLayout />,
        children: [
          {
            index: true,
            element: <Login />,
          },
        ],
      },
    ],
  },
  {
    path: '*',
    element: <NotFound />,
  },
])
