# Activity Tracker Setup

1.  Install [dotnet core](https://www.microsoft.com/net/download/windows)
2.  Install [NPM](https://www.npmjs.com/get-npm)
3.  Install angular cli - run 'npm install -g @angular/cli' from command line
4.  Install [Visual Studio Code](https://code.visualstudio.com/)
4.  Clone this repo
5.  Open ActivityTracker folder with Visual Studio Code
6.  Install extensions fifth icon on the left side. Looks like a box in a box - C#, Debugger for Chrome, Path Intellisense, Prettier, TSLint
7.  Set ASPNET environment variable on command line: windows - 'SET ASPNETCORE_Environment=Development' osx/linux - 'export ASPNETCORE_Environment=Development'
8.  Run 'npm install' from the cmd line in the ActivityTracker/ClientApp folder
9.  Run 'dotnet ef database update' from the cmd line in the ActivityTracker folder to create the database with the Users table
10. Run 'dotnet run' from cmd line in ActivityTracker folder
11. Goto localhost:5000; a simple website should appear!

# Project Layout

## dotnet core api

The dotnet core api is at the root folder and consists of primarily Controllers (Business Logic), Models (Data Transformation Objects), and Migrations (database changes). 

### Controllers

Very similiar to the MVC style controllers you have seen in the past but they have no view. By default they return JSON objects. The class name is annotated by `[Route("api/[controller]")]` This tells dotnet that this api is reached by localhost:5000/api/controllername so UsersController is localhost:5000/api/Users. The controller in the name is implicitly removed. Specific actions are annotated on a function level  `[HttpGet("Me")]` would mean that function is called when GET verb is called on localhost:5000/api/Users/Me. The `[Authorize]` means that the user must have been authenticated. This is done by Auth0 and framework libraries that are setup in Startup.cs

## Models

Models are simple classes used to model data and create DTOs (Data Transformation Objects)

## Migrations

Exactly the same as used in MVC. For local development we are using SQLite because it is cross platform and super easy to use. For production we will be using a more traditional database. 

## Angular Application

The angular application is located in ClientApp folder and is automatically loaded when `dotnet run` is called. In the src/app folder is where the primary objects are. The solid tutorial on Angular is [here](https://angular.io/tutorial).

### Components

User interface components that are made up of an html files and typescript file. The html defines how it looks and the typescript defines what it does. 