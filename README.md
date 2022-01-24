# APT Technical Test

A big part of what APT does, is translating plain text files into a BACSTEL-IP Standard 18 file format. We do this by using a templating tool that can take almost any type of plain text file.
For this exercise we will be providing a CSV source file containing values delimited by comma. We will need you to build a very simple frontend for the customer to upload the file and a backend RESTful API that will receive the file and save it into a database.

The file fields are per the following order:
**Code,Name,Reference,Amount**

-   You should use .NET CORE Web API and Angular to build this web application. We encourage you to build this using the techniques and methodologies that you use every day and     if relevant/practical we would like you to provide these as part of your submission (i.e. unit tests if you're into TDD, a Trello board if you tend to break things down into     tasks Kanban style).
-   Code-First EF Core with migrations, Fluent API configurations, Relationships, Model Validations
-   Create Resources for our Angular app to use a ResponseWrapper
-   Show your working with csv data files with comma-delimited values
-   All stories should be completed with an appropriate amount of testing.
-   Please create a public repo (GitHub, BitBucket, GitLab etc) and send us the link, make sure to commit regularly so we can see how you came up with the solution.
-   Remember to be RESTful.

## Story 1 - Backend Processor

-   Create a file processor.
-   Validate and save the file into a database.

## Story 2 - Endpoint

-   Create an endpoint so that other services can access the file processor.
-   Use this swagger spec as the basis for your API

swagger: '2.0'
info:
  title: APT Submission API
  version: 1.0.0
basePath: /api
schemes:
  - https
paths:
  /submission:
    post:
      summary: Process and validates the file to be saved
      body:
          code: number
          name: string
          reference: string          
          amount: amount
          required: true
      produces:
        - application/json
      responses:
        '200':
          description: OK
          schema:
            $ref: '#/definitions/Submission'
definitions:
  FileResponse:
    type: object
    required:
      - filename
      - totalLinesRead
    properties:
      filename:
        type: string
      totalLinesRead: 
        type: number

## Story 3 - Validation

-   There are two validation rules:
    -   Minimum amount is £1.00.
    -   Maximum amount is £20,000,000.00.
-   Add validation for these cases and any other validation / error handling you think is appropriate for this endpoint.
-   Add appropriate tests and document the endpoint

## Story 4 - Storing file

-   When a user submits a file.
-   We need to store:
    -   File Name
    -   Total Amount of the File
    -   File Details
	    - Code
	    - Name
	    - Reference
	    - Amount
-   Mock a database or add a simple in-memory database (maybe LiteDB) to persist this data.
-   Produce a unique id for the file.
-   Produce a unique id for each line in the file
-   Add appropriate tests and document the endpoint

## Story 5 - Front End
- Angular
- Create a very simple (do not worry how it looks) for the user to upload a file
- Use the front end to make an API call the RESTful API you have built previously
- Display the error or the success when submitting


Thanks for your time, we look forward to hearing from you!
