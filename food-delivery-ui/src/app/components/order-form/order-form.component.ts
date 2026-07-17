import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { OrderService } from '../../services/order.service';
import { CreateOrderRequest, UpdateOrderRequest } from '../../models/order.model';

@Component({
    selector: 'app-order-form',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule],
    templateUrl: './order-form.component.html',
    styleUrls: ['./order-form.component.css']
})
export class OrderFormComponent implements OnInit {
    @Input() orderId: number | null = null;
    @Output() orderCreated = new EventEmitter<void>();
    @Output() orderUpdated = new EventEmitter<void>();
    @Output() cancelled = new EventEmitter<void>();

    form!: FormGroup;
    loading: boolean = false;
    error: string | null = null;
    success: string | null = null;

    constructor(private fb: FormBuilder, private orderService: OrderService) {
        this.form = this.fb.group({
            customerName: ['', [Validators.required, Validators.maxLength(150)]],
            customerPhone: ['', [Validators.required, Validators.maxLength(20)]],
            foodItem: ['', [Validators.required, Validators.maxLength(200)]],
            quantity: [1, [Validators.required, Validators.min(1)]],
            price: [0, [Validators.required, Validators.min(0.01)]],
            deliveryAddress: ['', [Validators.required, Validators.maxLength(500)]]
        });
    }

    ngOnInit(): void {
        if (this.orderId) {
            this.loadOrder();
        }
    }

    loadOrder(): void {
        if (!this.orderId) return;
        this.loading = true;
        this.orderService.getOrderById(this.orderId).subscribe({
            next: (order) => {
                this.form.patchValue({
                    customerName: order.customerName,
                    customerPhone: order.customerPhone,
                    foodItem: order.foodItem,
                    quantity: order.quantity,
                    price: order.price,
                    deliveryAddress: order.deliveryAddress
                });
                this.loading = false;
            },
            error: (err) => {
                this.error = 'Failed to load order';
                this.loading = false;
            }
        });
    }

    submit(): void {
        if (this.form.invalid) {
            this.error = 'Please fill all required fields correctly';
            return;
        }

        this.loading = true;
        this.error = null;
        this.success = null;

        const formValue = this.form.value;

        if (this.orderId) {
            const request: UpdateOrderRequest = formValue;
            this.orderService.updateOrder(this.orderId, request).subscribe({
                next: () => {
                    this.success = 'Order updated successfully!';
                    this.loading = false;
                    setTimeout(() => {
                        this.orderUpdated.emit();
                        this.resetForm();
                    }, 1000);
                },
                error: (err) => {
                    this.error = err.error?.message || 'Failed to update order';
                    this.loading = false;
                }
            });
        } else {
            const request: CreateOrderRequest = formValue;
            this.orderService.createOrder(request).subscribe({
                next: () => {
                    this.success = 'Order created successfully!';
                    this.loading = false;
                    setTimeout(() => {
                        this.orderCreated.emit();
                        this.resetForm();
                    }, 1000);
                },
                error: (err) => {
                    this.error = err.error?.message || 'Failed to create order';
                    this.loading = false;
                }
            });
        }
    }

    resetForm(): void {
        this.form.reset({ quantity: 1, price: 0 });
        this.error = null;
        this.success = null;
    }

    onCancel(): void {
        this.resetForm();
        this.cancelled.emit();
    }
}
