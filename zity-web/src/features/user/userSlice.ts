import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { apiSlice } from '../api/apiSlice'

interface UserState {
  isEditingUser: boolean
}

const initialState: UserState = {
  isEditingUser: false,
}

const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    editingUser(state, action: PayloadAction<UserState>) {
      state.isEditingUser = action.payload.isEditingUser
    },
  },
})

export default userSlice.reducer
export const { editingUser } = userSlice.actions
