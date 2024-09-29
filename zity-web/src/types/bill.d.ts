import { BillStatus } from "@/enums"

export interface Bill {
	id: number
	monthly: number
	total_price: number
	old_water: number | null,
	new_water: number | null,
	status: BillStatus,
	relationship_id: number
}