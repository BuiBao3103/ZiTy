import { Report } from '@/schema/report.validate'
import { apiSlice } from '../api/apiSlice'

export const reportsSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getReports: builder.query<ResponseDataType<Report>, number | void>({
      query: (page = 1) => {
        return {
          url: `reports?page=${page}`,
        }
      },
      providesTags: (result) =>
        result
          ? [
              ...result.contents.map(({ id }) => ({
                type: 'Reports' as const,
                id,
              })),
              { type: 'Reports', id: 'LIST' },
            ]
          : [{ type: 'Reports', id: 'LIST' }],
    }),
    getReport: builder.query<Report, string | number>({
      query: (id: string) => ({
        url: `reports/${id}`,
        method: 'GET',
      }),
      providesTags: (result, error, id) =>
        result ? [{ type: 'Reports', id }] : [],
    }),
    createReport: builder.mutation<Report, Partial<Report>>({
      query: (data) => ({
        url: 'reports',
        method: 'POST',
        body: data,
      }),
      invalidatesTags: [{ type: 'Reports', id: 'LIST' }],
    }),
    updateReport: builder.mutation<void, { id: string | number | undefined; body: Partial<Report> }>(
      {
        query: (data) => ({
          url: `reports/${data.id}`,
          method: 'PUT',
          body: data.body,
        }),
        invalidatesTags: (result, error, arg) => [
          { type: 'Reports', id: arg.id },
        ],
      },
    ),
    deleteReport: builder.mutation<void, string | number | undefined>({
      query: (id: string) => ({
        url: `reports/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: (result, error, id) => [{ type: 'Reports', id }],
    }),
  }),
})

export const {
  useGetReportsQuery,
  useGetReportQuery,
  useUpdateReportMutation,
  useDeleteReportMutation,
  useCreateReportMutation,
} = reportsSlice
