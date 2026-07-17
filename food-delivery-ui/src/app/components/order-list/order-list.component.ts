import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { OrderService } from '../../services/order.service';
import { Order, ORDER_STATUSES } from '../../models/order.model';
import { OrderFormComponent } from '../order-form/order-form.component';

@Component({
  selector: 'app-order-list',
  standalone: true,
  imports: [CommonModule, FormsModule, OrderFormComponent],
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.css']
})
export class OrderListComponent implements OnInit {
  orders: Order[] = [];
  loading: boolean = false;
  error: string | null = null;
  searchCustomerName: string = '';
  filterStatus: string = '';
  statusOptions = ORDER_STATUSES;
  editingId: number | null = null;
  showForm: boolean = false;

  constructor(private orderService: OrderService) { }

  ngOnInit(): void {
    this.loadOrders();
  }

  loadOrders(): void {
    this.loading = true;
    this.error = null;
    this.orderService.getOrders().subscribe({
      next: (data) => {
        this.orders = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load orders. ' + (err.error?.message || err.message);
        this.loading = false;
      }
    });
  }

  searchOrders(): void {
    if (!this.searchCustomerName && !this.filterStatus) {
      this.loadOrders();
      return;
    }

    this.loading = true;
    this.error = null;
    this.orderService.searchOrders(this.searchCustomerName, this.filterStatus).subscribe({
      next: (data) => {
        this.orders = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to search orders. ' + (err.error?.message || err.message);
        this.loading = false;
      }
    });
  }

  clearSearch(): void {
    this.searchCustomerName = '';
    this.filterStatus = '';
    this.loadOrders();
  }

  updateStatus(order: Order, newStatus: string): void {
    this.orderService.updateOrderStatus(order.id, { status: newStatus }).subscribe({
      next: (updated) => {
        const index = this.orders.findIndex(o => o.id === updated.id);
        if (index !== -1) {
          this.orders[index] = updated;
        }
      },
      error: (err) => {
        this.error = 'Failed to update status. ' + (err.error?.message || err.message);
      }
    });
  }

  deleteOrder(id: number): void {
    if (!confirm('Are you sure you want to delete this order?')) return;

    this.orderService.deleteOrder(id).subscribe({
      next: () => {
        this.orders = this.orders.filter(order => order.id !== id);
      },
      error: (err) => {
        this.error = 'Failed to delete order. ' + (err.error?.message || err.message);
      }
    });
  }

  toggleForm(): void {
    this.showForm = !this.showForm;
    this.editingId = null;
  }

  onOrderCreated(): void {
    this.showForm = false;
    this.loadOrders();
  }

  onOrderUpdated(): void {
    this.showForm = false;
    this.editingId = null;
    this.loadOrders();
  }
}
