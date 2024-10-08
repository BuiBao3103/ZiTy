import { Bill } from '@/schema/bill.validate'
import { apiSlice } from '../api/apiSlice'

export const billSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getBills: builder.query<void, void>({
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
    // createBill: builder.mutation<void, void>({
    //   query: () => ({
    //     url: 'bills',
    //     method: 'POST',
    //   }),
    // }),
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
  }),
})
