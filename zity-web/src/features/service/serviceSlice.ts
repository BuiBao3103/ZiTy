import { Service } from '@/schema/service.validate'
import { apiSlice } from '../api/apiSlice'

export const serviceApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getServices: builder.query<Service[], void>({
      query: () => ({
        url: 'services',
        method: 'GET',
      }),
      providesTags: (results) =>
        results
          ? [
              ...results.map(({ id }) => ({ type: 'Service' as const, id })),
              { type: 'Service', id: 'LIST' },
            ]
          : [{ type: 'Service', id: 'LIST' }],
    }),
    getService: builder.query<Service, string>({
      query: (id: string) => ({
        url: `services/${id}`,
        method: 'GET',
      }),
      // providesTags: (result, error, id) => [{ type: 'Service', id }],
    }),
    createService: builder.mutation<
      Service,
      Partial<Service> & Omit<Service, 'id'>
    >({
      query: (body) => ({
        url: 'services',
        method: 'POST',
        body,
      }),
      invalidatesTags: [{ type: 'Service', id: 'LIST' }],
    }),
    updateService: builder.mutation<Service, Service>({
      query: (body) => ({
        url: `services/${body.id}`,
        method: 'PUT',
        body,
      }),
    }),
    patchService: builder.mutation<Service, Partial<Service>>({
      query: (body) => ({
        url: `services/${body.id}`,
        method: 'PATCH',
        body,
      }),
    }),
    deleteService: builder.mutation<void, string>({
      query: (id: string) => ({
        url: `services/${id}`,
        method: 'DELETE',
      }),
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
