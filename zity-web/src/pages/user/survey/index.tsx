import React from 'react'
import BreadCrumb from '@/components/breadcrumb'
import { useParams } from 'react-router-dom'
import { Button } from '@/components/ui/button'
import { AlarmClock, Clock } from 'lucide-react'
const Index = () => {
  const params = useParams()

  return (
    <div className="w-full sm:h-screen flex flex-col bg-zinc-100">
      <BreadCrumb
        paths={[
          { label: 'survey', to: '/survey' },
          ...(params.id ? [{ label: params.id }] : []),
        ]}
      />
      <div className="size-full p-4 overflow-hidden">
        <div className="size-full flex flex-col bg-white rounded-md space-y-4 p-4">
          <div
            className={`w-full h-auto overflow-y-auto grid grid-cols-2 gap-4`}>
            {Array.from({ length: 1 }).map((_, index) => (
              <div
                key={index}
                className="w-full p-4 rounded-md border flex flex-col gap-4">
                <div className="w-full flex justify-between items-center">
                  <div className="w-full space-y-0.5 font-medium">
                    <h1 className="text-xl">Title</h1>
                    <p className="text-sm text-zinc-500">
                      Enim commodo eu ullamco aliqua.Esse esse magna Lorem id ad
                      irure nisi velit.
                    </p>
                    <p>
                      Total Questions: <span>5</span>
                    </p>
                  </div>
                  <div className="w-full flex gap-4 justify-end items-center">
                    <section className="flex gap-2">
                      <span className="w-16 inline-flex rounded-sm bg-zinc-100 justify-center items-center">
                        <Clock />
                      </span>
                      <div className="w-full flex flex-col">
                        <p className="text-sm font-medium text-zinc-500">
                          Start Date
                        </p>
                        <p className="text-sm font-medium">12/12/2021</p>
                      </div>
                    </section>
                    <section className="flex gap-2">
                      <span className="w-16 inline-flex rounded-sm bg-zinc-100 justify-center items-center">
                        <AlarmClock />
                      </span>
                      <div className="w-full flex flex-col">
                        <p className="text-sm font-medium text-zinc-500">
                          Due Date
                        </p>
                        <p className="text-sm font-medium">12/12/2021</p>
                      </div>
                    </section>
                  </div>
                </div>
                <div className="w-full flex gap-4">
                  <Button>Start</Button>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  )
}

export default Index
