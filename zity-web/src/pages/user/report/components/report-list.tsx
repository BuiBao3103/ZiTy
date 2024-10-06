import { Badge } from '@/components/ui/badge'
import ReportForm from './report-form'
import { Button } from '@/components/ui/button'
import { BadgePlus } from 'lucide-react'
import AlertDelete from '@/components/alert/AlertDelete'

const ReportList = () => {
  const date = new Date()
  const formattedDate =
    ('0' + date.getDate()).slice(-2) +
    '/' +
    ('0' + (date.getMonth() + 1)).slice(-2) +
    '/' +
    date.getFullYear()

  const setAction = () => {
    console.log('delete')
  }
  return (
    <div
      className={`w-full h-full grid grid-cols-1 lg:grid-cols-3 gap-4 overflow-y-auto`}>
      <ReportForm>
        <div className="p-4 w-full h-auto bg-white hover:bg-zinc-100 transition-all cursor-pointer rounded-md flex flex-col justify-center items-center gap-2 border">
          <p className="text-xl font-medium">New Report</p>
          <BadgePlus size={50} />
        </div>
      </ReportForm>
      {Array.from({ length: 10 }).map((_, index) => (
        <div
          key={index}
          className="p-4 w-full h-[310px] bg-white rounded-md flex flex-col gap-4 border">
          <div className="w-full flex justify-between items-center">
            <span className="font-medium">{formattedDate}</span>
            <ReportForm id="1">
              <Button variant={'default'} size={'sm'}>
                Details
              </Button>
            </ReportForm>
          </div>
          <div className="w-full h-full rounded-md bg-zinc-100 p-4">
            <p className="line-clamp-4 font-medium">
              <span className="font-normal">Description:</span> Occaecat aliquip
              aliqua eu labore exercitation ex qui proident magna eiusmod
              excepteur. Qui enim tempor Lorem amet.Fugiat voluptate anim aute
              nostrud elit do voluptate cupidatat ullamco et eiusmod elit enim
              ullamco.Mollit amet anim enim duis pariatur irure aliqua enim
              excepteur labore nulla laborum.
            </p>
          </div>
          <div className="flex gap-2 uppercase">
            <Badge variant={'info'}>Other</Badge>
            <Badge variant={'error'}>Environment</Badge>
            <Badge variant={'warning'}>Noise</Badge>
          </div>
          <div className="w-full flex justify-between items-center">
            <div className="flex gap-2">
              <img
                src="https://picsum.photos/200/300"
                alt="user avatar"
                className="size-12 rounded-full"
              />
              <div className="flex flex-col">
                <p className="font-medium">
                  <span>Bui Ngoc Thuc</span>
                </p>
                <span className="text-sm font-medium ">A.101</span>
              </div>
            </div>
            <AlertDelete description="report" setAction={setAction} variants='error' />
          </div>
        </div>
      ))}
    </div>
  )
}

export default ReportList
