import { IRejectionReason } from '@/schema/report.validate'
import { apiSlice } from '../api/apiSlice'

export const rejectedReasonsSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getRejectionReasons: builder.query<ResponseDataType<IRejectionReason>, number | void>({
      query: (page = 1) => {
        return {
          url: `rejectionReasons?page=${page}`,
        }
      },
      providesTags: (result) =>
        result
          ? [
              ...result.contents.map(({ id }) => ({
                type: 'RejectionReasons' as const,
                id,
              })),
              { type: 'RejectionReasons', id: 'LIST' },
            ]
          : [{ type: 'RejectionReasons', id: 'LIST' }],
    }),
    getRejectionReason: builder.query<IRejectionReason, string | number>({
      query: (id: string) => ({
        url: `rejected-reasons/${id}`,
        method: 'GET',
      }),
      providesTags: (result, error, id) => (result ? [{ type: 'RejectionReasons', id }] : []),
    }),
    createRejectionReason: builder.mutation<
      IRejectionReason,
      Partial<IRejectionReason> & Omit<IRejectionReason, 'id'>
    >({
      query: (data) => ({
        url: 'rejectionReasons',
        method: 'POST',
        body: data,
      }),
      invalidatesTags: [{ type: 'RejectionReasons', id: 'LIST' }],
    }),
    updateRejectionReason: builder.mutation<
      void,
      { id: string | number | undefined; body: Partial<IRejectionReason> }
    >({
      query: (data) => ({
        url: `rejectionReasons/${data.id}`,
        method: 'PATCH',
        body: data.body,
      }),
      invalidatesTags: (result, error, arg) => [{ type: 'RejectionReasons', id: arg.id }],
    }),
    deleteRejectionReason: builder.mutation<void, string | number | undefined>({
      query: (id: string) => ({
        url: `rejectionReasons/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: (result, error, id) => [{ type: 'RejectionReasons', id }],
    }),
  }),
})

export const { useCreateRejectionReasonMutation } = rejectedReasonsSlice
