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
- **GET /api/customers**: Retrieve all customers.
- **GET /api/customers/{id}**: Retrieve a customer by ID.
- **POST /api/customers**: Create a new customer.
- **PUT /api/customers/{id}**: Update a customer.
- **DELETE /api/customers/{id}**: Delete a customer.

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