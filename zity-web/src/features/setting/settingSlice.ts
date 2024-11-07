import { ISetting } from '@/schema/setting.validate'
import { apiSlice } from '../api/apiSlice'

const settingApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getSettings: builder.query<ResponseDataType<ISetting[]>, void>({
      query: () => 'settings',
    }),
    getSetting: builder.query<ResponseDataType<ISetting>, number | void>({
      query: (id) => `settings/${id}`,
    }),
    patchSetting: builder.mutation<ISetting, Partial<ISetting>>({
      query: (data) => ({
        url: 'settings',
        method: 'PATCH',
        body: data,
      }),
    }),
  }),
})

export const {
  useGetSettingsQuery,
  useGetSettingQuery,
  usePatchSettingMutation,
} = settingApiSlice
