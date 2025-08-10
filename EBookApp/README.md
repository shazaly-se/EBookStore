# Book Ecommerce Application
This is a full-featured Book Ecommerce web application built with ASP.NET Core MVC and Entity Framework Core.
It supports user registration, authentication, roles, book and author management, shopping cart, and payment integration.

# Features
User Roles:

Admin: Manage authors and books (add, edit, delete).

Customer: Browse books, add to cart, checkout, and view orders.

Authentication:

User registration with role selection (Admin or Customer).

Login and logout functionality.

Forgot password with email confirmation using Hangfire background jobs.

Author Management:

Add, edit, list, and delete authors.

Author profile picture upload and preview.

Book Management:

Add, edit, list, and delete books.

Associate books with authors.

Cover image upload and preview.

Shopping Cart:

Add books to cart from any page without page reload (AJAX or redirect back).

View cart with book details, quantity, subtotal, and total amount.

Payment:

Integrated with Stripe payment gateway for secure payments.

After successful payment, order details are saved to the database.

Order Management:

Customers can view their past orders with detailed book info, quantities, and totals.

Invoice email sent automatically upon successful payment.

Email Sending:

Email confirmation, password reset, and order invoice emails sent via background jobs using Hangfire.
# Technologies Used
ASP.NET Core MVC

Entity Framework Core

ASP.NET Identity (with roles)

Hangfire (Background Jobs for sending emails)

Stripe Payment Gateway

Bootstrap 5 (for responsive UI)

jQuery (for interactivity like image preview and AJAX)

# Getting Started
Clone the repository:git clone https://github.com/yourusername/ebook-ecommerce.git

Configure the database connection string in appsettings.json.

# Run migrations and update the database:
dotnet ef database update
Configure Stripe keys in the appsettings or secrets manager.

# Run the application:
dotnet run
Register a new user and select the role (Admin or Customer).

Start managing authors and books, add items to cart, and test payments.

# Notes
The application requires an SMTP email service configured for sending emails.

Hangfire dashboard is available for monitoring background jobs.

Admin users have access to author and book management pages.

Customers have access to browsing books, cart, and order history.
