import PieChartItem from '@/components/chart/PieChartItem'
import { Button } from '@/components/ui/button'
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from '@/components/ui/dialog'
import { Progress } from '@/components/ui/progress'
import { ISurvey } from '@/schema/survey.validate'
import { ChartBarBig } from 'lucide-react'
import { useState } from 'react'

interface StatisticsSurveyProps {
  survey?: ISurvey
}

const StatisticsSurvey = ({ survey }: StatisticsSurveyProps) => {
  const [open, setOpen] = useState<boolean>(false)

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger asChild>
        <Button variant="info" className="gap-2">
          <ChartBarBig />
          Statistics
        </Button>
      </DialogTrigger>
      <DialogContent className="xl:max-w-7xl">
        <DialogHeader>
          <DialogTitle>{survey?.title}</DialogTitle>
          <DialogDescription>
            This information is gathered from the poll to demonstrate whether
            users are experiencing any issues.
          </DialogDescription>
          <div className="w-full overflow-hidden flex flex-col space-y-4">
            <div className="w-full grid grid-cols-2 grid-rows-2 divide-x divide-y divide-black">
              <p className="row-start-1 col-start-1 text-muted-foreground p-2 border-l border-black border-t">
                Start Date:{' '}
                <span className="font-medium text-black">
                  {' '}
                  {survey?.startDate &&
                    new Date(survey.startDate).toLocaleDateString('vi-VN', {
                      day: '2-digit',
                      month: '2-digit',
                      year: 'numeric',
                      hour: '2-digit',
                      minute: '2-digit',
                      second: '2-digit',
                      hour12: false,
                    })}
                </span>
              </p>
              <p className="row-start-2 col-start-1 text-muted-foreground p-2 !border-b border-black">
                End Date:{' '}
                <span className="font-medium text-black">
                  {' '}
                  {survey?.endDate &&
                    new Date(survey.endDate).toLocaleDateString('vi-VN', {
                      day: '2-digit',
                      month: '2-digit',
                      year: 'numeric',
                      hour: '2-digit',
                      minute: '2-digit',
                      second: '2-digit',
                      hour12: false,
                    })}
                </span>
              </p>
              <p className="col-start-2 row-start-1 text-muted-foreground p-2 !border-r border-black	">
                Number of questions:{' '}
                <span className="font-medium text-black">
                  {survey?.totalQuestions}
                </span>
              </p>
              <p className="col-start-2 row-start-2 text-muted-foreground p-2 !border-r !border-b border-black	">
                Number of participants:{' '}
                <span className="font-medium text-black">10</span>
              </p>
            </div>
            <div className="w-full h-[600px] flex flex-col gap-4 overflow-y-auto overflow-x-hidden">
              {Array.from({ length: 5 }).map((_, index) => (
                <div className="p-2 border border-black w-full flex justify-between">
                  <div className="w-3/5 flex flex-col space-y-1">
                    <h3 className="font-semibold text-xl">
                      Question {index + 1}
                    </h3>
                    <p className="text-black">
                      Do nisi aute dolore nulla incididunt laborum amet fugiat
                      sit culpa.
                    </p>
                    <div className="flex flex-col space-y-2">
                      {Array.from({ length: 5 }).map((_, index) => (
                        <div className="flex flex-col space-y-1">
                          <div
                            className="w-auto text-nowrap font-medium"
                            style={{
                              color: `hsl(var(--chart-${index + 1}))`,
                            }}>
                            Option {index + 1}
                          </div>
                          <Progress
                            className="h-3 bg-zinc-200"
                            backgroundColorIndicator={`hsl(var(--chart-${index + 1}))`}
                            value={Math.floor(Math.random() * 100)}
                          />
                        </div>
                      ))}
                    </div>
                  </div>
                  <div className="w-2/5 flex justify-center">
                    <PieChartItem />
                  </div>
                </div>
              ))}
            </div>
          </div>
        </DialogHeader>
      </DialogContent>
    </Dialog>
  )
}

export default StatisticsSurvey
