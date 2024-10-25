import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { apiSlice } from '../api/apiSlice'
import { UserLogin } from '@/schema/user.validate'

interface AuthState {
  user: UserLogin,
  accessToken: string,
	refreshToken: string,
}

const initialState: AuthState = {
  user: {
    username: '',
    password: '',
  },
  accessToken: '',
	refreshToken: '',
}

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    userLoggedIn(state, action: PayloadAction<AuthState>) {
      state.user = action.payload.user
      state.accessToken = action.payload.accessToken
			state.refreshToken = action.payload.refreshToken
    },
    userLoggedOut(state) {
      state.user = initialState.user
      state.accessToken = initialState.accessToken
    },
  },
})
export const authApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    login: builder.mutation<AuthState, Omit<AuthState, 'accessToken' | 'refreshToken'>>({
      query: (body) => ({
        url: 'login',
        method: 'POST',
        body,
      }),
      transformErrorResponse: (error) => {
        if (error.status === 'FETCH_ERROR') {
          return { message: 'Network error' }
        }
        console.log(error)
        return error
      },
    }),
  }),
})

export const { userLoggedIn, userLoggedOut } = authSlice.actions

export default authSlice.reducer
export const { useLoginMutation } = authApiSlice
