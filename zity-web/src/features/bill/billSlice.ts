import { IBill } from '@/schema/bill.validate'
import { apiSlice } from '../api/apiSlice'

export const billSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getBills: builder.query<
      ResponseDataType<IBill>,
      {
        page?: number
        pageSize?: number
        id?: number
        includes?: string[]
        relationshipId?: number
      }
    >({
      query: (params = { page: 1 }) => {
        let url = `bills?page=${params.page}`
        if (params.pageSize) {
          url += `&PageSize=${params.pageSize}`
        }
        if (params.relationshipId) {
          url += `&relationshipId=eq:${params.relationshipId}`
        }
        if (params.id) {
          url += `&id=eq:${params.id}`
        }
        if (params.includes && params.includes.length > 0) {
          url += `&includes=${params.includes.join(',')}`
        }
        return {
          url: url,
        }
      },
      providesTags: (results) =>
        results
          ? [
              ...results.contents.map(({ id }) => ({
                type: 'Bills' as const,
                id,
              })),
              { type: 'Bills', id: 'LIST' },
            ]
          : [{ type: 'Bills', id: 'LIST' }],
    }),
    getBill: builder.query<IBill, number | undefined | string>({
      query: (id) => ({
        url: `bills/${id}?includes=Relationship`,
      }),
      providesTags: (result, error, id) => [{ type: 'Bills', id }],
    }),
    updateBill: builder.mutation<void, { id?: number; body: Partial<IBill> }>({
      query: (data) => ({
        url: `bills/${data.id}`,
        method: 'PATCH',
        body: data.body,
      }),
      invalidatesTags: (result, error, { id }) => [{ type: 'Bills', id }],
    }),
    deleteBill: builder.mutation<void, number | undefined>({
      query: (id) => ({
        url: `bills/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: (result, error, id) => [{ type: 'Bills', id }],
    }),
    paidByMomo: builder.mutation<
      void,
      {
        id?: string
        body: { RequestType?: string }
      }
    >({
      query: (data) => ({
        url: `bills/${data.id}/payment/momo`,
        method: 'POST',
        body: data.body,
      }),
    }),
    paidByVnpay: builder.mutation<void, { id?: string }>({
      query: (data) => ({
        url: `bills/${data.id}/payment/vnpay`,
        method: 'POST',
      }),
    }),
  }),
})

export const {
  usePaidByMomoMutation,
  usePaidByVnpayMutation,
  useGetBillsQuery,
  useGetBillQuery,
  useUpdateBillMutation,
  useDeleteBillMutation,
} = billSlice
