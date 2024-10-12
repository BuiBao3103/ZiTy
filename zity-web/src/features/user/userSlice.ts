import { createSlice, PayloadAction } from '@reduxjs/toolkit'

interface QrScanInformation {
  nationID?: string
  name?: string
  dob?: string
  gender?: string
}

interface UserState {
  isEditingUser: boolean
  qrScanInformation?: Partial<QrScanInformation>
}

const initialState: UserState = {
  isEditingUser: false,
  qrScanInformation: undefined,
}

const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    getQrScanInformation: (state, action: PayloadAction<QrScanInformation | undefined>) => {
      state.qrScanInformation = action.payload
    },
  },
})

export default userSlice.reducer
export const { getQrScanInformation } = userSlice.actions
