import { apiSlice } from '../api/apiSlice'
import { IApartment } from '@/schema/apartment.validate'

export const apartmentSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getApartments: builder.query<
      ResponseDataType<IApartment>,
      {
        page?: number
        pageSize?: number
        id?: number
        includes?: string[]
      }
    >({
      query: (params = {}) => {
        let url = `apartments?page=${params.page}`
        if (params.pageSize) {
          url += `&PageSize=${params.pageSize}`
        }
        if (params.id) {
          url += `$id=eq:${params.id}`
        }
        if (params.includes && params.includes?.length > 0) {
          url += `&includes=${params.includes.join(',')}`
        }
        return {
          url: url,
        }
      },
      providesTags: (result) =>
        result
          ? [
              ...result.contents.map(({ id }) => ({
                type: 'Apartments' as const,
                id,
              })),
              { type: 'Apartments', id: 'LIST' },
            ]
          : [{ type: 'Apartments', id: 'LIST' }],
    }),
    getApartment: builder.query<IApartment, string | undefined>({
      query: (id: string) => ({
        url: `apartments/${id}`,
        method: 'GET',
      }),
      providesTags: (result, error, id) =>
        result ? [{ type: 'Apartments', id }] : [],
    }),
    updateApartment: builder.mutation<
      void,
      { id: string; body: Partial<IApartment> }
    >({
      query: (data) => ({
        url: `apartments/${data.id}`,
        method: 'PUT',
        body: data.body,
      }),
      invalidatesTags: (result, error, arg) => [
        { type: 'Apartments', id: arg.id },
      ],
    }),
    deleteApartment: builder.mutation<void, string | undefined>({
      query: (id: string) => ({
        url: `apartments/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: (result, error, id) => [{ type: 'Apartments', id }],
    }),
  }),
})

export const {
  useGetApartmentsQuery,
  useGetApartmentQuery,
  useUpdateApartmentMutation,
  useDeleteApartmentMutation,
  useLazyGetApartmentQuery,
} = apartmentSlice
