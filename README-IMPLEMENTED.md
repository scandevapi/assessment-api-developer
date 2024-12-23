# README-IMPLEMENTED.md

## Overview
This project demonstrates the refactoring of a legacy customer management system into a modern, maintainable, and testable solution. The implementation adheres to SOLID principles, introduces intelligent validation, and provides RESTful API endpoints for managing customers.

---

## Features

### 1. **Refactoring for SOLID Design Principles**
- **Separation of Concerns**: Divided functionality into:
  - **Models**: For data representation.
  - **Services**: For business logic.
  - **Repositories**: For data persistence.
- Applied design patterns to ensure flexibility and testability:
  - Dependency injection for loose coupling.
  - Strategy pattern for validation logic.
  
### 2. **Intelligent Field Validation**
- **Frontend**: Validates ZIP codes and ensures correct formatting (`#####` or `#####-####`).
- **Backend**: Cross-validates the ZIP code and state field to ensure consistency.

### 3. **RESTful API Endpoints**
Implemented a complete set of API endpoints:
- **GET /api/v1/customers**: Retrieve all customers.
- **GET /api/v1/customers/{id}**: Retrieve a customer by ID.
- **POST /api/v1/customers**: Create a new customer.
- **PUT /api/v1/customers/{id}**: Update a customer.
- **DELETE /api/v1/customers/{id}**: Delete a customer.

### 4. **Dynamic UI Updates**
- Integrated APIs with the dropdown list to dynamically reflect customer creation, updates, and deletions.

### 5. **Unit Testing**
- Added unit tests for:
  - Validation logic.
  - Service methods.
  - API controllers (using mocks for external dependencies).

---

## Installation and Usage

### Prerequisites
- .NET SDK (version 8 or later)
- A tool for testing APIs (e.g., Postman, curl or using Swagger)

### Steps
1. **Clone the repository**:
   ```bash
   git clone [repository-link]
   cd [project-directory]

2. **Configure CORS in API**:
   - Add you configuration in API for Cores. `AllowSpecificOrigins`.

## Configure the project

By default, the project is configured to use in-memory data. If you want to use a SQL database, follow these steps:

1. **Modify the `CustomerRepository`**:
   - Comment out all in-memory code.
   - Uncomment all `DbContext` code.

2. **Configure the database connection in the API project**:
   - Update the connection string in the `appsettings.json` file.
   - Ensure the `ICustomerRepository` service is registered correctly in the `Startup.cs` or `Program.cs` file.

3. **Set up the SQL database**:
   - Use one of the provided SQL scripts to create the database schema.
   - Alternatively, you can use the provided database backup to restore the database.
