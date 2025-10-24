# Online SuperMarket - Lite Version

A lightweight online supermarket REST API built with **ASP.NET Core** and **Entity Framework Core** using SQLite database.

## Features

- 🛒 **Product Management** - Create, read, update, and delete products
- 📂 **Category Management** - Organize products by categories
- 👤 **Customer Management** - Manage customer information
- 📦 **Order Management** - Create and track customer orders
- 📊 **Swagger Documentation** - Interactive API documentation
- 💾 **SQLite Database** - Lightweight, file-based database with seed data

## Technology Stack

- **ASP.NET Core 9.0** - Web API framework
- **Entity Framework Core 9.0** - ORM for database operations
- **SQLite** - Lightweight database
- **Swashbuckle** - Swagger/OpenAPI documentation

## Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/alijatoi/Online-SuperMarket-LiteVersion-.git
   cd Online-SuperMarket-LiteVersion-
   ```

2. Navigate to the project directory:
   ```bash
   cd src/OnlineSuperMarket.Api
   ```

3. Restore dependencies:
   ```bash
   dotnet restore
   ```

4. Build the project:
   ```bash
   dotnet build
   ```

5. Run the application:
   ```bash
   dotnet run
   ```

The API will be available at `http://localhost:5000` (or `https://localhost:5001` for HTTPS).

### Accessing the API

- **Swagger UI**: Navigate to `http://localhost:5000/swagger` for interactive API documentation
- **Base API URL**: `http://localhost:5000/api`

## API Endpoints

### Categories

- `GET /api/categories` - Get all categories
- `GET /api/categories/{id}` - Get a specific category
- `POST /api/categories` - Create a new category
- `PUT /api/categories/{id}` - Update a category
- `DELETE /api/categories/{id}` - Delete a category

### Products

- `GET /api/products` - Get all products (supports `?categoryId=` filter)
- `GET /api/products/{id}` - Get a specific product
- `POST /api/products` - Create a new product
- `PUT /api/products/{id}` - Update a product
- `DELETE /api/products/{id}` - Delete a product

### Customers

- `GET /api/customers` - Get all customers
- `GET /api/customers/{id}` - Get a specific customer
- `POST /api/customers` - Create a new customer
- `PUT /api/customers/{id}` - Update a customer
- `DELETE /api/customers/{id}` - Delete a customer

### Orders

- `GET /api/orders` - Get all orders (supports `?customerId=` filter)
- `GET /api/orders/{id}` - Get a specific order
- `POST /api/orders` - Create a new order
- `PUT /api/orders/{id}/status` - Update order status
- `DELETE /api/orders/{id}` - Delete an order

## Sample Data

The database is automatically seeded with sample data on first run:

### Categories
- Fruits & Vegetables
- Dairy Products
- Bakery
- Beverages
- Snacks

### Products
- 8 sample products across different categories with prices and stock quantities

## Example Usage

### Create a Customer
```bash
curl -X POST http://localhost:5000/api/customers \
  -H "Content-Type: application/json" \
  -d '{
    "name": "John Doe",
    "email": "john@example.com",
    "phone": "123-456-7890",
    "address": "123 Main St"
  }'
```

### Create an Order
```bash
curl -X POST http://localhost:5000/api/orders \
  -H "Content-Type: application/json" \
  -d '{
    "customerId": 1,
    "orderItems": [
      {"productId": 1, "quantity": 3},
      {"productId": 3, "quantity": 2}
    ]
  }'
```

### Get Products by Category
```bash
curl http://localhost:5000/api/products?categoryId=1
```

## Project Structure

```
src/OnlineSuperMarket.Api/
├── Controllers/          # API Controllers
│   ├── CategoriesController.cs
│   ├── ProductsController.cs
│   ├── CustomersController.cs
│   └── OrdersController.cs
├── Data/                 # Database Context
│   └── SuperMarketDbContext.cs
├── DTOs/                 # Data Transfer Objects
│   ├── CategoryDto.cs
│   ├── ProductDto.cs
│   ├── CustomerDto.cs
│   └── OrderDto.cs
├── Models/               # Domain Models
│   ├── Category.cs
│   ├── Product.cs
│   ├── Customer.cs
│   ├── Order.cs
│   └── OrderItem.cs
└── Program.cs            # Application Entry Point
```

## Database

The application uses SQLite with a file-based database (`supermarket.db`). The database is automatically created and seeded with sample data on first run.

To reset the database, simply delete the `supermarket.db` file and restart the application.

## Configuration

Database connection string can be configured in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=supermarket.db"
  }
}
```

## License

This project is open source and available under the [MIT License](LICENSE).

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.