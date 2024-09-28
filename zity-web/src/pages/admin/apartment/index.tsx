import { useDocumentTitle } from 'usehooks-ts'
import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbPage,
  BreadcrumbSeparator,
} from '@/components/ui/breadcrumb'
import { Button } from '@/components/ui/button'
import ApartmentList from './components/apartment-list'
import ApartmentForm from './components/apartment-form'
import { useState } from 'react'
import { Link, useParams } from 'react-router-dom'
import ApartmentDetail from './components/apartment-detail'
const Index = () => {
  const params = useParams()
  useDocumentTitle('Apartment')

  return (
    <div className="w-full h-full flex flex-col bg-zinc-100">
      <div className="w-full px-4 pt-4">
        <Breadcrumb className="p-4 font-medium bg-white rounded-md">
          <BreadcrumbList>
            <BreadcrumbItem>
              <BreadcrumbLink asChild>
                <Link to={'/'}>Home</Link>
              </BreadcrumbLink>
            </BreadcrumbItem>
            <BreadcrumbSeparator />
            {!params.id && (
              <BreadcrumbItem>
                <BreadcrumbPage>Apartment</BreadcrumbPage>
              </BreadcrumbItem>
            )}
            {params.id && (
              <>
                <BreadcrumbItem>
                  <BreadcrumbLink asChild>
                    <Link to={'/apartment'}>Apartment</Link>
                  </BreadcrumbLink>
                </BreadcrumbItem>
                <BreadcrumbSeparator />
                <BreadcrumbItem>
                  <BreadcrumbPage>{params.id}</BreadcrumbPage>
                </BreadcrumbItem>
              </>
            )}
          </BreadcrumbList>
        </Breadcrumb>
      </div>
      <div className="w-full h-full p-4">
        <div className="bg-white w-full h-full rounded-md p-4">
          {!params.id && (
            <>
              <section className="w-full flex flex-col sm:flex-row sm:gap-0 gap-4	 justify-between items-center">
                <div className="w-full sm:w-fit rounded-md flex bg-zinc-200 overflow-hidden">
                  {Array.from({ length: 3 }).map((_, index) => (
                    <div
                      key={index}
                      className="w-full text-nowrap sm:w-[120px] font-medium px-4 py-2 bg-transparent hover:bg-primary transition-all cursor-pointer">
                      Block {String.fromCharCode(65 + index)}
                    </div>
                  ))}
                </div>
                <ApartmentForm textTrigger="New Apartment" />
              </section>
              <ApartmentList />
            </>
          )}
          {params.id && <ApartmentDetail />}
        </div>
      </div>
    </div>
  )
}

export default Index
