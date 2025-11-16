# Clean Architecture .NET + Vue Todo App

A full-stack Todo application demonstrating Clean Architecture principles with a .NET 10 Web API backend and Vue 3 + TypeScript frontend.

---

## Architecture Notes (Thought Process)

### Backend: Clean Architecture with .NET

The backend follows **Clean Architecture** principles, organized into four distinct layers:

1. **TodoApp.Domain** - Core business entities

   -  Contains the `TodoItem` entity
   -  No dependencies on external frameworks
   -  Represents the innermost layer of the application

2. **TodoApp.Application** - Business logic and abstractions

   -  Contains `ITodoService` and `IRepository<T>` interfaces
   -  Implements `TodoService` with core business rules
   -  Defines contracts without implementation details
   -  Enables dependency inversion principle

3. **TodoApp.Infrastructure** - Data access and external concerns

   -  Implements `GenericRepository<T>` with EF Core
   -  Contains `AppDbContext` for database configuration
   -  Handles persistence logic separately from business logic
   -  Can be swapped out without affecting upper layers

4. **TodoApp.Api** - Presentation layer
   -  Contains `TasksController` for HTTP endpoints
   -  Defines DTOs (Data Transfer Objects) for API contracts
   -  Handles dependency injection and CORS configuration
   -  Exposes Swagger/OpenAPI documentation

**Why this structure?**

-  **Separation of Concerns**: Each layer has a single, well-defined responsibility
-  **Testability**: Business logic can be tested in isolation without databases or HTTP
-  **Maintainability**: Changes to infrastructure don't require changes to business logic
-  **Flexibility**: Easy to swap out the database, add new presentation layers, or modify APIs

### Frontend: Modern Vue 3 Architecture

The frontend is built with **Vue 3 Composition API** and **TypeScript**, organized for scalability:

**Structure:**

-  **`src/types/`** - TypeScript interfaces

   -  Centralized type definitions
   -  Single source of truth for data models
   -  Ensures type safety across components

-  **`src/composables/`** - Reusable logic

   -  `useTodoApi.ts` encapsulates all API communication and state management
   -  Follows the Composition API pattern for logic reuse
   -  Separates concerns: components focus on UI, composables handle data

-  **`src/components/`** - Reusable UI components

   -  `TodoItem.vue` - Individual todo display with inline editing
   -  `TodoForm.vue` - Form for creating new todos
   -  Each component has a single responsibility

-  **`src/views/`** - Page-level components
   -  `TodoListView.vue` orchestrates components and composables
   -  Acts as the container for business logic flow

**Why this structure?**

-  **Reusability**: Composables can be used across multiple components
-  **Type Safety**: TypeScript catches errors at compile-time
-  **Maintainability**: Clear separation between presentation and logic
-  **Scalability**: Easy to add new features without refactoring existing code

---

## Setup & Run Instructions

### Prerequisites

-  .NET 10 SDK
-  Node.js (v18 or higher)
-  npm or yarn

### Backend (.NET Web API)

1. Navigate to the Backend directory:

   ```bash
   cd Backend
   ```

2. Restore packages

   ```bash
   dotnet restore
   ```

3. Run the application:

   ```bash
   dotnet run --project TodoApp.Api
   ```

4. The API will start on **http://localhost:5123**

5. Access Swagger UI for API documentation:
   ```
   http://localhost:5123/swagger
   ```

**Available Endpoints:**

-  `GET /api/tasks` - Get all todos
-  `POST /api/tasks` - Create a new todo
-  `GET /api/tasks/{id}` - Get a specific todo
-  `PUT /api/tasks/{id}` - Update a todo
-  `DELETE /api/tasks/{id}` - Delete a todo

### Frontend (Vue 3 + TypeScript)

1. Navigate to the Frontend directory:

   ```bash
   cd Frontend
   ```

2. Install dependencies:

   ```bash
   npm install
   ```

3. Create environment configuration:

   ```bash
   cp .env.example .env
   ```

4. Ensure the `.env` file contains the correct backend URL:

   ```
   VITE_API_BASE_URL=http://localhost:5123
   ```

5. Start the development server:

   ```bash
   npm run dev
   ```

6. The application will start on **http://localhost:5173**

7. Open your browser and navigate to the URL shown in the terminal

---

## Trade-offs and Assumptions

### EF Core In-Memory Database

**Trade-off:** The backend uses **EF Core In-Memory** for data storage.

**Implications:**

