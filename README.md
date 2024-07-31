# TektonChallenge
 
This is a technical challenge to apply for the technical lead role at Tekton Labs

### Overview
Netcore api with a service-repository pattern, using Entity Framework Core to connect to Postgresql database with code-first approach.

### To execute this project
Please clone the project from this repository and open it in Visual Studio.

Make sure you have installed Netcore 8. If you don't have it, please refeer [here](https://dotnet.microsoft.com/es-es/download/dotnet/8.0)

To recreate database, please provide a valid ConnectionString value in appsettings.json using a Postgresql database. Then, execute the update-database command in package managment console (Using Visual Studio -> Tools -> NuGet package manager -> package managment console). 

** It's important that user provided in ConnectionString has enough privileges to create databases.

Finally, run the application from Run button in the toolbar to see swagger UI in your browser.
The Execution time log, will be saved in MyDocuments folder.