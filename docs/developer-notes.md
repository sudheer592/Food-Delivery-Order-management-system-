# Developer Notes

## Architecture Overview

### Backend (ASP.NET Core 8)
- **API Project**: FoodDelivery.Api
- **Layers**:
  - Models: Order entity
  - DTOs: Data transfer objects for requests/responses
  - Data: EF Core DbContext and seed data
  - Repositories: OrderRepository (data access pattern)
  - Services: OrderService (business logic)
  - Controllers: OrdersController (REST endpoints)
  - Middleware: Exception handling

- **Database**: SQLite (fooddelivery.db)
- **Async Throughout**: All operations are async for scalability
- **CORS**: Enabled for Angular at http://localhost:4200
- **Validation**: Data annotations on DTOs
- **Status Enum**: Placed, Preparing, OutForDelivery, Delivered, Cancelled

### Frontend (Angular 18)
- **Project**: food-delivery-ui
- **Structure**:
  - Components: Dashboard, OrderList, OrderForm (standalone)
  - Services: OrderService (HTTP calls)
  - Models: TypeScript interfaces
  - Styling: Bootstrap 5 + custom CSS

- **HTTP Client**: Communicates with backend at http://localhost:5002/api/orders
- **Forms**: Reactive forms with validation
- **State Management**: Local component state (could be enhanced with NgRx/Akita)
- **Error Handling**: User-friendly error messages

## Key Design Decisions

1. **Clean Architecture**: Separation of concerns with repositories and services
2. **Standalone Components**: Angular 18 modern approach without NgModules
3. **Reactive Forms**: Type-safe form validation and control
4. **Async/Await**: Cleaner async code handling
5. **DTOs**: Separate DTOs from domain models for flexibility
6. **Exception Middleware**: Centralized error handling
7. **Seed Data**: Pre-populated database for testing

## API Endpoints Implemented

- GET /api/orders - Get all orders
- GET /api/orders/{id} - Get specific order
- GET /api/orders/search - Search with filters
- POST /api/orders - Create order
- PUT /api/orders/{id} - Update order
- PATCH /api/orders/{id}/status - Update status
- DELETE /api/orders/{id} - Delete order
- GET /api/orders/summary - Get summary statistics

## Running the Project

### Backend
```bash
cd FoodDelivery.Api
dotnet run
# Swagger UI: http://localhost:5002/swagger
```

### Frontend
```bash
cd food-delivery-ui
npm install
npm start
# App: http://localhost:4200
```

## Future Enhancements

- Pagination and sorting
- Authentication & JWT tokens
- Role-based access control
- Audit logging
- Real-time updates with SignalR
- Unit and integration tests
- CI/CD pipeline
- Docker containerization
- Notification system
- Admin dashboard

