export interface Order {
  id: number;
  customerName: string;
  customerPhone: string;
  foodItem: string;
  quantity: number;
  price: number;
  deliveryAddress: string;
  status: string;
  orderDate: string;
}

export interface OrderSummary {
  totalOrders: number;
  placedOrders: number;
  preparingOrders: number;
  outForDeliveryOrders: number;
  deliveredOrders: number;
  cancelledOrders: number;
  totalRevenue: number;
}

export interface CreateOrderRequest {
  customerName: string;
  customerPhone: string;
  foodItem: string;
  quantity: number;
  price: number;
  deliveryAddress: string;
}

export interface UpdateOrderRequest {
  customerName: string;
  customerPhone: string;
  foodItem: string;
  quantity: number;
  price: number;
  deliveryAddress: string;
}

export interface UpdateOrderStatusRequest {
  status: string;
}

export const ORDER_STATUSES = ['Placed', 'Preparing', 'OutForDelivery', 'Delivered', 'Cancelled'];
