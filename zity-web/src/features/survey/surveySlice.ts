import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { apiSlice } from '../api/apiSlice'

interface SurveyState {
  isCreateNewSurvey: boolean
}

const initialState: SurveyState = {
  isCreateNewSurvey: false,
}

const surveySlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    createNewSurvey(state, action: PayloadAction<SurveyState>) {
      state.isCreateNewSurvey = action.payload.isCreateNewSurvey
    },
  },
})

export const { createNewSurvey } = surveySlice.actions
export default surveySlice.reducer
