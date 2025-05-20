# Bulletin Board Web Application

The task is to create an API and MVC for a modest Bulletin Board web application. The application is using ASP.NET Core API for the backend and ASP.NET Core MVC for the frontend. Here is a backup file "BulletinBoard.bak" for the Microsoft SQL Server Management Studio in the root of the repository, which is our database we are using. There are stored procedures that are used in the BoardAPI project.


Functionality:

1) user can create new announcements.
2) user can see a list of all announcements (with filtering by status, category, subcategory).
3) user can update existing announcements.
4) user can delete existing announcements.
5) each announcement must have a title, can have a description, has a creation date, and status (active/inactive).

How to test:

1) clone/download repository.
2) in the Microsoft SQL Server Management Studio "restore" the database using backup from the repository root ("BulletinBoard.bak").
3) double-check the connection string "BillboardDbConnection" in the BoardApi/appsettings.json and your real connection string where the database is deployed; it may differ based on how and where you deploy the database.
4) run the BoardAPI project, then run the BoardMVC, for example, from the Visual Studio IDE, right click, start without debugging.
6) there will be Swagger UI after the API starts and a user interface after the MVC runs.