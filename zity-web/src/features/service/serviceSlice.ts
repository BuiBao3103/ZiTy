import { Service } from '@/schema/service.validate'
import { apiSlice } from '../api/apiSlice'

export const serviceApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getServices: builder.query<ResponseDataType<Service>, number | void>({
      query: (page = 1) => ({
        url: `services?page${page}`,
        method: 'GET',
      }),
      providesTags: (results) =>
        results
          ? [
              ...results.contents.map(({ id }) => ({
                type: 'Service' as const,
                id,
              })),
              { type: 'Service', id: 'LIST' },
            ]
          : [{ type: 'Service', id: 'LIST' }],
    }),
    getService: builder.query<Service, string | number | undefined>({
      query: (id: string) => ({
        url: `services/${id}`,
        method: 'GET',
      }),
      providesTags: (result, error, id) => [{ type: 'Service', id }],
    }),
    createService: builder.mutation<
      Service,
      Omit<Service, 'id' | 'createdAt' | 'updatedAt'>
    >({
      query: (body) => ({
        url: 'services',
        method: 'POST',
        body,
      }),
      invalidatesTags: [{ type: 'Service', id: 'LIST' }],
    }),
    updateService: builder.mutation<
      Service,
      {
        id: number | undefined
        body: Partial<Service> & Omit<Service, 'id' | 'createdAt' | 'updatedAt'>
      }
    >({
      query: (data) => ({
        url: `services/${data.id}`,
        method: 'PUT',
        body: data.body,
      }),
      invalidatesTags: (result, error, { id }) => [{ type: 'Service', id }],
    }),
    patchService: builder.mutation<Service, Partial<Service>>({
      query: (body) => ({
        url: `services/${body.id}`,
        method: 'PATCH',
        body,
      }),
    }),
    deleteService: builder.mutation<void, string | number | undefined>({
      query: (id: string) => ({
        url: `services/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: (result, error, id) => [{ type: 'Service', id }],
    }),
  }),
})

export const {
  useGetServicesQuery,
  useGetServiceQuery,
  useCreateServiceMutation,
  useDeleteServiceMutation,
  usePatchServiceMutation,
  useUpdateServiceMutation,
} = serviceApiSlice