-  ✅ **Pros**: Quick setup, no external database required, perfect for testing and demos
-  ❌ **Cons**: Data is volatile and lost on application restart
-  ❌ **Cons**: Not suitable for production use
-  ❌ **Cons**: Lacks referential integrity checks and some SQL features

**Why this choice?**
For this demonstration project, an in-memory database allows for rapid development and testing without the overhead of setting up a real database. The Clean Architecture design means switching to a production database (SQL Server, PostgreSQL, etc.) only requires changes to the Infrastructure layer.

### CORS Configuration

The backend is configured to allow requests from `http://localhost:5173` for local development. This would need to be properly configured for production environments.

### Error Handling

Basic error handling is implemented. A production application would benefit from:

-  Global exception handling middleware
-  Structured logging (Serilog, NLog)
-  Proper HTTP status codes for all edge cases

---

## Future Improvements / Production MVP

To make this application production-ready, the following enhancements are necessary:

### 1. Authentication & Authorization

-  **JWT-based authentication** for secure user sessions
-  **User registration and login** endpoints
-  **Role-based access control** (RBAC) to restrict operations
-  **User-scoped todos** so each user sees only their own tasks

### 2. Real Database Persistence

-  Replace In-Memory database with **SQL Server, PostgreSQL, or MySQL**
-  Add **database migrations** for schema versioning
-  Implement **connection string management** via configuration
-  The Infrastructure layer is already designed to support this swap with minimal changes

### 3. Robust Input Validation

-  **Backend**: Implement **FluentValidation** for comprehensive DTO validation
-  **Frontend**: Add client-side validation with user-friendly error messages
-  **API**: Return detailed validation errors in a consistent format

### 4. Production Infrastructure

-  **Containerization**: Dockerize both frontend and backend
-  **CI/CD Pipeline**: Automated testing and deployment
-  **Environment Configuration**: Proper secrets management (Azure Key Vault, AWS Secrets Manager)
-  **Logging & Monitoring**: Application Insights, ELK stack, or similar
-  **Rate Limiting**: Protect APIs from abuse

### 5. Enhanced Features

-  **Pagination**: Handle large todo lists efficiently
-  **Filtering & Sorting**: Search and organize todos
-  **Due Dates & Priorities**: Add task scheduling capabilities
-  **Real-time Updates**: WebSocket or SignalR for live sync across devices
-  **File Attachments**: Support for adding files to todos

### 6. Testing

-  **Backend**: Unit tests for business logic, integration tests for API endpoints
-  **Frontend**: Component tests with Vitest, E2E tests with Playwright
-  **Test Coverage**: Aim for >80% coverage on critical paths

### 7. UI/UX Enhancements

-  **Accessibility**: ARIA labels, keyboard navigation, screen reader support
-  **Internationalization**: Multi-language support
-  **Dark Mode**: Theme switching capability
-  **Mobile Responsiveness**: Enhanced mobile experience
-  **Progressive Web App**: Offline support and installability

---

## Technology Stack

### Backend

-  **.NET 10** - Modern, cross-platform framework
-  **ASP.NET Core Web API** - RESTful API development
-  **Entity Framework Core** - ORM for data access
-  **Swagger/OpenAPI** - API documentation

### Frontend

-  **Vue 3** - Progressive JavaScript framework
-  **TypeScript** - Type-safe JavaScript
-  **Vite** - Fast build tool and dev server
-  **Composition API** - Modern Vue development pattern

---

## Project Structure

```
dotnet-vue-todo-app/
├── Backend/
│   ├── TodoApp.Domain/          # Entities and core domain logic
│   ├── TodoApp.Application/     # Business logic and interfaces
│   ├── TodoApp.Infrastructure/  # Data access and EF Core
│   └── TodoApp.Api/             # Controllers and API layer
│
├── Frontend/
│   └── src/
│       ├── components/          # Reusable UI components
│       ├── views/              # Page-level components
│       ├── composables/        # Business logic and state
│       ├── types/              # TypeScript interfaces
│       └── App.vue             # Root component
│
└── README.md
```

---

## Development Notes

### Backend Development

-  The application uses **Clean Architecture** to ensure maintainability
-  **Dependency Injection** is configured in `Program.cs`
-  **CORS** is enabled for local frontend development
-  **Repository Pattern** abstracts data access logic

### Frontend Development

-  Uses **`<script setup lang="ts">`** syntax for cleaner, type-safe components
-  **Composables** handle all API communication and state management
-  **Environment variables** manage configuration (via Vite)
-  **Modern CSS** with scoped styles and gradient backgrounds

---

## License

This project is for demonstration purposes.
