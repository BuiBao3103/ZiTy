import { ISetting } from '@/schema/setting.validate'
import { apiSlice } from '../api/apiSlice'

const settingApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getSettings: builder.query<ISetting, void>({
      query: () => 'settings',
      providesTags: [{ type: 'Settings' }],
    }),
    patchSetting: builder.mutation<ISetting, Partial<ISetting>>({
      query: (data) => ({
        url: 'settings',
        method: 'PATCH',
        body: data,
      }),
      invalidatesTags: (result, error, arg) => [{ type: 'Settings', id: arg.id }],
    }),
    updateTransitionPrepayment: builder.mutation<ISetting, void>({
      query: () => ({
        url: 'settings/transition/prepayment',
        method: 'POST',
      }),
      invalidatesTags(result, error, arg, meta) {
        return [{ type: 'Settings' }, { type: 'Bills' }]
      },
    }),
    updateTransitionPayment: builder.mutation<ISetting, void>({
      query: () => ({
        url: 'settings/transition/payment',
        method: 'POST',
      }),
      invalidatesTags(result, error, arg, meta) {
        return [{ type: 'Settings' }, { type: 'Bills' }]
      },
    }),
    updateTransitionOverdue: builder.mutation<ISetting, void>({
      query: () => ({
        url: 'settings/transition/overdue',
        method: 'POST',
      }),
      invalidatesTags(result, error, arg, meta) {
        return [{ type: 'Settings' }, { type: 'Bills' }]
      },
    }),
    updateTransitionDelinquent: builder.mutation<ISetting, void>({
      query: () => ({
        url: 'settings/transition/delinquent',
        method: 'POST',
      }),
      invalidatesTags(result, error, arg, meta) {
        return [{ type: 'Settings' }, { type: 'Apartments',id: "LIST" }]
      },
    }),
  }),
})

export const {
  useGetSettingsQuery,
  usePatchSettingMutation,
  useUpdateTransitionDelinquentMutation,
  useUpdateTransitionOverdueMutation,
  useUpdateTransitionPaymentMutation,
  useUpdateTransitionPrepaymentMutation,
} = settingApiSlice
