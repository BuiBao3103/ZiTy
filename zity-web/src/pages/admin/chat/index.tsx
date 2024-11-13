import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import BreadCrumb from '@/components/breadcrumb'
import { FormEvent, useEffect, useState } from 'react'
import Message from '@components/chat/message'
import { db } from '@/firebase'
import {
  addDoc,
  collection,
  doc,
  limit,
  onSnapshot,
  orderBy,
  query,
  serverTimestamp,
  Timestamp,
  updateDoc,
} from 'firebase/firestore'
import { useGetUsersInScrollQuery } from '@/features/user/userSlice'

interface Message {
  senderId: number
  text: string
  timestamp: Timestamp // Changed to number
}
const Index = () => {
  const [currentPage, setCurrentPage] = useState<number>(1)
  const { data, isLoading, isFetching } = useGetUsersInScrollQuery(currentPage)
  const [messages, setMessages] = useState<Message[]>([])
  const [msg, setMsg] = useState<string>('')
  console.log(data?.contents)
  useEffect(() => {
    const messagesRef = collection(db, 'conversations/1/messages')
    const q = query(messagesRef, orderBy('timestamp', 'asc'), limit(50))
    const unsubscribe = onSnapshot(q, (snapshot) => {
      const messages: Message[] = []
      snapshot.forEach((doc) => {
        messages.push(doc.data() as Message)
      })
      setMessages(messages)
    })
    return () => unsubscribe()
  }, [])

  useEffect(() => {
    const onScroll = () => {
      const chatConversation = document.getElementById('chatConversation')
      const scrolledToBottom =
        chatConversation &&
        chatConversation.scrollHeight + chatConversation.scrollTop >= 200
      if (chatConversation) {
        chatConversation.scrollHeight + chatConversation.scrollTop >= 200
      }
      if (scrolledToBottom && !isFetching) {
        console.log('Fetching more data...')
        setCurrentPage(currentPage + 1)
      }
    }

    document.addEventListener('scroll', onScroll)

    return function () {
      document.removeEventListener('scroll', onScroll)
    }
  }, [currentPage, isFetching])

  const onSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    const messagesRef = collection(db, `conversations/1/messages`)
    const messageDocRef = await addDoc(messagesRef, {
      senderId: 1,
      timestamp: serverTimestamp(),
      text: msg,
    })

    // Reference to the conversation document
    const conversationRef = doc(db, `conversations/1`)

    // Update the conversation docume
    await updateDoc(conversationRef, {
      is_admin_seen: false,
      is_resident_seen: true,
      last_message: messageDocRef,
      last_messaage_timestamp: serverTimestamp(), // Optional: Update timestamp to the current time
    })
    setMsg('')
  }
  return (
    <div className="bg-zinc-100 sm:h-screen size-full flex flex-col">
      <BreadCrumb
        paths={[
          {
            label: 'Chat',
          },
        ]}
      />
      <div className="p-4 h-full flex gap-4">
        <div className="md:w-1/5 w-full bg-white rounded-md">
          <div id="chatConversation" className="h-[200px] overflow-y-auto">
            {data?.contents.map((user, index) => (
              <p key={index}>{user.fullName}</p>
            ))}
          </div>
        </div>
        <div className="bg-white rounded-md md:w-4/5 w-full flex gap-4 p-4 overflow-hidden">
          {/* <div className="w-2/3 flex flex-col gap-4">
          <div className="flex flex-col gap-2 overflow-y-auto">
            {messages.map((message, index) => (
              <Message key={index} {...message} />
            ))}
          </div>
          <form onSubmit={onSubmit} className="flex gap-2 mt-6">
            <Input
              type="text"
              onChange={(e) => setMsg(e.target.value)}
              value={msg}
              placeholder="Send message here"
            />
            <Button type="submit" variant={'default'}>
              Submit
            </Button>
          </form>
        </div> */}
        </div>
      </div>
    </div>
  )
}

export default Index
