# Interactive notes for Quantum

## Overview
This repository contains a Blazor Server App along with two additional projects: a dynamic library for database access and a project for testing services. The purpose of this documentation is to provide an overview of the project's structure, explain its components, and guide you through setting up and running the application.

## Project Structure
The solution consists of the following projects:

- **InteractiveNotes** - This is the main Blazor Server Application project where the web application is implemented.

- **InteractiveNotes.Data** - This project serves as a dynamic library for database access. It encapsulates data access logic and provides an abstraction layer for interacting with the database. This separation of concerns makes the application more maintainable and testable.

- **InteractiveNotes.Tests** - This project contains unit tests for the services and components used in the Blazor Server App. These tests help ensure the reliability and correctness of the application.

_Folders tree:_
<pre>
├───InteractiveNotes    
│   ├───Data    
│   ├───DTO   
│   ├───Mapping   
│   ├───Pages   
│   ├───Properties  
│   ├───Shared  
│   └───wwwroot  
│       ├───css  
│       │   ├───bootstrap  
│       │   └───open-iconic  
│       │       └───font  
│       │           ├───css  
│       │           └───font  
│       └───fonts  
├───InteractiveNotes.Data  
│   ├───EF  
│   ├───Entities  
│   ├───Migrations  
│   └───Repositories  
└───InteractiveNotes.Tests  
    └───Services  
</pre>

## App screenshots:

- ### Main page
![MainPageScreenshot](/Media/MainPage.png)

- ### Editing feature
![EditingFeature](/Media/EditingFeature.png)

- ### Note adding feature
![NoteAddingFeature](/Media/NoteAddingFeature.png)

- ### Note searching feature
![NoteSearchingFeature](/Media/NoteSearchingFeature.png)