import {
  configureStore,
  isRejectedWithValue,
  Middleware,
  MiddlewareAPI,
} from '@reduxjs/toolkit'
import { useDispatch, useSelector } from 'react-redux'
import authSlice from './features/auth/authSlice'
import { apiSlice } from './features/api/apiSlice'
import surveySlice from './features/survey/surveySlice'
import userSlice from './features/user/userSlice'
import { toast } from 'sonner'

export const rtkQueryErrorLogger: Middleware =
  (api: MiddlewareAPI) => (next) => (action) => {
    // RTK Query uses `createAsyncThunk` from redux-toolkit under the hood, so we're able to utilize these matchers!
    if (isRejectedWithValue(action)) {
      console.warn('We got a rejected action!')
      toast.warning('Async error!', {
        description:
          'data' in action.error
            ? (action.error.data as { message: string }).message
            : action.error.message,
      })
    }

    return next(action)
  }

export const store = configureStore({
  reducer: {
    userReducer: userSlice,
    authReducer: authSlice,
    surveyReducer: surveySlice,
    [apiSlice.reducerPath]: apiSlice.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(apiSlice.middleware, rtkQueryErrorLogger),

  // Thêm middleware để enable các tính năng như caching, invalidation, polling, và nhiều hơn nữa của RTK Query.
})
// Lấy RootState và AppDispatch từ store của chúng ta.
export type RootState = ReturnType<typeof store.getState>

export type AppDispatch = typeof store.dispatch

export const useAppDispath = useDispatch.withTypes<AppDispatch>()
export const useAppSelector = useSelector.withTypes<RootState>()
