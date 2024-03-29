# IbiMovie

This is a simple web application with two views - Actor page showcases the association between actors and movies, allowing users to filter by actor names. People page allows users to search Star Wars characters by name using SWAPI. 

Demo: [link](http://ibimoive.trimation.group)

## Project Design

Despite the simplicity of the project, as a seasoned technical lead and a Clean Architecture advocate, I wanted to demonstrate my deep understanding of architectural best practices and design patterns. I used C# and ASP.NET Core MVC to demonstrate how we can apply Clean Architecture in building modern web applications.

The project has the following structure:

1. ibiMovie.Core project contains essential domain models, such as Actor and Movie, to encapsulate the core business logic and data structures
2. ibiMovie.Application project to describe the application-specific business rules and the actions that the system can perform
3. ibiMovie.Infra project to manage database activities.
4. ibiMovie.WebAPI project for managing API endpoints while injecting dependencies at runtime.
5. ibiMovie.WebUI project to handle user interactions and views.
6. ibiMovie.Tests project to ensure the code's correctness and reliability.

By adhering to the Clean Architecture principles and software development best practices, I have created a scalable, maintainable, and testable application.

### Screenshots

![demo-screenshot1](./demo_screenshot_1.jpg)

![demo-screenshot2](./demo_screenshot_2.jpg)
