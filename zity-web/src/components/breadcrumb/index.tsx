import { Link } from 'react-router-dom'
import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbPage,
  BreadcrumbSeparator,
} from '@/components/ui/breadcrumb'

interface BreadcrumbProps {
  paths: { label: string; to?: string }[] // Array to handle multiple breadcrumb levels
}

const Index: React.FC<BreadcrumbProps> = ({ paths }) => {
  return (
    <div className="w-full px-4 pt-4">
      <Breadcrumb className="p-4 font-medium bg-white rounded-md">
        <BreadcrumbList>
          <BreadcrumbItem>
            <BreadcrumbLink asChild>
              <Link to={'/'}>Home</Link>
            </BreadcrumbLink>
          </BreadcrumbItem>
          <BreadcrumbSeparator />
          {paths.map((path, index) => (
            <>
              {index != paths.length - 1 ? (
                <BreadcrumbItem key={index}>
                  <BreadcrumbLink asChild>
                    <Link to={path.to || ''} className='first-letter:uppercase'>{path.label}</Link>
                  </BreadcrumbLink>
                </BreadcrumbItem>
              ) : (
                <BreadcrumbItem key={index}>
                  <BreadcrumbPage className='first-letter:uppercase'>{path.label}</BreadcrumbPage>
                </BreadcrumbItem>
              )}
              {index < paths.length - 1 && <BreadcrumbSeparator />}
            </>
          ))}
        </BreadcrumbList>
      </Breadcrumb>
    </div>
  )
}

export default Index
