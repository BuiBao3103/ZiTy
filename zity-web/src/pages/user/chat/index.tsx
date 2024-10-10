import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar'
import BreadCrumb from '@components/breadcrumb'
import DefaultAvatar from '@/assets/default-avatar.jpeg'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Send } from 'lucide-react'
import { Separator } from '@/components/ui/separator'
import {
  addDoc,
  collection,
  doc,
  limit,
  onSnapshot,
  orderBy,
  query,
  serverTimestamp,
  updateDoc,
} from 'firebase/firestore'
import { useEffect, useRef, useState } from 'react'
import { db } from '@/firebase'
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
} from '@/components/ui/form'
import { FieldErrors, useForm } from 'react-hook-form'
import { z } from 'zod'
import { MessageSchema } from '@/schema/message.validate'
import Messages from '@components/chat/messages'
const Index = () => {
  const [messages, setMessages] = useState<z.infer<typeof MessageSchema>[]>([])
  const msgContainerRef = useRef<HTMLDivElement>(null)
  const form = useForm<z.infer<typeof MessageSchema>>({
    defaultValues: {
      text: '',
    },
  })
  const onSubmit = async (data: z.infer<typeof MessageSchema>) => {
		if(data.text.length === 0) {
			return;
		}
    const messagesRef = collection(db, `conversations/1/messages`)
    const messageDocRef = await addDoc(messagesRef, {
      senderId: 1,
      timestamp: serverTimestamp(),
      text: data.text,
    })

    // Reference to the conversation document
    const conversationRef = doc(db, `conversations/1`)

    // Update the conversation docume
    form.reset({ text: '' })
    form.setFocus('text')
    await updateDoc(conversationRef, {
      is_admin_seen: false,
      is_resident_seen: true,
      last_message: messageDocRef,
      last_messaage_timestamp: serverTimestamp(), // Optional: Update timestamp to the current time
    })
  }
  const onError = (error: FieldErrors<z.infer<typeof MessageSchema>>) => {
    if (error.text) {
      console.log(error.text.message)
    }
  }

  useEffect(() => {
    const messagesRef = collection(db, 'conversations/1/messages')
    const q = query(messagesRef, orderBy('timestamp', 'asc'), limit(50))
    const unsubscribe = onSnapshot(q, (snapshot) => {
      const messages: z.infer<typeof MessageSchema>[] = []
      snapshot.forEach((doc) => {
        messages.push(doc.data() as z.infer<typeof MessageSchema>)
      })
      setMessages(messages)
    })
    return () => unsubscribe()
  }, [])
  useEffect(() => {
    if (msgContainerRef.current) {
      msgContainerRef.current.scrollTo({
        top: msgContainerRef.current.scrollHeight,
        behavior: 'smooth', // This adds smooth scroll behavior
      })
    }
  }, [messages])
  return (
    <div className="w-full h-dvh flex flex-col bg-zinc-100 overflow-hidden">
      <BreadCrumb paths={[{ label: 'chat', to: '/chat' }]} />
      <div className="w-full h-full p-4 flex gap-4 overflow-hidden">
        <div className="w-full h-full flex flex-col bg-white rounded-md">
          <div className="w-full rounded-md bg-zinc-50 flex gap-2 p-4">
            <Avatar className="size-12">
              <AvatarImage src={DefaultAvatar} />
              <AvatarFallback>ADMIN</AvatarFallback>
            </Avatar>
            <div className="flex flex-col font-medium">
              <h2>Admin</h2>
              <p className="text-xs text-green-400">Online</p>
            </div>
          </div>
          <Separator />
          <div className="size-full p-4 overflow-hidden">
            <div
              ref={msgContainerRef}
              className="size-full flex flex-col overflow-y-auto gap-2">
              <Messages messages={messages} />
            </div>
          </div>
          <Separator />
          <Form {...form}>
            <form
              onSubmit={form.handleSubmit(onSubmit, onError)}
              className="p-4 flex gap-4">
              <FormField
                control={form.control}
                name="text"
                render={({ field }) => (
                  <FormItem className="space-y-0 w-full">
                    <FormLabel className="sr-only">Message</FormLabel>
                    <FormControl>
                      <Input
                        {...field}
                        type="text"
                        placeholder="Type your message here...."
                      />
                    </FormControl>
                  </FormItem>
                )}
              />
              <Button className="gap-2" type="submit">
                Send
                <Send />
              </Button>
            </form>
          </Form>
        </div>
      </div>
    </div>
  )
}

export default Index
