import { Button } from '@/components/ui/button'
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from '@/components/ui/dialog'
import { ChartBarBig } from 'lucide-react'
import React from 'react'

const StatisticsSurvey = () => {
  return (
    <Dialog>
      <DialogTrigger asChild>
        <Button variant="info" className="gap-2">
          <ChartBarBig />
          Statistics
        </Button>
      </DialogTrigger>
      <DialogContent className="xl:max-w-7xl">
        <DialogHeader>
          <DialogTitle>Survey Statistics</DialogTitle>
          <DialogDescription>
            This dialog provides a comprehensive overview of key metrics and
            insights to help you monitor and analyze surveys.
          </DialogDescription>
        </DialogHeader>
        
      </DialogContent>
    </Dialog>
  )
}

export default StatisticsSurvey
