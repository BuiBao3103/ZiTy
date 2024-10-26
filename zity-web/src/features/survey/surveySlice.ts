import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { apiSlice } from '../api/apiSlice'
import { ISurvey } from '@/schema/survey.validate'
import { FetchBaseQueryError } from '@reduxjs/toolkit/query'
import { IQuestion } from '@/schema/question.validate'

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
    getSurverys: builder.query<ResponseDataType<ISurvey>, number | void>({
      query: (page = 1) => ({ url: `surveys?page=${page}` }),
      providesTags: (results) =>
        results
          ? [
              ...results.contents.map(({ id }) => ({
                type: 'Surveys' as const,
                id,
              })),
              { type: 'Surveys', id: 'LIST' },
            ]
          : [{ type: 'Surveys', id: 'LIST' }],
    }),
    getSurveyById: builder.query<ISurvey, string | number>({
      async queryFn(_arg, _queryApi, _extraOptions, fetchWithBQ) {
        // Fetch survey details
        const surveyResult = await fetchWithBQ(`/surveys/${_arg}`)
        if (surveyResult.error)
          return { error: surveyResult.error as FetchBaseQueryError }
        const survey = surveyResult.data as ISurvey
        // Fetch questions using the survey ID
        const questionsResult = await fetchWithBQ(
          `/questions?SurveyId=eq:${survey.id}&Includes=answers`,
        )
        if (questionsResult.error)
          return { error: questionsResult.error as FetchBaseQueryError }
        const questions = (questionsResult.data as ResponseDataType<IQuestion>)
          .contents
        // Combine survey data with questions and return the combined result
        return {
          data: {
            ...survey,
            questions, // Attach questions to the survey
          },
        }
      },
			providesTags: (result, error, id) =>
        result ? [{ type: 'Surveys', id }] : [],
    }),

    createSurvey: builder.mutation<ISurvey, Partial<ISurvey>>({
      query: (data) => ({
        url: 'surveys',
        method: 'POST',
        body: data,
      }),
			invalidatesTags: [{ type: 'Surveys', id: 'LIST' }],
    }),
    updateSurvery: builder.mutation<
      void,
      { id: number | undefined; body: Partial<ISurvey> }
    >({
      query: (data) => ({
        url: `surveys/${data.id}`,
        method: 'PUT',
        body: data.body,
      }),
			invalidatesTags: (result, error, arg) => [
				{ type: 'Surveys', id: arg.id },
			],
    }),
    deleteSurvey: builder.mutation<void, string | number | undefined>({
      query: (id) => ({
        url: `surveys/${id}`,
        method: 'DELETE',
      }),
			invalidatesTags: (result, error, id) => [{ type: 'Surveys', id }],
    }),
  }),
})

export const { createNewSurvey } = surveySlice.actions
export default surveySlice.reducer
export const {
  useGetSurveyByIdQuery,
  useGetSurverysQuery,
  useCreateSurveyMutation,
  useUpdateSurveryMutation,
  useDeleteSurveyMutation,
} = surveyApiSlice
