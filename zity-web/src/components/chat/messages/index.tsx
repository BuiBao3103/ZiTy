import { memo } from 'react'
import Message from '../message'
import { MessageType } from '@/schema/message.validate'

interface MessagesProps {
  messages: MessageType[]
}
const Index = ({ messages }: MessagesProps) => {
  return messages.map((message, index) => <Message {...message} key={index} />)
}

export default memo(Index)
