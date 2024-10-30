import { apiSlice } from "../api/apiSlice";

// const settingApiSlice = apiSlice.injectEndpoints({
// 	endpoints: (builder) => ({
// 		getSetting: builder.query<ResponseDataType<ISetting>, number | void>({
// 			query: () => `setting`,
// 		}),
// 		updateSetting: builder.mutation<ISetting, Partial<ISetting>>({
// 			query: (data) => ({
// 				url: 'setting',
// 				method: 'PATCH',
// 				body: data,
// 			}),
// 		}),
// 	}),
// })