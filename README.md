# Airport Reservation System

A comprehensive airport management and flight reservation system built with ASP.NET Core MVC and Entity Framework Core.

## 📋 Overview

This project is an airport reservation system that allows passengers to book flights, manage reservations, and handle flight operations. The system supports different types of passengers (Travellers and Staff) and provides a complete flight management solution.

## ✨ Features

- **Flight Management**: Create, update, and manage flight schedules
- **Passenger Registration**: Register passengers with detailed information
- **Reservation System**: Book flights with seat selection and VIP options
- **Multi-type Passengers**: Support for regular travelers and staff members
- **Plane Management**: Track aircraft information and capacity
- **Age Calculation**: Automatic age calculation based on birthdate
- **Validation**: Comprehensive data validation for all inputs

## 🏗️ Project Structure

```
Airport-ReservationSystem/
│
├── AM.Core.Domain/          # Domain entities and business logic
│   ├── Flight.cs            # Flight entity
│   ├── Passenger.cs         # Base passenger entity
│   ├── Traveller.cs         # Traveller passenger type
│   ├── Staff.cs             # Staff passenger type
│   ├── Plane.cs             # Aircraft entity
│   ├── Reservation.cs       # Reservation entity
│   └── FullName.cs          # Value object for names
│
├── AM.Core.Services/        # Business logic and service layer
│   ├── IService.cs          # Generic service interface
│   ├── Service.cs           # Generic service implementation
│   ├── IFlightService.cs    # Flight service interface
│   ├── FlightService.cs     # Flight service implementation
│   ├── IPlaneService.cs     # Plane service interface
│   └── PlaneService.cs      # Plane service implementation
│
├── AM.Core.Interfaces/      # Repository and UoW interfaces
│   ├── IRepository.cs       # Generic repository interface
│   └── IUnitOfWork.cs       # Unit of Work interface
│
├── AM.Data/        # Data access layer implementation
│   ├── AMContext.cs         # DbContext
│   ├── Repository.cs        # Repository implementation
│   └── UnitOfWork.cs        # Unit of Work implementation
│
├── AM.UI.Web/               # ASP.NET Core MVC Web Application
│   ├── Controllers/         # MVC Controllers
│   ├── Views/              # Razor views
│   ├── wwwroot/            # Static files
│   └── Program.cs          # Application entry point
│
└── README.md
```

## 🛠️ Technologies Used

- **Framework**: .NET 8.0
- **Architecture**: ASP.NET Core MVC
- **ORM**: Entity Framework Core
- **Database**: SQL Server (configurable)
- **Language**: C#
- **Design Patterns**: 
  - Repository Pattern
  - Unit of Work Pattern
  - Service Layer Pattern
  - Domain-Driven Design (DDD)

## 📦 Domain Models

### Passenger
Base class for all passenger types with the following properties:
- Passport Number (7 characters, Primary Key)
- Birth Date with automatic age calculation
- Email Address with validation
- Full Name (FirstName, LastName)
- Phone Number
- Associated Reservations

### Traveller
Extends Passenger with:
- Health Information
- Nationality

### Staff
Extends Passenger with:
- Employment Date
- Job Function
- Salary

### Flight
- Destination and Departure locations
- Flight Date and Effective Arrival time
- Estimated Duration (in minutes)
- Associated Plane
- Reservations list

### Plane
- Capacity
- Manufacture Date
- Plane Type (Boeing or Airbus)
- Associated Flights

### Reservation
Links Passengers to Flights with:
- Price
- Seat assignment
- VIP status
- Foreign keys to Passenger and Flight

## 🎯 Service Layer

The application implements a robust service layer with the following services:

### Generic Service (IService<T>)
Base service interface providing CRUD operations:
- `Add(T obj)` - Create new entity
- `Get(object id)` - Retrieve entity by ID
- `GetAll()` - Retrieve all entities
- `Update(T obj)` - Update existing entity
- `Delete(T obj)` - Remove entity

### Flight Service (IFlightService)
Specialized flight management operations:
- `GetFlightDates(string destination)` - Get all flight dates for a destination
- `GetFlights(string filterType, string filterValue)` - Filter flights by various criteria
- `GetWeeklyFlightNumber(DateTime date)` - Count flights in a week period
- `GetDurationAverage(string destination)` - Calculate average flight duration
- `SortFlights()` - Sort flights by estimated duration
- `ShowFlightDetails(Plane plane)` - Display flight information for a specific plane
- `GetThreeOlderTravellers(Flight flight)` - Retrieve oldest passengers on a flight

