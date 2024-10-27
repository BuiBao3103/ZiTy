import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { apiSlice } from '../api/apiSlice'
import { User } from '@/schema/user.validate'

interface QrScanInformation {
  nationID?: string
  name?: string
  dob?: string
  gender?: string
}

interface UserState {
  isEditingUser: boolean
  qrScanInformation?: Partial<QrScanInformation>
  user?: User
}

const initialState: UserState = {
  isEditingUser: false,
  qrScanInformation: undefined,
  user: undefined,
}

const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    getQrScanInformation: (
      state,
      action: PayloadAction<QrScanInformation | undefined>,
    ) => {
      state.qrScanInformation = action.payload
    },
    getUserInformation: (state, action: PayloadAction<User>) => {
      state.user = action.payload
    },
  },
})

const userApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getUser: builder.query<ResponseDataType<User>, number | void>({
      query: (page = 1) => `users?page=${page}`,
    }),
    getUserById: builder.query<User, string | number>({
      query: (id) => `users/${id}`,
    }),
    getCurrentUser: builder.query<User, void>({
      query: () => 'users/me',
    }),
    createUser: builder.mutation<
      void,
      Partial<User> &
        Omit<
          User,
          | 'id'
          | 'createdAt'
          | 'updatedAt'
          | 'items'
          | 'otherAnswers'
          | 'relationships'
          | 'surveys'
          | 'userAnswers'
        >
    >({
      query: (data) => ({
        url: '/users',
        method: 'POST',
        body: data,
      }),
    }),
    updateUser: builder.mutation<
      void,
      {
        id: string | number
        body: Partial<User> &
          Omit<
            User,
            | 'id'
            | 'createdAt'
            | 'updatedAt'
            | 'items'
            | 'otherAnswers'
            | 'relationships'
            | 'surveys'
            | 'userAnswers'
          >
      }
    >({
      query: (data) => ({
        url: `/users/${data.id}`,
        method: 'PUT',
        body: data.body,
      }),
    }),
    deleteUser: builder.mutation<void, string | number | undefined>({
      query: (id) => ({
        url: `/users/${id}`,
        method: 'DELETE',
      }),
    }),
  }),
})

export default userSlice.reducer
export const { getQrScanInformation,getUserInformation } = userSlice.actions
export const {
  useGetUserQuery,
  useGetUserByIdQuery,
  useCreateUserMutation,
  useUpdateUserMutation,
  useDeleteUserMutation,
  useGetCurrentUserQuery,
	useLazyGetCurrentUserQuery
} = userApiSlice
