import { IPackage } from '@/schema/package.validate'
import { apiSlice } from '../api/apiSlice'

const packageSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getPackages: builder.query<ResponseDataType<IPackage>, {
			page?: number
			pageSize?: number
			includes?: string[]
			sort?: string[]
		}>({
      query: (params = {page: 1,pageSize: 1,}) => {
				let url = '/items'
				if (params.page) {
					url += `?page=${params.page}`
				}
				if (params.pageSize) {
					url += `&pageSize=${params.pageSize}`
				}
				if (params.includes && params.includes.length > 0) {
					url += `&includes=${params.includes.join(',')}`
				}
				if (params.sort && params.sort.length > 0) {
					url += `&sort=${params.sort.join(',')}`
				}
				return {
					url: url
				}
			},
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
