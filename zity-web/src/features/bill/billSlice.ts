import { apiSlice } from "../api/apiSlice";

export const billSlice = apiSlice.injectEndpoints({
	endpoints: (builder) => ({
		getBills: builder.query<void, void>({
			query: () => ({
				url: 'bills',
				method: 'GET',
			}),
		}),
		getBill: builder.query<void, string>({
			query: (id: string) => ({
				url: `bills/${id}`,
				method: 'GET',
			}),
		}),
		createBill: builder.mutation<void, void>({
			query: () => ({
				url: 'bills',
				method: 'POST',
			}),
		}),
		updateBill: builder.mutation<void, void>({
			query: () => ({
				url: 'bills',
				method: 'PUT',
			}),
		}),
		deleteBill: builder.mutation<void, string>({
			query: (id: string) => ({
				url: `bills/${id}`,
				method: 'DELETE',
			}),
		}),
	}),
})