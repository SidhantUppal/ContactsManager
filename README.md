Contact Management Web Application

Overview
This web application manages contacts using a .NET 8 MVC architecture with Microsoft SQL Server for data storage. It utilizes jQuery and Bootstrap for front-end functionality and styling.

Tech Stack
Framework: Microsoft .NET 8 MVC
Database: Microsoft SQL Server
ORM: Entity Framework Core (Code First approach)
Frontend: jQuery, Bootstrap

Features
1. Display Contacts
Displays a list or grid of all contacts from the database on the main page.

2. Create Contact
Clicking on the "Create Contact" button opens a new window to add a new contact.

3. View/Edit Contact
"View" button opens a popup to display and edit all contact information.
Supports updating contact details like first name, last name, phone number, email, and category.

4. Delete Contact
Provides a "Delete Contact" button in the editing view.
Confirms deletion with a modal before proceeding.

5. Address and Maps
Add a contact’s address to the record.

6. Displays a map with a pin when an address is present, using an external mapping service (e.g., Google Maps).

7. Includes a basic search functionality to filter contacts based on first name, last name, phone number, or email.

Entity Framework Core
Uses Code First approach to define database schema and seed initial data.
Manages database operations and migrations through Entity Framework Core.

Source Control
Maintains source code in Git-based version control.

Unit Testing

Setup Instructions:

Clone the repository from Git.
Configure the connection string for Microsoft SQL Server in the appsettings.json.
Run Entity Framework Core migrations to create the database schema and seed initial data.
Launch the application and navigate to the homepage to start managing contacts.
