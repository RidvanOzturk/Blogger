# Blogger Project

A simple blogging platform where users can post articles, leave comments, and manage their profiles. This project is built using **ASP.NET MVC** and **Entity Framework**, following a multi-layered architecture to ensure clean code and separation of concerns.

## Features

- **User Registration & Authentication**: Users can create accounts, log in, and manage their profiles securely.  
- **Create & Manage Posts**: Users can write, edit, and delete their own blog posts.  
- **Commenting System**: Users can comment on posts and engage in discussions.  
- **Profile Management**: Each user has a profile page where they can update personal information and change their password.  
- **Role-Based Access**: Users can only modify their own content, with restrictions in place to prevent unauthorized access.  
- **Responsive Design**: The project offers a responsive layout for better user experience across devices.  

## Technologies

- **ASP.NET MVC**: For building the web application.
- **Entity Framework (EF)**: As the ORM for database operations.
- **SQL Server**: Database used to store user and post data.
- **Bootstrap**: For responsive design.
- **Chart.js**: For visual representation of data in reports.
- **Dependency Injection**: Managed via built-in IoC container.

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/RidvanOzturk/Blogger.git
2. **Navigate to the Project Directory**:
   ```bash
   cd Blogger
3. **Run Database Migrations**:
   ```bash
   dotnet ef database update
4. **Run the Application**:
   ```bash
   dotnet run
