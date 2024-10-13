import {
  Document,
  Page,
  Text,
  View,
  StyleSheet,
  Image,
  usePDF,
} from '@react-pdf/renderer'
import Logo from '@assets/default-avatar.jpeg'
import { formatDateWithSlash, generateInvoiceNumber } from '@/utils/Generate'
import { Skeleton } from '@/components/ui/skeleton'
const PackageDetail = ({ id }: { id: number }) => {
  const styles = StyleSheet.create({
    page: {
      flexDirection: 'row',
      backgroundColor: '#ffffff',
      padding: 10,
    },
    top: {
      flexDirection: 'row',
      justifyContent: 'space-between',
      alignItems: 'center',
      borderBottom: '1px solid #000',
      height: 100,
      width: '100%',
    },
    logo: {
      width: 80,
      height: 80,
    },
    topCenterText: {
      fontSize: 20,
      fontWeight: 'bold',
      textTransform: 'uppercase',
    },
    topLeft: {
      flexDirection: 'column',
      justifyContent: 'center',
      alignItems: 'flex-start',
      gap: 2,
      fontSize: 11,
    },
		body: {
			
		}
  })

  return (
    <>
      <Document title="Package PDF" author="Jack Phat" language="vi-vn">
        <Page size="A4" style={styles.page}>
          <View style={styles.top}>
            <Image src={Logo} fixed style={styles.logo} />
            <Text style={styles.topCenterText}>Package Preorder Invoice</Text>
            <View style={styles.topLeft}>
              <Text>Date: {formatDateWithSlash()}</Text>
              <Text>Serial: {generateInvoiceNumber(id)}</Text>
            </View>
          </View>
					<View style={styles.body}>
						
					</View>
        </Page>
      </Document>
    </>
  )
}

export default PackageDetail
