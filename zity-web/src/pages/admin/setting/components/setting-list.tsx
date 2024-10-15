import AlertDelete from '@/components/alert/AlertDelete'
import { Badge } from '@/components/ui/badge'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table'
import {
  Tooltip,
  TooltipContent,
  TooltipTrigger,
} from '@/components/ui/tooltip'
import { Setting } from '@/schema/setting.validate'

interface SettingListProps {
  settings: Setting[]
}

const SettingList = ({ settings }: SettingListProps) => {
  return (
    <>
      <Table className="mt-4 h-full">
        <TableHeader>
          <TableRow>
            <TableHead>ID</TableHead>
            <TableHead>Name</TableHead>
            <TableHead>Description</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {settings.map((setting) => (
            <TableRow key={setting.id} className="font-medium cursor-pointer">
              <TableCell className="py-3">{setting.id}</TableCell>
              <TableCell className="">
                <p className="">{setting.room_price_per_m2}</p>
              </TableCell>
              <TableCell className="uppercase">{}</TableCell>
              <TableCell>
                <Tooltip>
                  <TooltipTrigger>
                    <AlertDelete
                      description="package"
                      setAction={() => {}}
                      type="icon"
                      variants="ghost"
                    />
                  </TooltipTrigger>
                  <TooltipContent>Delete</TooltipContent>
                </Tooltip>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </>
  )
}

export default SettingList
