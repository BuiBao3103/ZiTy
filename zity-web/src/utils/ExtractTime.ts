export function formatISODate(dateTime: string): string {
  const date = new Date(dateTime)
  return date.toLocaleDateString('en-US', {
    year: 'numeric',
    month: 'short',
    day: '2-digit',
  })
}
