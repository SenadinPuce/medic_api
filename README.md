# MedicLab API

## Description
The MedicLab API is a backend service developed in .NET 8 to manage access to the MedicLab system. It provides endpoints for user authentication, user management, and other administrative tasks. This API is consumed by the MedicLab frontend application.

## Features
- **Authentication:**
  - Admin login authentication endpoint.
  - Admin logout endpoint.
- **User Management:**
  - Fetch all users.
  - Fetch details of a specific user.
  - Block a user by ID.
  - Register/add a new user.

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server or any other compatible database

### How to Start the Application
1. Clone the repository:

   ```
   git clone https://github.com/yourusername/medic_api.git

   ```
      
2. Restore the dependencies:

   ```
   dotnet restore

   ```

3. Update the appsettings.json file with your database connection string:

    {
    "ConnectionStrings": {
      "DefaultConnection": "YourDatabaseConnectionString"
    }
  }



4. Run the application:

  ```
  dotnet run

  ```

## API Documentation
The API documentation is available at:
[Swagger Documentation](https://medic-lab-api.azurewebsites.net/swagger/index.html)


## Notes
- Sometimes you may experience a delay of 20-30 seconds for responses due to the Azure free web service limitations.

## License
This project is licensed under the MIT License.


