import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { OrderListComponent } from './components/order-list/order-list.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, DashboardComponent, OrderListComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Food Delivery Order Management System';
  activeTab: 'dashboard' | 'orders' = 'dashboard';

  switchTab(tab: 'dashboard' | 'orders'): void {
    this.activeTab = tab;
  }
}
