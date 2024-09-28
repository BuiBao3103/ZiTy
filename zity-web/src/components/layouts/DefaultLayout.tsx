import { Outlet, useLocation } from 'react-router-dom'
import Header from './includes/Header'
import { Toaster } from '@components/ui/toaster'
import { Toaster as Sonner } from '@components/ui/sonner'

const DefaultLayout = () => {
  return (
    <div className="w-full min-h-screen flex sm:flex-row flex-col">
      <Header />
      <main className={`w-full min-h-screen relative overflow-hidden`}>
        <Outlet />
      </main>
      <Toaster />
      <Sonner richColors theme="light" toastOptions={{}} closeButton visibleToasts={4} />
    </div>
  )
}

export default DefaultLayout
