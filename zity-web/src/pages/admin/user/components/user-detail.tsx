import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar'
import { Button } from '@/components/ui/button'
import { User } from '@/types'
import { X } from 'lucide-react'

interface UserDetailProps {
  user: User | null
  setShowDetail: (user: User | null) => void
}

const UserDetail = ({ user, setShowDetail }: UserDetailProps) => {
  return (
    <div
      onClick={() => setShowDetail(null)}
      className={`fixed top-0 ${
        user ? 'right-0 animate-in fade-in-0 duration-300' : 'right-full'
      } w-full h-screen bg-black/10 z-50 shadow-lg border-l flex justify-end`}>
      <div className="w-3/4 md:w-1/3 h-full animate-in slide-in-from-right-10 duration-500 relative bg-white border-l border-zinc-300 shadow-[-2px_0px_5px_0px_#00000024] px-6 pb-6 pt-10 md:pb-10 md:pt-24">
        <div className=""></div>
				<Button
          className="absolute top-2 right-2"
          size={'icon'}
          onClick={() => setShowDetail(null)}
          variant={'ghost'}>
          <X size={20} />
        </Button>
        <div className="size-full flex flex-col gap-2">
          <Avatar className="size-32 border-4 border-white shadow-lg">
            <AvatarImage src={user?.avatar} alt="User avatar" />
            <AvatarFallback className="text-lg">
              {user?.name
                .split(' ')
                .map((item) => item.charAt(0))
                .join('')}
            </AvatarFallback>
          </Avatar>
          <p className="text-lg font-medium">{user?.name}</p>
          <p className="text-sm">{user?.room}</p>
        </div>
      </div>
    </div>
  )
}

export default UserDetail
