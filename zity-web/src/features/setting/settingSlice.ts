import { ISetting } from '@/schema/setting.validate'
import { apiSlice } from '../api/apiSlice'

const settingApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getSettings: builder.query<ISetting, void>({
      query: () => 'settings',
    }),
    patchSetting: builder.mutation<ISetting, Partial<ISetting>>({
      query: (data) => ({
        url: 'settings',
        method: 'PATCH',
        body: data,
      }),
    }),
    updateTransitionPrepayment: builder.mutation<ISetting, void>({
      query: () => ({
        url: 'settings/transition/prepayment',
        method: 'POST',
      }),
    }),
    updateTransitionPayment: builder.mutation<ISetting, void>({
      query: () => ({
        url: 'settings/transition/payment',
        method: 'POST',
      }),
    }),
    updateTransitionOverdue: builder.mutation<ISetting, void>({
      query: () => ({
        url: 'settings/transition/overdue',
        method: 'POST',
      }),
    }),
    updateTransitionDelinquent: builder.mutation<ISetting, void>({
      query: () => ({
        url: 'settings/transition/prepayment',
        method: 'POST',
      }),
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
