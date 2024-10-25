import { apiSlice } from '../api/apiSlice'
import { IQuestion } from '@/schema/question.validate'

const questionsSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getQuestionsById: builder.query<ResponseDataType<IQuestion>, number | void>({
      query: (page = 1) => `/surveys?page=${page}`,
    }),
  }),
})

export const {
	useGetQuestionsByIdQuery,
} = questionsSlice
