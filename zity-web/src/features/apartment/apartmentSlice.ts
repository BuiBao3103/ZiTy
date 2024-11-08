import { apiSlice } from '../api/apiSlice'
import { Apartment } from '@/schema/apartment.validate'

export const apartmentSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getApartments: builder.query<ResponseDataType<Apartment>, number | void>({
      query: (page = 1) => {
        return {
          url: `apartments?page=${page}`,
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
    getApartment: builder.query<Apartment, string | undefined>({
      query: (id: string) => ({
        url: `apartments/${id}`,
        method: 'GET',
      }),
      providesTags: (result, error, id) =>
        result ? [{ type: 'Apartments', id }] : [],
    }),
    updateApartment: builder.mutation<
      void,
      { id: string; body: Partial<Apartment> }
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
	useLazyGetApartmentQuery
} = apartmentSlice
