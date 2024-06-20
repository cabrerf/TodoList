# TodoList

## Installation Instructions

1. Clone the repository.
2. Open the solution in Visual Studio.
3. Compile and run the application.

## Comments 

Todo entity is defined as: ID, description
Example of description: "Sleep, go to play football"

## Comments about test

1. Test are in project TodoTestUnit
2. I use mocks for testing
3. Testing are for GET /api/todos

## Details

1. TodosController have a dependency injection of a repository that manage the list of todo
2. TodosController have a dependency injection of a featureflag to manage feature flags
3. Solid principles 
3. It´s used the Repository pattern
   