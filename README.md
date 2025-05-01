RideEase - Vehicle Rental System

RideEase is a web application built using ASP.NET MVC for Rapid Application and Development Module.This allows users to register, browse vehicles, make bookings, and manage them  and administrators can manage vehicle listings and view bookings.



Technologies Used

- ASP.NET MVC 5
- Entity Framework 6
- SQL Server (LocalDB)
- Bootstrap 5
- Identity for Authentication
- Git + GitHub for version control

Features

User Features
- Register/Login with ASP.NET Identity
- View and filter available vehicles
- Book vehicles with date selection
- View, edit, cancel, or filter personal bookings
- Responsive UI with smooth navigation

 Admin Features
 
- Admin login via role-based access
- Dashboard showing booking stats
- Approve/Reject bookings
- Add/Edit/Delete vehicles
- Upload vehicle images securely

How to Run the Project

1. Go the GitHub repository
2. Click the green Code button-choose Dowload ZIP
3. Extract the ZIP file to a simple location (C:\RideEase\)

Open the Project in Visual Studio

4. Select RideEase.sln and click open
5. Go to Build in the tp menu
6. Then select Rebuild Solution

Ensure Packages are restored

1. Open Tools → NuGet Package Manager → Package Manager Console
2. Run: Update-Package -reinstall
3. Wait untill all the dependencies are installed successfully.

Database Setup(LocalDB)

The project uses Entity Framework Code First.
On first run, the database will be automatically created and seeded with test data.
No manual SQL setup is needed.
If needed, you manually trigger it.
Update-Database

Run the Application

Press F5 or click Start button
The browser will open and display the homepage.
   
