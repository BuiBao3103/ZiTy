import { Button } from '@/components/ui/button'
import React from 'react'
import { useNavigate } from 'react-router-dom'

const ApartmentList = () => {
  const navigate = useNavigate()

  return (
    <div className="w-full flex flex-col gap-4">
      <div className="w-full p-4 border rounded-md bg-zinc-100">
        <h1 className="text-xl font-bold">Floor 1</h1>
        <div className="w-full flex gap-4 mt-4 overflow-x-auto">
          {Array.from({ length: 6 }).map((_, index) => (
            <div
              key={index}
              className="p-4 min-w-full sm:min-w-[300px] flex flex-col gap-1.5 bg-white rounded-lg border">
              <div className="w-full h-full grid grid-cols-2">
                <span className="text-lg font-medium">A.10{index}</span>
                <Button
                  variant="default"
                  size="sm"
                  onClick={() => {
                    navigate(`/apartment/A.10${index}`)
                  }}
                  className="w-fit place-self-end">
                  Details
                </Button>
              </div>
              <div className="w-full h-full grid grid-cols-2">
                <span className="">Owner:</span>
                <span className="font-medium place-self-end">Bui Hong Bao</span>
              </div>
              <div className="w-full h-full grid grid-cols-2">
                <span className="">User:</span>
                <span className="font-medium place-self-end">
                  Bui Ngoc Thuc
                </span>
              </div>
              <div className="w-full h-full grid grid-cols-2">
                <span className="">Phone:</span>
                <span
                  className="font-medium place-self-end"
                  onClick={() => window.open(`tel:0344248396`)}>
                  0123456789
                </span>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  )
}

export default ApartmentList
