Hostel Management System
A comprehensive C# Windows Forms application for managing hostel operations efficiently.

Overview
The Hostel Management System is a desktop application built with C# Windows Forms and MySQL database that automates hostel administration tasks. It follows 3-Tier Architecture and implements all 4 Pillars of OOP.

Features
Admin Login
Secure login with username and password

Student Management
Add, Update, Delete, View students

Room Allotment
Manage rooms, track occupancy

Fee Management
Collect fees, track payment status

Daily Attendance
Mark and track student attendance

Mess Management
Manage mess facility and menu

Mess Menu
Create weekly meal schedule

Mess Attendance
Track meal consumption

Complaint System
Submit and resolve complaints with notifications

Admin Profile
View admin profile

Technology Stack
Frontend
C# Windows Forms (.NET Framework)

Architecture
3-Tier (UI, BL, DL)

Database
MySQL

Connection
ADO.NET with MySql.Data

IDE
Visual Studio

Architecture
3-Tier Architecture

UI Layer (Forms)
User interface, takes input, displays data. No database code here.

BL Layer (Business Logic)
Validation, business rules, calculations. No database code here.

DL Layer (Data Access)
Database operations, SQL queries. Only database code here.

Database
MySQL with 8 tables

OOP Principles Implemented

Encapsulation
Model classes, 3-Tier layers

Inheritance
All forms inherit from BaseForm/AbstractBaseForm

Polymorphism
Same method names (SaveStudent, SaveRoom, etc.)

Abstraction
3-Tier Architecture, DatabaseConnection class

Users
Roohan
Username: Roohan
Password: abc123
Role: Admin

Maib
Username: Maib
Password: abc123
Role: Admin

How to Run
Step 1: Clone the Repository
git clone https://github.com/yourusername/hostel-management-system.git

Step 2: Open in Visual Studio
Open hostel_management_system_oop.sln in Visual Studio

Step 3: Setup Database
Open phpMyAdmin and run the DatabaseScript.sql file. Database will be created automatically.

Step 4: Update Connection String
Open App.config and update the connection string with your MySQL password.

Step 5: Build and Run
Press F5 in Visual Studio

Project Structure
BL Folder
Business Logic Layer (All BL classes)

DL Folder
Data Layer (All DL classes)

Model Folder
Model Layer (All Model classes)

UI Folder
All Forms (UI Layer)

Properties Folder
AssemblyInfo and project properties

App.config
Database connection string

Program.cs
Entry point of application

DatabaseScript.sql
SQL file to create database

Database Tables
owners
Admin users

students
Student records

rooms
Room allotment

fees
Fee collection

dailyAttendance
Daily attendance

complaints
Student complaints

messmenu
Mess menu

messattendance
Mess attendance

Future Enhancements
Email/SMS notification system
Export reports to Excel/PDF
Student/Parent web portal
Mobile application
Online fee payment integration

Developer
Name
Muhammad Roohan Hussain

Program
BS Computer Science

University
UET Lahore, Faisalabad Campus

Year
2022-2025

License
This project was developed as a semester project for BS Computer Science.

Made with ❤️ by Roohan