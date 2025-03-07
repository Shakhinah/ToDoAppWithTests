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
3. Restore dependencies:
   ```bash
   dotnet restore
4. Apply database migrations:
   ```bash
   dotnet ef database update --project ToDoApp
5. Run the backend:
   ```bash
   dotnet run --project ToDoApp
6. Open your browser and navigate to:
   ```bash
   http://localhost:5081

### Frontend Setup (if applicable) 
If your project includes a frontend (e.g., React), follow these additional steps:
1. Navigate to the frontend folder:
   ```bash
   cd todoapp-frontend 
2. Install dependencies:
   ```bash
   npm install
3. Run the frontend:
   ```bash
   npm start
4. Open your browser and navigate to:
   ```bash
   http://localhost:3000

### Running Tests
To run the tests, follow these steps:
1. Navigate to the test project folder:
   ```bash
   cd ToDoApp.Tests 
2. Run the tests:
   ```bash
   dotnet test
3. Run the frontend:
   ```bash
   npm start
4. Open your browser and navigate to:
   ```bash
   http://localhost:3000

### Folder Structure 
   ```bash
ToDoAppWithTests/
├── ToDoApp/               # Main app project
│   ├── Connected\ Services/
│   ├── Dependencies/
│   ├── Properties/
│   ├── API/
│   │   └── Controllers/
│   ├── Core/
│   │   ├── Entities/
│   │   └── Interfaces/
│   ├── Infrastructure/
│   │   ├── Data/
│   │   └── Repositories/
│   ├── Migrations/
│   ├── todoapp-frontend/
│   ├── appsettings.json
│   ├── package.json
│   ├── Program.cs
│   ├── todo.db
│   └── ToDoApp.http
├── ToDoApp.Tests/         # Test project
│   ├── Dependencies/
│   └── ToDoRepositoryTests.cs
├── .gitignore             # Git ignore file
├── README.md              # Project documentation
├── LICENSE                # License file
└── ToDoAppWithTests.sln   # Solution file
