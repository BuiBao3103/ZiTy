import { configureStore } from '@reduxjs/toolkit'
import { useDispatch, useSelector } from 'react-redux'
import authSlice from './features/auth/authSlice'
import { apiSlice } from './features/api/apiSlice'
import surveySlice from './features/survey/surveySlice'

export const store = configureStore({
  reducer: {
    authReducer: authSlice,
		surveyReducer: surveySlice,
    [apiSlice.reducerPath]: apiSlice.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(apiSlice.middleware),

  // Thêm middleware để enable các tính năng như caching, invalidation, polling, và nhiều hơn nữa của RTK Query.
})
// Lấy RootState và AppDispatch từ store của chúng ta.
export type RootState = ReturnType<typeof store.getState>

export type AppDispatch = typeof store.dispatch

export const useAppDispath = useDispatch.withTypes<AppDispatch>()
export const useAppSelector = useSelector.withTypes<RootState>()
