# RomanNumbers

## Overview
RomanNumbers is an web API implemented with .NET Core which converts arabic numbers to roman. 

## API
To convert a number to its corresponding roman number do it through:

```
GET http://localhost:10984/roman/YOUR_NUMBER_GOES_HERE
```

To convert the numbers of a text access the following URL:

```
GET http://localhost:10984/roman/text/YOUR_TEXT_HERE
```
## Runing application
Since RomanNumbers is a .NET Core application in order to execute it just dir into RomanNumbers.Web folder and execute:

```
dotnet run
```

