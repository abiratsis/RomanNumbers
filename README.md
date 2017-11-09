# RomanNumbers

## Overview
RomanNumbers is an web API implemented with .NET Core which converts arabic numbers to roman. 

## API
To convert a number to its corresponding roman one execute the next GET request:

```
GET http://localhost:10984/roman/YOUR_NUMBER_GOES_HERE
```

To convert all the numbers within a text execute the next GET request:

```
GET http://localhost:10984/roman/text/YOUR_TEXT_HERE
```
## Runing application
Since RomanNumbers is a .NET Core application in order to execute it just go to RomanNumbers.Web folder and execute:

```
dotnet run
```

