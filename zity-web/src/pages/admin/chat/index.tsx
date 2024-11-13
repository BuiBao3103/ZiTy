import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import DefaultAvatar from '@/assets/default-avatar.jpeg'
import BreadCrumb from '@/components/breadcrumb'
import { FormEvent, useEffect, useRef, useState } from 'react'
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
import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar'

interface Message {
  senderId: number
  text: string
  timestamp: Timestamp
}

const Index = () => {
  const [currentPage, setCurrentPage] = useState<number>(1)
  const { data, isLoading, isFetching } = useGetUsersInScrollQuery(currentPage)
  const [messages, setMessages] = useState<Message[]>([])
  const [msg, setMsg] = useState<string>('')
  const [isLoadingMore, setIsLoadingMore] = useState(false)

  // Create a ref for the chat container and timeout
  const chatContainerRef = useRef<HTMLDivElement>(null)
  const timeoutRef = useRef<NodeJS.Timeout>()

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

  // Handle scroll event for infinite scroll with delay
  const handleScroll = () => {
    if (!chatContainerRef.current || isFetching || isLoadingMore) return

    const { scrollTop, scrollHeight, clientHeight } = chatContainerRef.current
    const scrollThreshold = 20
    const isNearBottom =
      scrollHeight - (scrollTop + clientHeight) <= scrollThreshold

    if (isNearBottom) {
      // Clear any existing timeout
      if (timeoutRef.current) {
        clearTimeout(timeoutRef.current)
      }

      // Set loading state immediately
      setIsLoadingMore(true)

      // Set new timeout for 1 second delay
      timeoutRef.current = setTimeout(() => {
        console.log('Fetching more data after delay...')
        setCurrentPage((prev) => prev + 1)
        setIsLoadingMore(false)
      }, 1000)
    }
  }

  // Add scroll event listener
  useEffect(() => {
    const chatContainer = chatContainerRef.current
    if (chatContainer) {
      chatContainer.addEventListener('scroll', handleScroll)
      return () => {
        chatContainer.removeEventListener('scroll', handleScroll)
        // Clear any existing timeout on cleanup
        if (timeoutRef.current) {
          clearTimeout(timeoutRef.current)
        }
      }
    }
  }, [isFetching, isLoadingMore])

  const onSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    const messagesRef = collection(db, `conversations/1/messages`)
    const messageDocRef = await addDoc(messagesRef, {
      senderId: 1,
      timestamp: serverTimestamp(),
      text: msg,
    })

    const conversationRef = doc(db, `conversations/1`)
    await updateDoc(conversationRef, {
      is_admin_seen: false,
      is_resident_seen: true,
      last_message: messageDocRef,
      last_messaage_timestamp: serverTimestamp(),
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
      <div className="p-4 h-full flex gap-4 overflow-hidden">
        <div className="md:w-1/4 w-full bg-white rounded-md overflow-hidden p-3">
          <div
            ref={chatContainerRef}
            className="h-full flex flex-col gap-1 overflow-y-auto">
            {isLoading ? (
              <div className="flex items-center justify-center h-full">
                <p className="text-gray-500">Loading...</p>
              </div>
            ) : (
              data?.contents.map((user, index) => (
                <div
                  key={index}
                  className="w-full rounded-md h-fit flex items-center gap-2 px-2 py-3 hover:bg-zinc-200 transition-all cursor-pointer">
                  <Avatar>
                    <AvatarImage src={user.avatar ?? DefaultAvatar} />
                    <AvatarFallback>
                      {user.fullName.charAt(0).toUpperCase()}
                    </AvatarFallback>
                  </Avatar>
                  <div className="w-full h-full grid grid-cols-[1fr_40px]">
                    <p className="text-sm font-bold">{user.fullName}</p>
                    <span className="size-2 rounded-full bg-primary"></span>
                    <p className="text-xs text-gray-500 truncate w-[90%]">
                      Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                      Nullam nec feugiat nunc. Nam nec.
                    </p>
                    <p className="text-xs text-gray-400">12:00</p>
                  </div>
                </div>
              ))
            )}
            {(isFetching || isLoadingMore) && (
              <div className="py-2 text-center">
                <p className="text-sm text-gray-500">Loading more...</p>
              </div>
            )}
          </div>
        </div>
        <div className="bg-white rounded-md md:w-3/4 w-full flex gap-4 p-4 overflow-hidden">
          {/* Chat messages section */}
        </div>
      </div>
    </div>
  )
}

export default Index
