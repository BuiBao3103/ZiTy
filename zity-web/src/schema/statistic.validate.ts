import z from 'zod'

export const StatisticSchema = z.object({
	month: z.string(),
	totalRevenue: z.number(),
})
export type Statistic = z.infer<typeof StatisticSchema>