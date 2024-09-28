import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar'
import { CheckCheck } from 'lucide-react'
import { Timestamp } from '@firebase/firestore-types';

interface MessageProps {
  sender_id: number
  text: string
  timestamp: Timestamp
}
const Index = ({ sender_id, text, timestamp }: MessageProps) => {
  return (
    <div
      className={`grid gap-x-3 gap-y-1 w-full ${
        sender_id % 2 == 0
          ? 'place-items-end grid-cols-[1fr_auto]'
          : 'place-items-start grid-cols-[auto_1fr]'
      }`}>
      <Avatar
        className={`${
          sender_id % 2 == 0 ? 'col-start-2' : 'col-start-1'
        } row-span-2 self-end`}>
        <AvatarImage src="https://github.com/shadcn.png" alt="@shadcn" />
        <AvatarFallback>JP</AvatarFallback>
      </Avatar>
      <div
        className={`${
          sender_id % 2 == 0
            ? 'col-start-1 row-start-1'
            : 'col-start-2 row-start-1'
        } text-sm leading-tight`}>
        User <span className="text-xs opacity-50">{timestamp?.toDate()?.toLocaleTimeString()}</span>
      </div>
      <p
        className={`w-fit p-2 rounded-md text-base text-white relative ${
          sender_id % 2 == 0
            ? 'bg-blue-500 col-start-1'
            : 'bg-gray-400 col-start-2'
        }`}>
        <span
          className={`size-2.5 absolute top-[calc(50%-0.25rem)] ${
            sender_id % 2 == 0
              ? '[clip-path:polygon(100%_50%,_0%_100%,_0%_0%)] bg-blue-500 -right-2'
              : '[clip-path:polygon(0%_50%,_100%_0%,_100%_100%)] bg-gray-400 -left-2'
          }`}></span>
        {text}
      </p>
      <div
        className={`text-sm opacity-70 flex gap-1 items-center ${
          sender_id % 2 == 0
            ? 'col-start-1 row-start-1'
            : 'col-start-2 row-start-1'
        }}`}>
        <CheckCheck size={16} /> seen at 12:45
      </div>
    </div>
  )
}

export default Index
