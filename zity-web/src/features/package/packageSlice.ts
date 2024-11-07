import { IPackage } from '@/schema/package.validate'
import { apiSlice } from '../api/apiSlice'

const packageSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getPackages: builder.query<ResponseDataType<IPackage>, number | void>({
      query: (page = 1) => `/items?page=${page}`,
    }),
    getPackage: builder.query<IPackage, string>({
      query: (id) => `/items/${id}`,
    }),
    updatePackage: builder.mutation<IPackage, IPackage>({
      query: (data) => ({
        url: `/items/${data.id}`,
        method: 'PUT',
        body: data,
      }),
    }),
    createPackage: builder.mutation<IPackage, IPackage>({
      query: (data) => ({
        url: '/items',
        method: 'POST',
        body: data,
      }),
    }),
    deletePackage: builder.mutation<void, string>({
      query: (id) => ({
        url: `/items/${id}`,
        method: 'DELETE',
      }),
    }),
    patchPackage: builder.mutation<
      IPackage,
      { id: string; data: Partial<IPackage> }
    >({
      query: ({ id, data }) => ({
        url: `/items/${id}`,
        method: 'PATCH',
        body: data,
      }),
    }),
  }),
})

export const {
  useGetPackagesQuery,
  useCreatePackageMutation,
  useDeletePackageMutation,
  useUpdatePackageMutation,
  useGetPackageQuery,
} = packageSlice
