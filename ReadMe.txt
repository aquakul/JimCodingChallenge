

This solution contains the following Projects -

Web - Is a .Net Core Web API project which exposes an API to invoke the calculator functions from Postman, Browser etc.
Generator - Is a Class Library which encapsulates the various random data generation methods(Local, 3rd Party API).
Operator - Is a Class Library which encapsulates the various mathematical operations supported (Add, Subtract, Multiply, Divide)
Console - Is a console Application which can act as an alternate to the Web project to take user input(Not completed. Just placeholder)
Test - Is a xUnit Test project for the class libraries.

CalculatorLambda - Is an AWS Lambda Project which has the Lambda function for the mathematical operations.
CalculatorLambda.Tests -  Test project for CalculatorLambda. Validates the mathematical operations


Dependencies - 
.Net Core SDK 3.1, 5
AWS Toolkit for VS2017 and VS 2019 Extension


Solution already contains working AWS Connection settings for the Lambda. The set of access key and secret has minimal privelege.
Code changes can be done to the Lambda and published. However Credential needed for it (Used my personal AWS Account)


To run the Service, Navigate to the 'Web' folder in VS Studio or VS Code terminal and run the command - dotnet watch run
The above command should open up the Swagger page. The service endpoint can be invoked from Swagger UI or from Postman/ Browser etc.

GET Requests would be of following format -  
https://localhost:5001/api/Calculator?op=add&source=api
https://localhost:5001/api/Calculator?op=multiply&source=local

Input query paramters are  - 
'op' which currently supports values - add, subtract, multiply and divide. Anything else would throw 400
'source' which currently supports values - local and api. Anything else passed would throw 400

Things that can be added - 


Put a Console App - This is done
Extend the support for double type or other numeric types
ENUMize the controller input(With GET)
Integrate outgoing Http calls with a resilient library like Polly to introduce things like Retry, fail fast etc.
Create wrapper Web services around the generator and operator core libraries, so that they can be independently deployed, maintained


AWS parameter store vs secrets manager

Went for parameter store because it's free :)
But otherwise secrets manager is more robust - Cross account access, secrets rotation


What design patterns are used in this code -
1. Separation of concern
2. Factory pattern
3. Decorator pattern


