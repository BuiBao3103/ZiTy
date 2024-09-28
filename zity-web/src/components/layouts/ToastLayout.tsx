import React from 'react'
import { Outlet } from 'react-router-dom'
import { Toaster } from '@components/ui/toaster'
import { Toaster as Sonner } from '@components/ui/sonner'

const ToastLayout = () => {
  return (
    <>
      <Outlet />
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

export default ToastLayout
