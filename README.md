# Sample NetCore WebApi
`Demonstrating`
* Customer [FirstName, LastName, DateOfBirth, IsArchived, CreatedOnDate, UpdatedOnDate] CRUD functionality 
* FirstName and Last name are required for creating a customer with date of birth can be updated later.
* Deleted customers are marked as archived and can be viewed in customers list when IsArchived flag is set to true.
* On deleting customer , new customer can be added with same first name and last name other wise api doesn't allow customers with same fisrtname and lastname. 
* Paged search of Customers based on partial first name, last name, and Customer Id Guid
* Sorting of the results based columns Id, First Name, Last Name,  
* Unit for Controllers, Services and Repository using Inmemory Database
* Integration tests to check the working endpoints. 
* Curl request logging
* Exception handling and logging via middleware based on each request type 

#### Tools/Techniques: .NetCore 2.2, C#, XUnit   

### Install Instructions
Fork or Download the repository

Navigate to the `CustomerApi` directory under the cloned repository from above and run:

`dotnet run`

This will run all unit tests:
`dotnet test CustomerApi.UnitTests`


This will run all integration tests:
`dotnet test CustomerApi.IntegrationTests`


Running Via Docker: run following commands.
`Navigate to solution folder`

`docker build -t CustomerApi .`

`docker run --rm -it -p 5000:80 CustomerApi`




