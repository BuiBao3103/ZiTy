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

const surveyApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getSurverys: builder.query<void, void>({
      query: () => '/survey',
    }),
    getSurveyById: builder.query<void, string>({
      query: (id) => `/survey/${id}`,
    }),
    createSurvey: builder.mutation<void, void>({
      query: (data) => ({
        url: '/survey',
        method: 'POST',
        body: data,
      }),
    }),
    editSurvery: builder.mutation<void, { id: string; data: void }>({
      query: (data) => ({
        url: `/survey/${data.id}`,
        method: 'PUT',
        body: data,
      }),
    }),
    deleteSurvey: builder.mutation<void, string>({
      query: (id) => ({
        url: `/survey/${id}`,
        method: 'DELETE',
      }),
    }),
  }),
})

export const { createNewSurvey } = surveySlice.actions
export default surveySlice.reducer
export const { useGetSurveyByIdQuery } = surveyApiSlice
