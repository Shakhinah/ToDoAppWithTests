# ToDoAppWithTests

This is a simple ToDo application built with ASP.NET Core. It includes a main application project (`ToDoApp`) and a test project (`ToDoApp.Tests`).

## Features
- Create, read, update, and delete (CRUD) ToDo items.
- Mark ToDo items as completed.
- Filter ToDo items by status (all, completed, incomplete).
- Unit tests for the backend.

## Technologies Used
- **Backend**: ASP.NET Core, Entity Framework Core, SQLite
- **Frontend**: React (if applicable)
- **Testing**: xUnit (for unit tests)

## Getting Started

Follow these steps to set up and run the project locally.

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0 or later)
- [Visual Studio](https://visualstudio.microsoft.com/) or any code editor (e.g., VS Code)
- [SQLite](https://sqlite.org/index.html) (for the database)

### Setup
1. Clone the repository:
   ```bash
   git clone https://github.com/Shakhinah/ToDoAppWithTests.git 
2. Navigate to the project folder:
```bash
  cd ToDoAppWithTests
3.Restore dependencies:
```bash
  dotnet restore
4.Apply database migrations:
```bash
  dotnet ef database update --project ToDoApp
5.Run the backend:
```bash
  dotnet run --project ToDoApp
6.Open your browser and navigate to:
http://localhost:5000
