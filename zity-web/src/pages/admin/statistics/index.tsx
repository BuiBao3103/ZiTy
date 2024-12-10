import Breadcrumb from '@/components/breadcrumb'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import StatisticsRevenue from './components/statitstics-revenue'
import { useState } from 'react'
import { useStatisticsRevenueQuery } from '@/features/bill/billSlice'
import { useDebounceCallback } from 'usehooks-ts'
import { Statistic } from '@/schema/statistic.validate'
import { ChartConfig } from '@/components/ui/chart'

const Index = () => {
  const [startDate, setStartDate] = useState<string>('2024-01')
  const [endDate, setEndDate] = useState<string>('2025-12')
  const debounceStartDate = useDebounceCallback(setStartDate, 500)
  const debounceEndDate = useDebounceCallback(setEndDate, 500)
  const { data, isLoading, isError, isFetching } = useStatisticsRevenueQuery({ startDate, endDate })

  const transformChartData = (data?: Statistic[]) => {
    return data?.map((item, index) => {
      return {
        month: item.month,
        totalRevenue: item.totalRevenue,
        fill: `hsl(var(--chart-${index + 1}))`,
      }
    })
  }
  const transformChartConfig = (data?: Statistic[]): ChartConfig => {
    return (
      data?.reduce((config, item, index) => {
        config[item.month] = {
          label: item.month,
          color: `hsl(var(--chart-${index + 1}))`,
        }
        return config
      }, {} as ChartConfig) || ({} as ChartConfig)
    )
  }

	console.log(startDate, endDate)

  return (
    <div className="w-full sm:h-screen h-full flex flex-col bg-gray-100">
      <Breadcrumb paths={[{ label: 'statistics', to: '/admin/statistics' }]} />
      <div className="size-full p-4 flex justify-center items-center overflow-hidden">
        <div className="rounded-md bg-white size-full flex flex-col space-y-4 gap-4 p-4">
          <div className="w-full flex justify-between items-center">
            <p className="w-full text-xl font-medium">Total Revenue</p>
            <div className="w-full flex items-center justify-end gap-4">
              <Label>Start Date</Label>
              <Input
                value={startDate}
                onChange={(e) => debounceStartDate(e.target.value)}
                type="month"
                className="w-48 font-medium"
                placeholder="Start Date"
              />
              <Label>End Date</Label>
              <Input
                value={endDate}
                onChange={(e) => debounceEndDate(e.target.value)}
                type="month"
                className="w-48 font-medium"
                placeholder="End Date"
              />
            </div>
          </div>
          <div className="size-full flex justify-center items-center overflow-hidden">
            <StatisticsRevenue
              data={transformChartData(data)}
              chartConfigData={transformChartConfig(data)}
            />
          </div>
        </div>
      </div>
    </div>
  )
}

export default Index
