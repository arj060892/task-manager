
# Solution Description for Task Management Application

## Table of Contents

- [Overview](#overview)
- [Backend](#backend)
  - [.NET 6](#net-6)
  - [SQL Database](#sql-database)
  - [Entity Framework (EF)](#entity-framework-ef)
  - [CQRS with MediatR](#cqrs-with-mediatr)
  - [Repository Pattern](#repository-pattern)
  - [Fluent Validations](#fluent-validations)
  - [AutoMapper](#automapper)
- [UI](#ui)
  - [Angular 15](#angular-15)
  - [NgRx Store](#ngrx-store)
  - [NgbBootstrap](#ngbbootstrap)
- [Steps to Run the Application](#steps-to-run-the-application)

## Overview

This task management application is designed as a comprehensive solution for managing tasks. It is built with a modern architecture, using .NET 6 for the backend, Angular 15 for the frontend, and SQL as the database. 

## Backend

### .NET 6

The backend of the application is built using the .NET 6 framework. .NET 6 is a free, cross-platform, open-source developer platform for building many different types of applications.

### SQL Database

The application uses a SQL Database for persistent data storage. SQL databases are known for their robustness and are widely used in the industry.

### Entity Framework (EF)

The application uses Entity Framework as the Object-Relational Mapper (ORM). It simplifies data access by allowing developers to work with data using domain-specific objects, eliminating the need for most of the data-access code that developers usually need to write.

### CQRS with MediatR

The application follows the Command Query Responsibility Segregation (CQRS) pattern, which is a pattern that segregates the operations that read data from the operations that update data by using separate interfaces. MediatR is used as a mediator to handle requests and responses. It acts as a memory bus and simplifies decoupling of in-process messaging.

### Repository Pattern

The repository pattern is used in the application to separate the logic that retrieves the data from the database from the business logic of the application. It acts as a middleman between the application’s business logic and data source.

### Fluent Validations

The application uses Fluent Validations for validating the models. It is a .NET library for building strongly-typed validation rules with a fluent interface.

### AutoMapper

AutoMapper is used in the application to map one object to another. It simplifies the code needed to convert one model to another, thereby reducing the risk of errors and saving development time.

## UI

### Angular 15

The frontend of the application is built using Angular 15, a platform for building mobile and desktop web applications. It includes a wealth of essential features such as mobile gestures, animations, filtering, routing, data binding, security, internationalization, and beautiful UI components.

### NgRx Store

NgRx Store is used for state management in the Angular application. It provides reactive state management for Angular apps inspired by Redux. It uses RxJS to interact with the store.

### NgbBootstrap

NgbBootstrap is used to integrate Bootstrap components with Angular. In this application, it is specifically used for the calendar and timepicker components, providing a seamless and familiar UI experience.

## Steps to Run the Application

### Backend

1. Navigate to the `TaskManager.API` project directory in your terminal.
2. Run the command `dotnet restore` to restore the necessary packages.
3. Run the command `dotnet build` to build the project.
4. Run the command `dotnet run` to start the API project. The API will start, and you should see output indicating the URL the API is running.

### UI

1. Navigate to the `TaskManager.UI/user-task-manager` directory in your terminal.
2. Run the command `npm install` to install the necessary packages.
3. Run the command `ng serve` to start the Angular development server.
4. Open your browser and navigate to `http://localhost:4200`. The application should be running and accessible at this URL.

---



# Steps to Run the Application

## Prerequisites

1. **Install Docker:**
   - Install Docker on your local machine to spin up the SQL Server instance.
   - Download Docker from [here](https://www.docker.com/products/docker-desktop).

2. **Install Node.js:**
   - Install Node.js to run the UI project.
   - Download Node.js from [here](https://nodejs.org/en/download/).

3. **Install Angular CLI:**
   - Install Angular CLI globally on your machine.
   ```sh
   npm install -g @angular/cli
   ```

## Start the SQL Server Instance

1. **Open Command Prompt:**
   - Open the command prompt as an administrator.

2. **Navigate to the Root Folder:**
   - Navigate to the root folder of the project where the `start-sql-container.bat` script is located.
   ```sh
   cd path\to\your\project\root
   ```

3. **Run the Script:**
   - Run the `start-sql-container.bat` script.
   ```sh
   start-sql-container.bat
   ```
   - Wait until you see the message `'SQL Server is ready'` in the command prompt.

## Run the API Project

1. **Navigate to the API Project Folder:**
   - Open a new command prompt and navigate to the API project folder.
   ```sh
   cd path\to\your\TaskManager.API
   ```

2. **Run the API Project:**
   - Run the API project. The API will automatically run the required migrations once it starts.
   ```sh
   dotnet run
   ```

## Run the UI Project

1. **Navigate to the UI Project Folder:**
   - Open a new command prompt and navigate to the UI project folder.
   ```sh
   cd path\to\your\TaskManager.UI
   ```

2. **Install Dependencies:**
   - Run `npm install` to install all the required packages.
   ```sh
   npm install
   ```

3. **Start the Application:**
   - Run `npm start` to start the Angular application.
   ```sh
   npm start
   ```

4. **Access the Application:**
   - Open your browser and navigate to `http://localhost:4200` to access the application.

That's it! Your application should now be up and running.
