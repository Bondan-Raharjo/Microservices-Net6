# Attendance System API

A simple microservices-based attendance system REST API built with .NET 6. This project demonstrates a clean architecture using OOP and SOLID principles with focus on separation of concerns.

## Project Structure

```
Microservices-Net6
├── Controllers
│   ├── AttendanceController.cs
│   ├── DepartmentController.cs
│   ├── EmployeeController.cs
│   └── WeatherForecastController.cs
├── Models
│   ├── Attendance.cs
│   ├── Department.cs
│   └── Employee.cs
├── DTOs
│   ├── AttendanceDTO.cs
│   ├── DepartmentDTO.cs
│   └── EmployeeDTO.cs
├── Repositories
│   ├── Interfaces
│   │   ├── IAttendanceRepository.cs
│   │   ├── IDepartmentRepository.cs
│   │   └── IEmployeeRepository.cs
│   ├── AttendanceRepository.cs
│   ├── DepartmentRepository.cs
│   └── EmployeeRepository.cs
├── Services
│   ├── Interfaces
│   │   ├── IAttendanceService.cs
│   │   ├── IDepartmentService.cs
│   │   └── IEmployeeService.cs
│   ├── AttendanceService.cs
│   ├── DepartmentService.cs
│   └── EmployeeService.cs
└── Program.cs
```

## Features

- **Department Management**: CRUD operations for departments
- **Employee Management**: CRUD operations with department association
- **Attendance Tracking**: Clock-in/Clock-out functionality with attendance records

## Relationship Design

- **Department - Employee**: One-to-many (1-n) relationship
- **Employee - Attendance**: One-to-many (1-n) relationship

## API Endpoints

### Department Endpoints

- `GET /api/Department` - Get all departments
- `GET /api/Department/{id}` - Get department by ID
- `POST /api/Department` - Create new department
- `PUT /api/Department/{id}` - Update department
- `DELETE /api/Department/{id}` - Delete department

### Employee Endpoints

- `GET /api/Employee` - Get all employees
- `GET /api/Employee/{id}` - Get employee by ID
- `GET /api/Employee/department/{departmentId}` - Get employees by department ID
- `POST /api/Employee` - Create new employee
- `PUT /api/Employee/{id}` - Update employee
- `DELETE /api/Employee/{id}` - Delete employee

### Attendance Endpoints

- `GET /api/Attendance` - Get all attendance records
- `GET /api/Attendance/{id}` - Get attendance record by ID
- `GET /api/Attendance/employee/{employeeId}` - Get attendance records by employee ID
- `GET /api/Attendance/date-range?startDate={date}&endDate={date}` - Get attendance records by date range
- `POST /api/Attendance` - Create attendance record manually
- `PUT /api/Attendance/{id}` - Update attendance record
- `DELETE /api/Attendance/{id}` - Delete attendance record
- `POST /api/Attendance/clock-in` - Clock in an employee
- `PUT /api/Attendance/{id}/clock-out` - Clock out an employee

## Technical Implementation

- **Clean Architecture**: Separation of concerns with models, repositories, services, and controllers
- **Dependency Injection**: All dependencies are injected through constructors
- **Repository Pattern**: Data access abstraction
- **DTO Pattern**: Data transfer between layers
- **SOLID Principles**:
  - Single Responsibility Principle
  - Open/Closed Principle
  - Liskov Substitution Principle
  - Interface Segregation Principle
  - Dependency Inversion Principle
- **Microservices Design**: Separate services for departments, employees, and attendance
- **In-Memory Data**: Uses dummy data for demonstration purposes

## Getting Started

1. Clone the repository
2. Open the solution in Visual Studio or VS Code
3. Run the project
4. Access the Swagger documentation at `https://localhost:5052/swagger`

## Notes

This is a simplified attendance system without a real database connection, using in-memory collections for data storage. In a production environment, you would integrate with a database and implement additional features like authentication and authorization.
