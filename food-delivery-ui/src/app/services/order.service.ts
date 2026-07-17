import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Order, OrderSummary, CreateOrderRequest, UpdateOrderRequest, UpdateOrderStatusRequest } from '../models/order.model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private apiUrl = 'http://localhost:5002/api/orders';

  constructor(private http: HttpClient) { }

  getOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.apiUrl);
  }

  getOrderById(id: number): Observable<Order> {
    return this.http.get<Order>(`${this.apiUrl}/${id}`);
  }

  searchOrders(customerName?: string, status?: string): Observable<Order[]> {
    let params = '';
    if (customerName) params += `?customerName=${encodeURIComponent(customerName)}`;
    if (status) params += (params ? '&' : '?') + `status=${encodeURIComponent(status)}`;
    return this.http.get<Order[]>(`${this.apiUrl}/search${params}`);
  }

  createOrder(request: CreateOrderRequest): Observable<Order> {
    return this.http.post<Order>(this.apiUrl, request);
  }

  updateOrder(id: number, request: UpdateOrderRequest): Observable<Order> {
    return this.http.put<Order>(`${this.apiUrl}/${id}`, request);
  }

  updateOrderStatus(id: number, request: UpdateOrderStatusRequest): Observable<Order> {
    return this.http.patch<Order>(`${this.apiUrl}/${id}/status`, request);
  }

  deleteOrder(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getSummary(): Observable<OrderSummary> {
    return this.http.get<OrderSummary>(`${this.apiUrl}/summary`);
  }
}