### Plane Service (IPlaneService)
Aircraft management operations:
- `GetPassengers(Plane plane)` - Get all passengers who flew on a specific plane
- `GetFlights(int NbrPlane)` - Get flights for newest planes
- `IsAvailable(Flight flight, int NbrSeat)` - Check seat availability on a flight

## 🏛️ Architecture Patterns

### Repository Pattern
Generic repository interface for data access abstraction:
```csharp
public interface IRepository<T> where T : class
{
    void Add(T entity);
    T Get(object id);
    IList<T> GetAll();
    void Update(T entity);
    void Remove(T entity);
}
```

### Unit of Work Pattern
Manages transactions and ensures data consistency:
```csharp
public interface IUnitOfWork
{
    IRepository<T> GetRepository<T>() where T : class;
    void Save();
}
```

## 🚀 Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB or Express)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/houssemdxd/Aireport-reservationSystem.git
   cd Aireport-reservationSystem
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Update Database Connection String**
   
   Open `appsettings.json` in the web project and update the connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AirportDB;Trusted_Connection=True;"
     }
   }
   ```

4. **Apply Database Migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run --project AM.UI.Web
   ```

6. **Access the application**
   
   Open your browser and navigate to `https://localhost:5001` or `http://localhost:5000`

## 📊 Database Schema

The system uses Entity Framework Core with the following relationships:
- **One-to-Many**: Plane → Flights
- **Many-to-Many**: Passengers ↔ Flights (through Reservations)
- **Inheritance**: Passenger → Traveller, Passenger → Staff (TPH - Table Per Hierarchy)

## 💻 Usage Examples

### Creating a Flight Service Instance
```csharp
var flightService = new FlightService(unitOfWork);
var flights = flightService.GetAll();
```

### Checking Flight Availability
```csharp
var planeService = new PlaneService(unitOfWork);
bool available = planeService.IsAvailable(flight, requestedSeats);
```

### Getting Flight Statistics
```csharp
var flightService = new FlightService(unitOfWork);
double avgDuration = flightService.GetDurationAverage("Paris");
int weeklyFlights = flightService.GetWeeklyFlightNumber(DateTime.Now);
```

### Filtering Flights
```csharp
var flights = flightService.GetFlights("Destination", "New York");
```

## 🔍 Key Features Explained

### Automatic Age Calculation
The Passenger class automatically calculates age based on birth date:
```csharp
public int Age { 
    get {
        int age = DateTime.Now.Year - BirthDate.Year;
        if ((DateTime.Now.Month < BirthDate.Month) ||
            (DateTime.Now.Month == BirthDate.Month && DateTime.Now.Day < BirthDate.Day))
            age--;
        return age;
    }
}
```

### Passenger Type Polymorphism
Different passenger types can be identified:
```csharp
Passenger passenger = new Traveller();
string type = passenger.GetPassengerType(); // Returns: "I'm a traveller"
```

### LINQ Query Support
The service layer supports both LINQ method syntax and query syntax:
```csharp
// Method syntax
var dates = flights.Where(f => f.Destination == destination)
                  .Select(f => f.FlightDate)
                  .ToList();

// Query syntax
var dates = (from f in flights
             where f.Destination == destination
             select f.FlightDate).ToList();
```

## 🧪 Data Validation

The system implements comprehensive validation:
- **Passport Number**: Exactly 7 characters
- **Email**: Valid email format
- **Phone**: Valid phone number format
- **Dates**: Proper DateTime validation
- **Capacity**: Non-negative integers
- **Salary**: Currency format validation

## 🔐 Business Rules

1. **Seat Availability**: System checks if enough seats are available before booking
2. **Age Calculation**: Automatically calculates passenger age accounting for leap years
3. **Flight Duration**: Tracked in minutes for precise scheduling
4. **Passenger Types**: Supports inheritance for different passenger categories
5. **Reservation Management**: Links passengers to flights with pricing and seat information

## 📝 API Endpoints (if Web API is implemented)

*To be documented based on your controller implementations*

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 👤 Author

**Houssem**
- GitHub: [@houssemdxd](https://github.com/houssemdxd)
- Repository: [Aireport-reservationSystem](https://github.com/houssemdxd/Aireport-reservationSystem)

## 🙏 Acknowledgments

- Built with ASP.NET Core MVC
- Entity Framework Core for data access
- Inspired by real-world airport reservation systems

## 📞 Support

For support, please open an issue in the GitHub repository or contact the maintainer.

---

**Note**: This project is for educational purposes and demonstrates the implementation of a multi-layered architecture using ASP.NET Core MVC, Entity Framework Core, and industry-standard design patterns.
