# Legato - Backend
Welcome to the GitHub repository of the Legato project's backend!

## The project
### The idea
The Legato project aims to create a community space where registered users can find each other based on their music
preferences. With Legato, we can meet people with whom we have a good chance of finding a common voice. As our motto
holds: Music connects people

### The backend
This repository contains the source files of Legato's main web API which contains the logic and data tier and
responsible for the HTTP request-response handling. The backend provides the processed data for the Legato's frontend.
Together they are creating a fully responsive web application.

### The frontend
The frontend's source code is available at the following GitHub repository: [Legato Frontend](https://github.com/MParoczi/Legato-Frontend)

### The technologies
The backend of the Legato social network were created with the following technologies:
 * .NET Core 3.1
 * ASP.NET Core as the main framework for the web server
 * Entity Framework which is responsible for data persistence
 * Identity as the system to authorize and authenticate the users

## How to run
To run the application, simply navigate to the source folder of the project and open the dotnet command line
interface and run ```dotnet run```. This requires [.NET Core SDK](https://docs.microsoft.com/en-us/dotnet/core/sdk) to be
installed on your system. The web server will host on ```http://localhost:5000```.

## Credits
The project was created during the period of my studies at Codecool Ltd. in 2020. **It is still under development!**