# CalculatorApi

## Startup steps
1. Navigate the Api directory.
2. Run `dotnet run` to start api server

Nodes:
- Entity framework migrations are run on startup.
- The API targets .NET 9

## Design Decisions

### Project Layout/Layers
- Route Modules - Maps the api routes paths to the appropriate handlers.
- Request Handlers - Handles the request
- Service Layer - Interfaces with entity framework to save, update, read and delete calculation entities.
- Entity Framework Core - ORM for handling queries etc.
- Sqlite

### Libaries
- Using entity framework core to simplify the database access layer and migrations.
- Using the Carter library because of it's use of the IEndpointRouteBuilder to keep the route mappings close together and separates the route mapping from their implementation. It also includes integration with FluentValidation to make validation simple, more declarative and reusable for both creation and updates.

## Assumptions
- A double data type is sufficient for both operands and the result data types.
- A double was chosen for the operands data type because the requirements specified that the operands would be a javascript number type, implying that inputs could be either an integer or floating point value.
- Only a relatively small number of clients would be making requests to the API.

## AWS
- Create a docker image to build and run the application, saving the image to somewhere like github or AWS ECR via github actions.
- Create a EC2 VM instance with appropriate IAM roles etc.
- Assign Static IP for the EC2 instance with Elastic IP.
- Setup either an API gateway or load balancer with ALB in front of the EC2 instance and setup the DNS/alias records etc. to point to the API.
- Pull the latest image and compose up the application

## Further improvements
- OpenApi integration for api documentation.
- Implement a logger with an environment variable to change the logging level.