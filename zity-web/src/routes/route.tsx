import DefaultLayout from '@/components/layouts/DefaultLayout'
import PrivateLayout from '@/components/layouts/PrivateLayout'
import { createBrowserRouter } from 'react-router-dom'

// Auth Pages
import AuthLayout from '@pages/auth'
import Login from '@pages/auth/login'

// Home Page
import Home from '@pages/home'

// Admin Pages
import HomeAdmin from '@admin/home'
import Apartment from '@admin/apartment'
import UserAdmin from '@admin/user'
import ServiceAdmin from '@admin/service'
import PackageAdmin from '@admin/package'
import BillAdmin from '@admin/bill'
import SurveyAdmin from '@admin/survey'
import ReportAdmin from '@admin/report'
import SettingAdmin from '@admin/setting'

// User Pages
import Package from '@user/package'
import Report from '@user/report'
import Bill from '@user/bill'
import Survey from '@user/survey'
import Chat from '@user/chat'

// Error and Payment pages
import NotFound from '@pages/404'
import MomoPaymentSuccess from '@/pages/notify-payment/MomoPaymentSuccess'
import AdminLayout from '@/components/layouts/AdminLayout'
import UserLayout from '@/components/layouts/UserLayout'

export const route = createBrowserRouter([
  {
    path: '/',
    element: <PrivateLayout />,
    errorElement: <NotFound />,
    children: [
      // User Layout
      {
        element: <DefaultLayout />,
        children: [
          {
            element: <UserLayout />,
            children: [
              { index: true, element: <Home /> },
              {
                path: '/apartments/:id?',
                element: <Apartment />,
              },
              {
                path: '/packages/:id?',
                element: <Package />,
              },
              {
                path: '/reports/:id?',
                element: <Report />,
              },
              {
                path: '/bills/:id?',
                element: <Bill />,
              },
              { path: '/surveys', element: <Survey /> },
              { path: '/chat', element: <Chat /> },
            ],
          },
          // Admin Layout
          {
            path: '/admin',
            element: <AdminLayout />,
            children: [
              { index: true, element: <HomeAdmin /> },
              { path: 'packages', element: <PackageAdmin /> },
              { path: 'bills', element: <BillAdmin /> },
              { path: 'services', element: <ServiceAdmin /> },
              { path: 'users', element: <UserAdmin /> },
              { path: 'surveys/:id?', element: <SurveyAdmin /> },
              { path: 'reports/:id?', element: <ReportAdmin /> },
              { path: 'settings/:id?', element: <SettingAdmin /> },
            ],
          },
        ],
      },
    ],
  },
  // Auth Route
  {
    path: '/login',
    element: <AuthLayout />,
    children: [{ index: true, element: <Login /> }],
  },
  // Payment Route
  { path: '/payment', element: <MomoPaymentSuccess /> },
  // Catch-all NotFound Route
  { path: '*', element: <NotFound /> },
])
