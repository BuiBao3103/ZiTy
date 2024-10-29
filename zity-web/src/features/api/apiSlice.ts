import { RootState } from '@/store'
import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import Cookies from 'universal-cookie'

const cookies = new Cookies(null,{path: '/'})

export const apiSlice = createApi({
  reducerPath: 'api',
  baseQuery: fetchBaseQuery({
    baseUrl: import.meta.env.VITE_SERVER_URL,
    prepareHeaders: (headers, { getState }) => {
      // By default, if we have a token in the store, let's use that for authenticated requests
      const token = (getState() as RootState).authReducer.token || cookies.get('accessToken')
      if (token) {
        headers.set('authorization', `Bearer ${token}`)
      }
      return headers
    },
  }),
  tagTypes: ['Auth', 'Service', 'Bill', 'Reports', 'Apartments', 'Surveys'],
  endpoints: (builder) => ({}),
})
