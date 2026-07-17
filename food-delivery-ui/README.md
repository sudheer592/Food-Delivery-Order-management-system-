# Frontend Project Structure

This Angular 18 frontend connects to the ASP.NET Core 8 backend API.

## File Structure

```
src/
├── app/
│   ├── components/
│   │   ├── dashboard/
│   │   │   ├── dashboard.component.ts
│   │   │   ├── dashboard.component.html
│   │   │   └── dashboard.component.css
│   │   ├── order-form/
│   │   │   ├── order-form.component.ts
│   │   │   ├── order-form.component.html
│   │   │   └── order-form.component.css
│   │   └── order-list/
│   │       ├── order-list.component.ts
│   │       ├── order-list.component.html
│   │       └── order-list.component.css
│   ├── models/
│   │   └── order.model.ts
│   ├── services/
│   │   └── order.service.ts
│   ├── app.component.ts
│   ├── app.component.html
│   └── app.component.css
├── main.ts
├── index.html
├── styles.css
└── ...
```

## Components

### Dashboard Component
- Displays summary cards with order counts by status
- Shows total revenue

### Order List Component
- Lists all orders in a table
- Search by customer name
- Filter by status
- Update order status with dropdown
- Delete orders
- Can toggle order form for creation

### Order Form Component
- Reactive forms for data validation
- Create new orders
- Update existing orders
- Validation messages

## Services

### Order Service
- Communicates with backend API at `http://localhost:5002/api/orders`
- Handles all CRUD operations

## Installation and Setup

1. Navigate to `food-delivery-ui` folder
2. Run `npm install` to install dependencies
3. Run `npm start` to start the dev server on port 4200
4. Ensure backend API is running on port 5002

## Building for Production

```bash
npm run build
```

This generates optimized production files in the `dist/` directory.
