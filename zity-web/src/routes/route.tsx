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
import Service from '@admin/service'
import PackageAdmin from '@admin/package'

//User Page
import Package from '@user/package'
import Report from '@user/report'
import Bill from '@user/bill'

//Error page
import NotFound from '@pages/404'

export const route = createBrowserRouter([
  {
    path: '/',
    element: <PrivateLayout />,
    loader: PrivateLoader,
    errorElement: <NotFound />,
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
          },
          {
            path: '/bill',
            element: <Bill />,
            children: [
              {
                path: ':id',
                element: <Bill />,
              },
            ],
          },
          {
            path: '/service',
            element: <Service />,
          },
					{
						path: '/admin/package',
						element: <PackageAdmin />,
					}
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
  {
    path: '*',
    element: <NotFound />,
  },
])
