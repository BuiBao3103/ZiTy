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
		updateTransitionPrepayment: builder.mutation<ISetting, Partial<ISetting>>({
			query: (data) => ({
				url: 'settings/transition/prepayment',
				method: 'POST',
				body: data,
			}),
		}),
		updateTransitionPayment: builder.mutation<ISetting, Partial<ISetting>>({
			query: (data) => ({
				url: 'settings/transition/payment',
				method: 'POST',
				body: data,
			}),
		}),
		updateTransitionOverdue: builder.mutation<ISetting, Partial<ISetting>>({
			query: (data) => ({
				url: 'settings/transition/overdue',
				method: 'POST',
				body: data,
			}),
		}),
		updateTransitionDelinquent: builder.mutation<ISetting, Partial<ISetting>>({
			query: (data) => ({
				url: 'settings/transition/prepayment',
				method: 'POST',
				body: data,
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
