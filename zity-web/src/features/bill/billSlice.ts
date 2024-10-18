import { Bill } from '@/schema/bill.validate'
import { apiSlice } from '../api/apiSlice'

export const billSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getBills: builder.query<
      {
        contents: Bill[],
        page: 1,
        pageSize: 10,
        totalItems: 28,
        totalPages: 3,
      },
      void
    >({
      query: () => ({
        url: 'bills',
        method: 'GET',
      }),
    }),
    getBill: builder.query<Bill, string>({
      query: (id: string) => ({
        url: `bills/${id}`,
        method: 'GET',
      }),
    }),
    updateBill: builder.mutation<void, { id: string; body: Partial<Bill> }>({
      query: (data) => ({
        url: `bills/${data.id}`,
        method: 'PUT',
        body: data.body,
      }),
    }),
    deleteBill: builder.mutation<void, string>({
      query: (id: string) => ({
        url: `bills/${id}`,
        method: 'DELETE',
      }),
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
} = billSlice
