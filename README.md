# Desafio API - Police department

A police department management REST API that allows for control of arrests, police force and more.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

What things you need to install the software and how to install them

- .Net Core → Version: 3.1.412;

### Installing

1. Clone the repository to you machine;

2. Create the MySql Schema using the information below:

> server=localhost;port=3306;
> database=desafioapi;
> uid=root;
> password=root

3. Run the following commands:

```
dotnet restore
```

```
dotnet ef database update
```

```
dotnet watch run
```

Then just access : https://localhost:5001/swagger/index.html to get information regarding the APIs methods.

### Running

To be able to access all methods you will need to [Register](https://localhost:5001/api/v1/users/Register) and then [Log in](https://localhost:5001/api/v1/users/Login).

In order to do so you will no to make a POST request to [Register](https://localhost:5001/api/v1/users/register) using the BODY example as described in the [SWAGGER](https://localhost:5001/swagger/index.html) doccumentation.

After that make another POST request but now to [Log in](https://localhost:5001/api/v1/users/login) using the following BODY example:

```
{
  "registerId": "lulay",
  "password": "gft2021"
}
```

The POST to Login will return a token, this token needs to be used in the Authorization part of the request in order to access the correct user and validate the current session; it will expire after a few hours.

## Built With

- [.Net Core](https://dotnet.microsoft.com/download) → Version: 3.1.412;
- [Microsoft.EntityFrameworkCore.Design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/3.1.17) → Version: 3.1.17;
- [Microsoft.IdentityModel.Tokens](https://www.nuget.org/packages/Microsoft.IdentityModel.Tokens/6.7.1) → Version: 6.7.1;
- [Microsoft.OpenApi](https://www.nuget.org/packages/Microsoft.OpenApi/1.2.3) → Version: 1.2.3;
- [Pomelo.EntityFrameworkCore.MySql](https://www.nuget.org/packages/Pomelo.EntityFrameworkCore.MySql/3.1.2) → Version: 3.1.2;
- [Swashbuckle.AspNetCore](https://www.nuget.org/packages/Swashbuckle.AspNetCore/6.1.5) → Version: 6.1.5;
- [System.IdentityModel.Tokens.Jwt](https://www.nuget.org/packages/System.IdentityModel.Tokens.Jwt/6.7.1) → Version: 6.7.1;
- [Microsoft.AspNetCore.Authentication.JwtBearer](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer/3.1.18) → Version: 3.1.18;
- [BCrypt.Net-Core](https://www.nuget.org/packages/BCrypt.Net-Core/1.6.0) → Version: 1.6.0;

## Documentation

In this project all documentation needed was generated using [SWAGGER](https://swagger.io/).

Access [Police departmentAPI/Swagger](https://localhost:5001/swagger/index.html) to read the documentation.

### Entities explanation:

> - _Person_ : An **Entity** who has a Name and a CPF;
> - _User_ : A **Person** who is registered in the API Db as either a Judge(CRUD) and Lawyer(GET);
> - _Victim_ : A **Person** harmed, injured, or killed as a result of a crime, accident, or other event or action;
> - _Perpetrator_ : A **Person** who carries out a harmful, illegal, or immoral act;
> - _Adress_ : The description of characteristics of a place;
> - _Police Department_ : A collection of information regarding a police department such as **Adress**, Phone Number and Name where a **Deputy** works;
> - _Deputy_ : A **Person** who is n charge of a **PoliceDepartment** for a shift;
> - _PoliceOfficer_: A **Person** responsible for making **Arrests**;
> - _Coroner_ : A **Person** who is responsible for performing **Autopsies**;
> - _Autopsy_ : A postmortem examination to discover the cause of death or the extent of disease of a **Victim**;
> - _Crime_ : A harmful act performed by a **Perpetator** on a **Victim** that contains information about how it happened, its date and its **Adress**;
> - _Arrest_ : The act of arresting a **Perpetrator** and creating records about it which should include a **PoliceOfficer**, **Deputy**, **Crime**, and Date;

## Swagger:

![img](https://i.imgur.com/PaurINY.png)

## Examples:

Autopsy [GET] by ID:

```javascript
Response: [
  {
    id: 1,
    victim: {
      id: 1,
      status: true,
      name: "Uzumaki Naruto",
      cpf: "456.456.456-45",
    },
    coroner: {
      id: 1,
      registerId: "123mandioca",
      status: true,
      name: "Bruce Mandioca",
      cpf: "654.654.654-65",
    },
    date: "2021-07-29T00:00:00",
    description: "The autopsy went very wrong, THE CORPSE IS ALIVE!",
    status: true,
  },
];
```

New Police Officer [POST]:

```javascript


Request:
{
  "name": "Gandalf",
  "cpf": "765.456.098-09",
  "registerId": "i_love_LOTR",
  "rankCode": 4
}

Response:
{
  "message": "Police Officer registration to Database complete!",
  "newOfficer": {
    "name": "Gandalf",
    "registerID": "i_love_LOTR",
    "rank": "Police lieutenant"
  }
}
```

## Notes

- CPF format should be: 000.000.000-00;
- Phone Number format should be: 0000-0000;
- Date format should be: 00/00/0000;

## Authors

- **Leonardo Machado** - _LOMH_ - [LeoMac00](https://github.com/leomac00)
