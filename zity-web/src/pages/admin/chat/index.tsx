import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
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

interface Message {
  sender_id: number
  text: string
  timestamp: Timestamp // Changed to number
}
const Index = () => {
	const [messages, setMessages] = useState<Message[]>([])
  const [msg, setMsg] = useState<string>('')

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

  const onSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    const messagesRef = collection(
      db,
      `conversations/1/messages`,
    )
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
		<div className="">
		 <div className="flex flex-col gap-2 p-4 border bg-gradient-to-r from-green-200 to-red-200 rounded-md">
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
		</div>
	)
}

export default Index