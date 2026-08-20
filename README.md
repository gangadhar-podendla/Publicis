# ABC Pharmacy

A simple Single Page Application for managing medicines and maintaining medicine sale records.

## Technology Stack

* ASP.NET Core Web API - .NET 10
* Angular
* JSON file storage
* Swagger / OpenAPI

## Features

* View available medicines
* Add medicine details
* Search medicines by medicine name
* Red indication for medicines expiring within 30 days
* Yellow indication for medicines with quantity less than 10
* Maintain medicine sale records
* Reduce available medicine quantity after a sale
* Swagger UI for API testing

## Medicine Details

Each medicine contains:

* Full Name
* Notes
* Expiry Date
* Quantity
* Price
* Brand

Notes are stored but are not displayed in the medicine grid, as per the requirement.

## Project Structure

```text
Publicis/
│
├── Pharmacy.Api/
│   ├── Controllers/
│   ├── Data/
│   ├── Models/
│   ├── Repositories/
│   ├── Services/
│   ├── wwwroot/
│   ├── Program.cs
│   └── Pharmacy.Api.csproj
│
├── pharmacy-ui/
│   ├── src/
│   ├── angular.json
│   └── package.json
│
├── Pharmacy.slnx
└── README.md
```

The Angular production build is included inside `Pharmacy.Api/wwwroot`, allowing the complete application to run using the .NET command only.

## Prerequisites

Install:

* .NET 10 SDK

Node.js is required only if you want to modify and rebuild the Angular application.

## Run the Application

Clone the repository:

```bash
git clone <repository-url>
```

Navigate to the repository:

```bash
cd Publicis
```

Run:

```bash
dotnet run --project Pharmacy.Api
```

The terminal will display the application URL, for example:

```text
http://localhost:5282
```

Open that URL in the browser.

The Angular application and Web API are both hosted by ASP.NET Core.

## Swagger

Swagger UI is available at:

```text
http://localhost:<port>/swagger
```

Example:

```text
http://localhost:5282/swagger
```

## API Endpoints

### Medicines

```text
GET /api/medicines
```

Returns the medicine list.

Search by medicine name:

```text
GET /api/medicines?search=para
```

Add medicine:

```text
POST /api/medicines
```

### Sales

```text
GET /api/sales
```

Returns medicine sale records.

```text
POST /api/sales
```

Records a medicine sale and reduces the available stock quantity.

## Data Storage

The application stores data in JSON files on the server:

```text
Pharmacy.Api/Data/medicines.json
Pharmacy.Api/Data/sales.json
```

No external database configuration is required.

## Frontend Development

The Angular source code is available in:

```text
pharmacy-ui/
```

To run Angular separately during development:

```bash
cd pharmacy-ui
npm install
npm start
```

The development server normally runs at:

```text
http://localhost:4200
```

## Rebuild Angular for ASP.NET Core Hosting

If frontend changes are made:

```bash
cd pharmacy-ui
npm install
npm run build
```

Then copy the contents of:

```text
pharmacy-ui/dist/pharmacy-ui/browser/
```

into:

```text
Pharmacy.Api/wwwroot/
```

On macOS/Linux, from the repository root:

```bash
rm -rf Pharmacy.Api/wwwroot
mkdir -p Pharmacy.Api/wwwroot
cp -R pharmacy-ui/dist/pharmacy-ui/browser/. Pharmacy.Api/wwwroot/
```

Then run:

```bash
dotnet run --project Pharmacy.Api
```

## Notes

The application intentionally uses a simple layered structure:

```text
Controller
   ↓
Service
   ↓
Repository
   ↓
JSON Storage
```

This keeps the solution simple while maintaining separation of concerns.
