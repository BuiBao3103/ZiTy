import { IPackage } from '@/schema/package.validate'
import { apiSlice } from '../api/apiSlice'
const packageSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getPackages: builder.query<
      ResponseDataType<IPackage>,
      {
        page?: number
        pageSize?: number
        includes?: string[]
        sort?: string[]
      }
    >({
      query: (params = { page: 1, pageSize: 1 }) => {
        let url = 'items'
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
          url: url,
        }
      },
      providesTags: (results) =>
        results
          ? [
              ...results.contents.map(({ id }) => ({
                type: 'Packages' as const,
                id,
              })),
              { type: 'Packages', id: 'LIST' },
            ]
          : [{ type: 'Packages', id: 'LIST' }],
    }),
    getPackage: builder.query<IPackage, number | undefined>({
      query: (id) => `items/${id}`,
      providesTags: (result, error, id) => [{ type: 'Packages', id }],
    }),
    updatePackage: builder.mutation<
      IPackage,
      {
        id: number
        body: Partial<IPackage> & Pick<IPackage, 'description' | 'isReceive'>
      }
    >({
      query: (data) => ({
        url: `items/${data.id}`,
        method: 'PATCH',
        body: data.body,
      }),
      invalidatesTags: (result, error, { id }) => [{ type: 'Packages', id }],
    }),
    updateImagePackage: builder.mutation<void, { id: number; image: FormData }>(
      {
        query: ({ id, image }) => ({
          url: `items/${id}/image`,
          method: 'POST',
          body: image,
          headers: {
            'Content-Type': undefined,
          },
        }),
        invalidatesTags: (result, error, { id }) => [{ type: 'Packages', id }],
      },
    ),
    createPackage: builder.mutation<
      IPackage,
      Pick<IPackage, 'image' | 'description' | 'isReceive'>
    >({
      query: (data) => ({
        url: 'items',
        method: 'POST',
        body: data,
      }),
      invalidatesTags: [{ type: 'Packages', id: 'LIST' }],
    }),
    deletePackage: builder.mutation<void, string>({
      query: (id) => ({
        url: `items/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: [{ type: 'Packages', id: 'LIST' }],
    }),
    patchPackage: builder.mutation<
      IPackage,
      { id: string; data: Partial<IPackage> }
    >({
      query: ({ id, data }) => ({
        url: `items/${id}`,
        method: 'PATCH',
        body: data,
      }),
      invalidatesTags: (result, error, { id }) => [{ type: 'Packages', id }],
    }),
  }),
})

export const {
  useGetPackagesQuery,
  useCreatePackageMutation,
  useDeletePackageMutation,
  useUpdatePackageMutation,
  useGetPackageQuery,
  useUpdateImagePackageMutation,
} = packageSlice
