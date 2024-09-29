import { ReportStatus } from '@/enums'

export interface Report {
  id: number
  content: string
  title: string
  status: ReportStatus
  relationship_id?: number
}

export interface RejectionReasons {
  id: number
  content: string
  report_id?: number | null
}
