# Mini E-Commerce (Products & Orders) - ASP.NET Core + EF Core + Blazor

## Overview
This project is a simple mini e-commerce system built using **ASP.NET Core Web API**, **Entity Framework Core**, and **Blazor**.  
It allows managing products and creating customer orders with stock validation and discount calculation.

---

## User Stories Implemented

### US-01 Create Product
Create a product with the following validation rules:
- Name is required
- Price must be greater than 0
- Available Quantity must be greater than or equal to 0

### US-02 List Products
- List all available products
- Pagination is supported as a bonus (if enabled)

### US-03 Create Order
Create an order with:
- Customer Name
- Customer Email
- Order Items (ProductId + Quantity)

Validation:
- Order must contain at least one item
- Quantity must be greater than 0
- Stock must be validated before placing the order
- Stock is reduced automatically after order creation

### US-04 Calculate Total
Discount is calculated based on the total number of items in the order:
- 2–4 items → 5% discount
- 5+ items → 10% discount
- Otherwise → 0%

### US-05 Get Order
Retrieve order details including:
- Customer information
- Order items
- Total amount
- Discount percentage
- Final total after discount

---

## Tech Stack
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Blazor (Front-end)

---

## Solution Structure
- `Domain` → Entities (Product, Order, OrderItem)
- `Infrastructure` → Repositories + Services + DbContext
- `Shared` → DTOs
- `Application` → API Controllers
- `Front` → Blazor UI

---

## Prerequisites
Make sure you have the following installed:
- Visual Studio 2022
- .NET SDK 7 or .NET SDK 8
- SQL Server (LocalDB or SQL Server instance)

---

## Database Configuration
1. Open the file:

Application/appsettings.json

2. Update the connection string:


```json
"ConnectionStrings": {
  "Default": "Server=(localdb)\\MSSQLLocalDB;Database=MiniShopDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```
3.Database Migrations (Visual Studio) in Package Manager Console
1.Add-Migration InitialCreate
2.Update-Database

## Useful URLs

Swagger (API Documentation):
https://localhost:<API_PORT>/swagger
Blazor UI:
https://localhost:<FRONT_PORT>/products 

## Run Using Visual Studio
Right click the solution → Set Startup Projects

Choose Multiple startup projects

Set the following:

Application (API) → Start

Front (Blazor) → Start

Click Run (F5)

## API Endpoints:

Products:
POST /api/products → Create Product
GET /api/products → Get All Products
GET /api/products/paged?page=1&pageSize=10 → Get Products with Pagination 

Orders:
POST /api/orders → Create Order
GET /api/orders/{id} → Get Order by Id

## Sample Request:
Create product:
POST /api/products
{
  "name": "Laptop",
  "price": 1000,
  "availableQuantity": 10
}
create order:
POST /api/orders
{
  "customerName": "Name",
  "customerEmail": "Name@email.com",
  "items": [
    { "productId": 1, "quantity": 2 } ]}







