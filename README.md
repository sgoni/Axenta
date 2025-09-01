# 📄 Axenta API Documentation

## Table of Contents

- [Built With](#built-with)
- [Overview](#overview)
- [Authentication](#authentication)
- [Base URL](#base-url)
- [Response Format](#response-format)
- [Error Handling](#error-handling)
- [Rate Limiting](#rate-limiting)
- [Endpoints](#endpoints)
  - [Health](#health) 
  - [Periods](#periods)
  - [Journal Entries](#journal-entries)
  - [Document References](#document-references)
  - [Currency Exchange Rate](#currency-exchange-rates)
  - [Cost Centers](#cost-centers)
  - [Companies](#companies)   
  - [Audit Logs](#audit-logs)
  - [Accounts](#accounts)
  - [Account Types](#account-types)
  - [Reports](#reports)  
- [Data Models](#data-models)
- [Usage Examples](#usage-examples)
- [SDKs and Libraries](#sdks-and-libraries)
- [Changelog](#changelog)

### Built With

* ![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
* ![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)
* ![Postgres](https://img.shields.io/badge/postgres-%23316192.svg?style=for-the-badge&logo=postgresql&logoColor=white)

## Overview

The Accounting API provides a complete set of endpoints for managing enterprise accounting systems, including:

### Key Features

- ✅ Multi-company management with multi-company support
- ✅ Accounting periods with opening/closing control
- ✅ Accounting journal entries with automatic validation (debit = credit)
- ✅ Hierarchical chart of accounts with account types
- ✅ Multi-currency support with exchange rates
- ✅ Document references for traceability
- ✅ Full audit trail of all transactions
- ✅ Journal entry reversals for corrections

### Proposed Software Architecture

#### Microservices-based architecture

* Frontend: Angular (SPA)
* API Gateway: ASP.NET Core + Ocelot/YARP
* Backend microservices: .NET 8, organized by business context
* Messaging: RabbitMQ or AWS SNS/SQS
* Authentication: IdentityServer4 or Amazon Cognito
* Database: PostgreSQL or SQL Server per microservice (Database per Service)
* Containers: Docker
* Orchestration: ECS or Kubernetes (EKS on AWS)
* CI/CD: GitHub Actions + AWS CodePipeline
* Observability: Prometheus + Grafana or AWS CloudWatch

### Functional Modules of the System

1. Accounting Catalog
   - Chart of Accounts, Account Types, Hierarchies
2. General Accounting
   - Accounting Entries, Journals, General Ledger
3. Accounts Payable
   - Vendors, Invoices, Payments
4. Accounts Receivable
   - Customers, Invoices, Collections
5. Bank Reconciliations
6. Fixed Assets
7. Taxes and Withholdings
8. Financial Reports
   - Balance Sheet, Income Statement
9. User and Role Management
   - Authentication and Authorization
10. Event Audit
    - Logs, Critical Data Changes

### API Version

Current version: **v1.0**

## Authentication

> **Note:** Add your authentication method here (API Key, Bearer Token, OAuth, etc.)

```bash
# Example with API Key
curl -H "Authorization: Bearer YOUR_API_KEY" \
     -H "Content-Type: application/json" \
     https://api.example.com/v1/periods
```

## Base URL

```abc
Production: https://api.example.com/v1
Staging: https://staging-api.example.com/v1
```
## Development URL
```abc
Accounting.Api: https://localhost:5050
Report.Api: https://localhost:6060
```

## Response Format

All API responses follow a consistent JSON structure:

### Success Response
```json
{
  "data": { /* Response data */ },
  "meta": {
    "timestamp": "2024-01-15T10:30:00Z",
    "version": "1.0"
  }
}
```

### Paginated Response
```json
{
  "pageIndex": 0,
  "pageSize": 10,
  "count": 150,
  "data": [/* Array of items */]
}
```

## Error Handling

The API uses standard HTTP status codes and returns detailed error information:

### Error Response Format
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "Bad Request",
  "status": 400,
  "detail": "The request contains invalid parameters",
  "instance": "/periods",
  "errors": {
    "PageSize": ["PageSize must be between 1 and 100"]
  }
}
```

### Common HTTP Status Codes
| Code | Description |
|------|-------------|
| `200` | OK - Request successful |
| `201` | Created - Resource created successfully |
| `400` | Bad Request - Invalid request parameters |
| `401` | Unauthorized - Authentication required |
| `403` | Forbidden - Insufficient permissions |
| `404` | Not Found - Resource not found |
| `409` | Conflict - Resource already exists |
| `422` | Unprocessable Entity - Validation failed |
| `500` | Internal Server Error - Server error |

## Rate Limiting

> **Note:** Add your rate limiting information here

- **Rate Limit:** 1000 requests per hour per API key
- **Headers:** Check `X-RateLimit-Remaining` and `X-RateLimit-Reset` headers

## Pagination

All endpoints that return lists support pagination:

**Parameters:**
- `Pageindex`: page number (base 0, default: 0)
- `Pagesize`: Elements per page (default: 10, maximum: 100)

**Response format:**
```json
{
  "pageIndex": 0,
  "pageSize": 10,
  "count": 45,
  "data": [...]
}
```

## Installation

1. Clone the repo
   ```sh
   git clone https://github.com/sgoni/Axenta.git
   ```

## Endpoints

### Health

```http
GET /health
```

**Example of application:**
```bash
curl -sS -H "Accept: application/json" https://localhost:5050/health
```

### Periods

Accounting periods define time windows for accounting transactions.

#### List Accounting Periods

```http
GET /periods
```

**Parameters:**

| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `PageIndex` | integer | 0 | Page number (base 0)
| `PageSize` | integer | 10 | Items per page (1-100)

**Sample application:**
```bash
curl -X 'GET' 
  'https://localhost:5050/periods?PageIndex=0&PageSize=10' 
  -H 'accept: application/json'
```

**Sample Answer:**
```json
{
  "periods": {
    "pageIndex": 0,
    "pageSize": 20,
    "count": 12,
    "data": [
      {
        "id": "123e4567-e89b-12d3-a456-426614174000",
        "year": 2024,
        "month": 1,
        "startDate": "2024-01-01T00:00:00Z",
        "endDate": "2024-01-31T23:59:59Z",
        "isClosed": false
      }
    ]
  }
}
```

Create Period

```http
POST /periods
```

**Parameters:**
| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `companyId` | UUID | Yes | ID of the company

**Body of the Request:**
```json
{
  "period": {
    "companyId": "41607051-4bd8-4a54-a5e2-cb713aef6ca2"
  }
}
```

**Sample Application:**
```bash
 curl -X 'POST' 
  'https://localhost:5050/periods' 
  -H 'accept: application/json' 
  -H 'Content-Type: application/json' 
  -d '{
  "period": {
    "companyId": "41607051-4bd8-4a54-a5e2-cb713aef6ca2"
  }
```     

**Sample Answer:**
```json
{
   "periodId": "123e4567-e89b-12d3-a456-426614174001"
}
```

#### Closing Period

```http
PUT /periods/close
```

**Parameters:**
| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `periodId` | UUID | Yes | ID of the period to close
| `companyId` | UUID | Yes | ID of the company

**Body of the Request:**
```json
{
  "period": {
    "periodId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "companyId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  }
}
```

**Sample Application:**
```bash
 curl -X 'PUT' 
  'https://localhost:5050/periods/close' 
  -H 'accept: application/json' 
  -H 'Content-Type: application/json' 
  -d '{
  "period": {
    "periodId": "e44ed594-272c-4978-a3b5-11fb47e9ca12",
    "companyId": "41607051-4bd8-4a54-a5e2-cb713aef6ca2"
  }
}'
```

**Sample Answer:**

```json
{
  "isSuccess": true
}
```

#### Check Existence of Period

```http
GET /periods/year={year}&month={month}
```

**Parameters:**

| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `year` | integer | Yes | Year of the period
| `month` | integer | Yes | Month of the period (1-12)

**Sample Application:**

```bash
curl -X 'GET' 
 'https://localhost:5050/periods/year=2025&month=7' 
  -H 'accept: application/json'
```

**Sample Answer:**

```json
{
  "isSuccess": true
}
```

#### Get Period by ID

```http
GET /periods/{periodId}
```

**Parameters:**

| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `periodId` | UUID | Yes | ID of the period to get

**Sample Application:**

```bash
curl -X 'GET' \
  'https://localhost:5050/periods/e44ed594-272c-4978-a3b5-11fb47e9ca12' \
  -H 'accept: application/json'
```

**Sample Answer:**

```json
{
  "periodDetail": {
    "id": "e44ed594-272c-4978-a3b5-11fb47e9ca12",
    "year": 2014,
    "month": 10,
    "startDate": "2025-08-01T00:00:00Z",
    "endDate": "2025-08-31T00:00:00Z",
    "isClosed": true
  }
}
```

#### Reopen Period

```http
PUT /periods/open
```

**Parameters:**

| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `periodId` | UUID | Yes | ID of the period to open
| `companyId` | UUID | Yes | ID of the company

**Body of the Request:**
```json
{
  "period": {
    "periodId": "e44ed594-272c-4978-a3b5-11fb47e9ca12",
    "companyId": "41607051-4bd8-4a54-a5e2-cb713aef6ca2"
  }
}
```

**Sample Application:**
```bash
 curl -X 'PUT' 
  'https://localhost:5050/periods/open' 
  -H 'accept: application/json' 
  -H 'Content-Type: application/json' 
  -d '{
  "period": {
    "periodId": "e44ed594-272c-4978-a3b5-11fb47e9ca12",
    "companyId": "41607051-4bd8-4a54-a5e2-cb713aef6ca2"
  }
}'
```

**Sample Answer:**
```json
{
  "isSuccess": true
}
```


### Journal Entries

Accounting entries record financial transactions with automatic debit = credit validation.

#### Create Accounting Entry

```http
POST /journal-entries
```

**Parameters:**

| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `Id` | UUID | Yes | ID of the period to open
| `description` | string | Yes | Journal description
| `periodId` | UUID | Yes | ID of the period
| `companyId` | UUID | Yes | ID of the company
| `currencyCode` | string | Yes | CUrrency code
| `exchangeRate` | Decimal | No | Exchange rate
| `exchangeRateDate` | UUID | No | Exchange rate date
| `accountId` | UUID | Yes | ID of the company
| `debit` | UUID | Yes | Balance debit
| `credit` | UUID | Yes | Balance credit
| `lineNumber` | UUID | Yes | Line number

**Body of the Request:**

```json
{
  "journalEntry": {
    "date": "2025-08-01T02:26:27.053Z",
    "description": "Registro de ventas demo.",
    "periodId": "e44ed594-272c-4978-a3b5-11fb47e9ca12",
    "companyId": "41607051-4bd8-4a54-a5e2-cb713aef6ca2",
    "currencyCode": "CRC",
    "exchangeRate": 0,
    "exchangeRateDate": "2025-08-14",    
    "lines": [
      {
        "accountId": "7573e77c-110b-4a3b-9bda-7be306bb14b4",
        "debit": 4000,
        "credit": 0,
        "lineNumber": 1
      },
      {
        "accountId": "cacfc56f-82ca-4cd5-b694-540b5e1b2e03",
        "debit": 0,
        "credit": 4000,
        "lineNumber": 2
      }
    ]
  }
}
```

**Validation Rules:**
- ✅ The total debits must equal the total credits
- ✅ At least two lines are required
- ✅ All amounts must be positive
- ✅ Valid account IDs are required
- ✅ The period must be open

**Sample Application:**
```bash
curl -X 'POST' \
  'https://localhost:5050/journal-entries' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "journalEntry": {
    "date": "2025-08-01T02:26:27.053Z",
    "description": "Registro de ventas demo.",
    "periodId": "e44ed594-272c-4978-a3b5-11fb47e9ca12",
    "companyId": "41607051-4bd8-4a54-a5e2-cb713aef6ca2",
    "currencyCode": "CRC",
    "exchangeRate": 0,
    "exchangeRateDate": "2025-08-14",    
    "lines": [
      {
        "accountId": "7573e77c-110b-4a3b-9bda-7be306bb14b4",
        "debit": 4000,
        "credit": 0,
        "lineNumber": 1
      },
      {
        "accountId": "cacfc56f-82ca-4cd5-b694-540b5e1b2e03",
        "debit": 0,
        "credit": 4000,
        "lineNumber": 2
      }
    ]
  }
}'
```     

**Sample Answer:**
```json
{
   "id": "123e4567-e89b-12d3-a456-426614174004"
}
```

**Note:** Entries can only be modified if the period is open.

#### List Accounting Entries

```http
GET /journal-entries
```

**Parameters:**

| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `PageIndex` | integer | 0 | Page number
| `PageSize` | integer | 10 | Elements per page
| `periodId` | UUID | Yes | ID of the period
| `companyId` | UUID | Yes | ID of the company

**Sample Application:**
```bash
 curl -X 'GET' \
   'https://localhost:5050/journal-entries?PageIndex=0&PageSize=10&PeriodId=e44ed594-272c-4978-a3b5-11fb47e9ca12&CompanyId=41607051-4bd8-4a54-a5e2-cb713aef6ca2' \
   -H 'accept: application/json'
```

**Sample Answer:**
```json
{
  "journalEntries": {
    "pageIndex": 0,
    "pageSize": 0,
    "count": 0,
    "data": [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "date": "2025-08-14T21:21:36.052Z",
        "description": "string",
        "periodId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "companyId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "currencyCode": "USD",
        "exchangeRate": 525,
        "exchangeRateDate": "2025-08-14",
        "isPosted": true,
        "isReversed": true,
        "lines": [
          {
            "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "journalEntryId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "accountId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "debit": 50000,
            "credit": 0,
            "lineNumber": 0
          },
          {
            "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "journalEntryId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "accountId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "debit": 0,
            "credit": 50000,
            "lineNumber": 0
          }          
        ]
      }
    ]
  }
}
```

#### Update Accounting Entry

```http
PUT /journal-entries
```

**Parameters:**

| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `id` | UUID | Yes | ID of the seat
| `description` | string | Yes | Journal description
| `periodId` | UUID | Yes | ID of the period
| `companyId` | UUID | Yes | ID of the company
| `currencyCode` | string | Yes | CUrrency code
| `exchangeRate` | Decimal | No | Exchange rate
| `exchangeRateDate` | UUID | No | Exchange rate date
| `accountId` | UUID | Yes | ID of the company
| `debit` | UUID | Yes | Balance debit
| `credit` | UUID | Yes | Balance credit
| `lineNumber` | UUID | Yes | Line number

**Body of the Request:**

```json
{
  "journalEntry": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "date": "2025-08-14T21:32:31.782Z",
    "description": "string",
    "currencyCode": "string",
    "exchangeRate": 0,
    "exchangeRateDate": "2025-08-14",
    "isPosted": true,
    "lines": [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "journalEntryId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "accountId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "debit": 0,
        "credit": 0,
        "lineNumber": 0
      }
    ]
  }
}
```

**Validation Rules:**
- ✅ The total debits must equal the total credits
- ✅ At least two lines are required
- ✅ All amounts must be positive
- ✅ Valid account IDs are required
- ✅ The period must be open

**Sample Application:**
```bash
 curl -X 'PUT' 
  'https://localhost:5050/journal-entries' 
  -H 'accept: application/json' 
  -H 'Content-Type: application/json' 
  -d '{
  "journalEntry": {
    "id": "b2d06aa7-3ae6-4741-93ac-68e1d900a262",
    "date": "2025-08-01T02:26:27.053Z",
    "description": "Registro de ventas por contado.",
    "currencyCode": "CRC",
    "exchangeRate": 0,
    "exchangeRateDate": "2025-08-14",
    "isPosted": true,
    "periodId": "e44ed594-272c-4978-a3b5-11fb47e9ca12",
    "lines": [
      {
        "id": "98723cc8-2f31-40a1-a4b0-aeb6ff085f86",
        "journalEntryId": "b2d06aa7-3ae6-4741-93ac-68e1d900a262",
        "accountId": "7573e77c-110b-4a3b-9bda-7be306bb14b4",
        "debit": 5000
        "credit": 0,
        "lineNumber": 1
      },
      {
        "id": "f3d8e587-6492-490d-823d-d1bffdf390fe",
        "journalEntryId": "b2d06aa7-3ae6-4741-93ac-68e1d900a262",
        "accountId": "cacfc56f-82ca-4cd5-b694-540b5e1b2e03",
        "debit": 0,
        "credit": 5000
        "lineNumber": 2
      }	  
    ]
  }
}'
```

**Sample Answer:**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

#### Delete Accounting Entry

```http
DELETE /journal-entries/{id}
```

**Parameters:**

| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `id` | UUID | Yes | ID of the seat to be deleted

**Validation Rules:**
- ✅ Valid account IDs are required
- ✅ The period must be open
- ✅ The journal entry is not posted

**Sample Application:**
```bash
curl -X 'DELETE' 
  'https://localhost:5050/journal-entries/896dd1df-1413-4098-bbbd-d5586cb8f86e' 
  -H 'accept: application/json'
```

**Sample Answer:**
```json
{
  "isSuccess": true
}
```

#### Get Seat by ID

```http
GET /journal-entries/{journalEntryId}
```

**Parameters:**

| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `id` | UUID | Yes | ID of the seat

**Sample Application:**
```bash
curl -X 'GET' 
 'https://localhost:5050/journal-entries/56cb166e-07b6-44f7-bd8b-cbc16c595946' 
 -H 'accept: application/json'
```

**Sample Answer:**
```json
{
  "journalEntryDetail": {
    "id": "56cb166e-07b6-44f7-bd8b-cbc16c595946",
    "date": "2014-10-20T00:00:00Z",
    "description": "Compra de mercadería para la venta",
    "periodId": "e44ed594-272c-4978-a3b5-11fb47e9ca12",
    "companyId": "41607051-4bd8-4a54-a5e2-cb713aef6ca2",
    "currencyCode": "CRC",
    "exchangeRate": 515.00,
    "exchangeRateDate": "2014-10-20",
    "isPosted": true,
    "isReversed": false,
    "lines": [
      {
        "id": "3f9069b0-4700-4e46-a14c-289df3317419",
        "journalEntryId": "56cb166e-07b6-44f7-bd8b-cbc16c595946",
        "accountId": "057a93fb-c93b-4cf7-a79e-fab3583f2f13",
        "debit": 2000000.00,
        "credit": 0.00,
        "lineNumber": 1
      },
      {
        "id": "ca0caa52-08d6-4c4a-8b03-273a710d385c",
        "journalEntryId": "56cb166e-07b6-44f7-bd8b-cbc16c595946",
        "accountId": "a04ee151-03d4-4840-8697-600360d6977d",
        "debit": 0.00,
        "credit": 2000000.00,
        "lineNumber": 2
      }
    ]
  }
}
```
#### Reverse an entry (only if the period is open)

```http
PUT /journal-entries/{id}/reverse
```

**Parameters:**

| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `id` | UUID | Yes | ID of the seat to be deleted

**Validation Rules:**
- ✅ Valid account IDs are required
- ✅ The period must be open

**Sample Application:**
```bash
curl -X 'PUT' 
 'https://localhost:5050/journal-entries3d5eeef9-d3a9-43e4-9b60-dd4a16e20ba7/reverse' 
 -H 'accept: application/json'
```

**Sample Answer:**
```json
{
  "isSuccess": true
}
```

### Document References

Management of references to external documents associated with accounting entries.

#### Create Document Reference

```http
POST /journal-entries/{id}/documents
```

**Parameters:**

| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `id` | UUID | Yes | Journal Entry ID

**Validation Rules:**
- ✅ The journal entry must exist

**Body of the Request:**
```json
{
  "documentReference": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "journalEntryId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "sourceType": "string",
    "sourceId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "referenceNumber": "string",
    "description": "string"
  }
}
```

**Sample Application:**
```bash
curl -X 'POST' \
  'https://localhost:5050/journal-entries/{id}/documents' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "documentReference": {
    "journalEntryId": "0b55189d-ce04-471f-abbb-f73208be063a",
    "sourceType": "PAYMENT",
    "sourceId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "referenceNumber": "ABC123",
    "description": "Purchase of merchandise on credit"
  }
}'
```

**Sample Answer:**
```json
{
  "id": "b2c6d0ca-cd5d-4154-951c-82568c4f01fd"
}
```

#### Get References by Seat

```http
GET /journal-entries/{id}/documents
```

**Parameters:**

| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `id` | UUID | Yes | Journal Entry ID

**Validation Rules:**
- ✅ The journal entry must exist

**Sample Application:**
```bash
 curl -X 'GET' 
  'https://localhost:5050/journal-entries/3fa85f64-5717-4562-b3fc-2c963f66afa6/documents' 
  -H 'accept: application/json'
```

**Sample Answer:**
```json
{
  "documentReferences": [
    {
      "id": "821d2b0b-112d-4f24-804e-4d95a8697d65",
      "journalEntryId": "0b55189d-ce04-471f-abbb-f73208be063a",
      "sourceType": "Loan",
      "sourceId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "referenceNumber": "123456789",
      "description": "Registro de préstamo con la entidad financiera"
    }
  ]
}
```

### Currencies

Administration of exchange rates and currencies.

#### Create a new exchange rate

```http
POST /currencies
```

**Parameters:**

| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `id` | UUID | Yes | Company ID
| `currencyCode` | string | Yes | Currency code
| `date` | Date | Yes | Exchange rate date
| `buyRate` | Decimal | Yes | Exchange rate purchase
| `sellRate` | Decimal | Yes | Exchange rate sale

**Body of the Request:**
```json
{
  "currencyExchangeRate": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "currencyCode": "string",
    "date": "2025-08-14",
    "buyRate": 0,
    "sellRate": 0
  }
}
```

**Sample Application:**
```bash
curl -X 'POST' \
  'https://localhost:5050/currencies' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "currencyExchangeRate": {
    "currencyCode": "USD",
    "date": "2025-08-14",
    "buyRate": 495,
    "sellRate": 510
  }
}'
```

**Sample Answer:**
```json
{
  "id": "66f63e17-b4ec-4624-a6a8-121515b47927"
}
```

#### List exchange rates

```http
GET /currencies
```

**Parameters:**
| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `PageIndex` | integer | 0 | Page number
| `PageSize` | integer | 10 | Elements per page

**Sample Application:**
```bash
curl -X 'GET' \
  'https://localhost:5050/currencies?PageIndex=0&PageSize=10' \
  -H 'accept: application/json'
```

**Sample Answer:**
```json
{
  "currencyExchangeRates": {
    "pageIndex": 0,
    "pageSize": 10,
    "count": 3,
    "data": [
      {
        "id": "c9726881-322c-4de3-bf52-fc03ea9cea67",
        "currencyCode": "USD",
        "date": "2014-10-01",
        "buyRate": 310,
        "sellRate": 315
      }
    ]
  }
}
```

#### Get the daily currency exchange rate

```http
GET /currencies/daily
```

**Sample Application:**
```bash
curl -X 'GET' \
  'https://localhost:5050/currencies/daily' \
  -H 'accept: application/json'
```

**Sample Answer:**
```json
{
  "currencyExchangeRate": {
    "id": "57ba3603-9249-40d3-8f07-61d67ab42b0f",
    "currencyCode": "USD",
    "date": "2025-08-14",
    "buyRate": 499,
    "sellRate": 513
  }
}
```

### Cost Centers

Administration of accounting costs

#### Activate cost center.

```http
put /cost-centers/{id}/activate
```

**Parameters:**
| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `Id` | UUID | Yes | Cost center ID to activate

**Sample Application:**
```bash
 curl -X 'PUT' \
  'https://localhost:5050/cost-centers/cfc933f3-0ba7-41b1-bbcd-9a766e547b26/activate' \
  -H 'accept: application/json'
```

**Sample Answer:**
```json
{
  "isSuccess": true
}
```

#### Create a new cost center

```http
post /cost-centers
```

**Parameters:**
| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `name` | string | No | Cost center name
| `description` | string | Yes | Description of the cost center
| `isActive` | bool | Yes | Active or inactive Cost Center
| `companyId` | UUID | Yes | Legal identifier of the company
| `parentCostCenterId` | UUID | Yes | Parent cost center ID

**Body of the Request:**
```json
{
  "costCenter": {
    "name": "string",
    "description": "string",
    "companyId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "parentCostCenterId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  }
}
```

**Sample Application:**
```bash
  curl -X 'POST' 
   'https://localhost:5050/cost-centers' 
   -H 'accept: application/json' 
   -H 'Content-Type: application/json' 
   -d '{
   "costCenter": {
     "name": "Administración",
     "description": "Gastos administrativos generales",
     "isActive": true,
     "companyId": "41607051-4bd8-4a54-a5e2-cb713aef6ca2",
     "parentCostCenterId": null
   }
```

**Sample Answer:**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

#### Modify an existing cost center

```http
PUT /cost-centers
```

**Parameters:**
| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `name` | string | No | Cost center name
| `description` | string | Yes | Description of the cost center
| `isActive` | bool | Yes | Active or inactive Cost Center
| `parentCostCenterId` | UUID | Yes | Parent cost center ID

**Body of the Request:**
```json
{
  "costCenter": {
    "name": "string",
    "description": "string",
    "companyId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "parentCostCenterId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  }
}
```

**Sample Application:**
```bash
curl -X 'PUT' \
  'https://localhost:5050/cost-centers' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "costCenter": {
    "id": "fa19b15d-6fdc-465d-8c01-817625211286",
    "name": "Gastos operativos",
    "description": "Gastos operativos",
    "isActive": true,
    "parentCostCenterId": null
  }
}'
```

**Sample Answer:**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

#### Deactivate cost center.

```http
put /cost-centers/{id}/deactivate
```

**Parameters:**
| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `Id` | UUID | Yes | Cost center to deactivate

**Sample Application:**
```bash
curl -X 'PUT' \
  'https://localhost:5050/cost-centers/cfc933f3-0ba7-41b1-bbcd-9a766e547b26/deactivate' \
  -H 'accept: application/json'
```

**Sample Answer:**
```json
{
  "isSuccess": true
}

### Companies

Company administration

#### Create a new company

```http
POST /companies
```

**Parameters:**
| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `id` | UUID | No | Company ID
| `name` | string | Yes | Company name
| `taxId` | string | Yes | Legal identifier of the company
| `country` | string | Yes | Name of the country
| `currencyCode` | string | Yes | Currency code
| `isActive` | boolean | No | Company active

**Body of the Request:**
```json
{
  "company": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "string",
    "taxId": "string",
    "country": "string",
    "currencyCode": "string",
    "isActive": true
  }
}
```

**Sample Application:**
```bash
 curl -X 'POST' 
  'https://localhost:5050/companies' 
  -H 'accept: application/json' 
  -H 'Content-Type: application/json' 
  -d '{
  "account": {
    "name": "Demo",
    "taxId": "3101268467",
    "country": "CR",
    "currencyCode": "USD",
    "isActive": true
  }
}'
```

**Sample Answer:**
```json
{
  "currencyExchangeRate": {
    "id": "57ba3603-9249-40d3-8f07-61d67ab42b0f",
    "currencyCode": "USD",
    "date": "2025-08-14",
    "buyRate": 499,
    "sellRate": 513
  }
}
```

#### Get list of companies

```http
GET /companies
```

**Parameters:**
| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `PageIndex` | integer | 0 | Page number
| `PageSize` | integer | 10 | Elements per page

**Sample Application:**
```bash
 curl -X 'GET' 
  'https://localhost:5050/companies?PageIndex=0&PageSize=10' 
  -H 'accept: application/json'
```

**Sample Answer:**
```json
{
  "companies": {
    "pageIndex": 0,
    "pageSize": 10,
    "count": 34,
    "data": [
      {
        "id": "41607051-4bd8-4a54-a5e2-cb713aef6ca2",
        "name": "ABC S.A",
        "taxId": "3101009240",
        "country": "CR",
        "currencyCode": "CRC",
        "isActive": true
      },
      {
        "id": "d30bbc16-c7f6-458c-84fa-ffb1c7727950",
        "name": "XYZ S.A",
        "taxId": "3101168458",
        "country": "CR",
        "currencyCode": "CRC",
        "isActive": true
      }
    ]
  }
}
```

#### Update an existing company

```http
PUT /companies
```

**Parameters:**
| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `id` | UUID | Yes | Company ID
| `name` | string | Yes | Company name
| `taxId` | string | Yes | Legal identifier of the company
| `country` | string | Yes | Name of the country
| `currencyCode` | string | Yes | Currency code
| `isActive` | boolean | Yes | Company active

**Body of the Request:**
```json
{
  "company": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "string",
    "taxId": "string",
    "country": "string",
    "currencyCode": "string",
    "isActive": true
  }
}
```

**Sample Application:**
```bash
 curl -X 'PUT' 
  'https://localhost:5050/companies' 
  -H 'accept: application/json' 
  -H 'Content-Type: application/json' 
  -d '{
  "company": {
        "id": "9429a754-221f-45ca-aeeb-f8b443157831",
        "name": "Demo",
        "taxId": "3101268467",
        "country": "CR",
        "currencyCode": "USD",
        "isActive": false
  }
}'
```

**Sample Answer:**
```json
{
  "isSuccess": true
}
```

#### Get a specific company

```http
GET /companies/{companyId}
```

**Parameters:**
| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `companyId` | UUID | Yes | Company ID

**Sample Application:**
```bash
 curl -X 'GET' 
  'https://localhost:5050/companies/41607051-4bd8-4a54-a5e2-cb713aef6ca2' 
  -H 'accept: application/json'
```

**Body of the Request:**
```json
{
  "companyDetail": {
    "id": "41607051-4bd8-4a54-a5e2-cb713aef6ca2",
    "name": "ABC S.A",
    "taxId": "3101009240",
    "country": "CR",
    "currencyCode": "CRC",
    "isActive": true
  }
}
```

### Audit Logs

View the history of changes and operations performed in the system.

#### Get Audit Log by ID

```http
GET /audit-logs/{id}
```

**Parameters:**
| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `id` | UUID | Yes | Company ID

**Sample Application:**
```bash
 curl -X 'GET' 
  'https://localhost:5050/audit-logs/e44ed594-272c-4978-a3b5-11fb47e9ca12' 
  -H 'accept: application/json'
```

**Body of the Request:**
```json
{
  "auditDetail": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "entity": "string",
    "action": "string",
    "performedBy": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "performedAt": "2025-08-15T16:22:28.446Z",
    "details": "string"
  }
}
```

**Sample Answer:**
```json
{
  "isSuccess": true
}
```

#### List Audit Logs

```http
GET /audit-logs
```

**Parameters:**

| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `PageIndex` | integer | 0 | Page number
| `PageSize` | integer | 10 | Elements per page

**Sample Answer:**
```json
{
   "auditlogs": {
      "pageIndex": 0,
      "pageSize": 10,
      "count": 150,
      "data": [
         {
            "id": "123e4567-e89b-12d3-a456-426614174012",
            "entity": "JournalEntry",
            "action": "Create",
            "performedBy": "123e4567-e89b-12d3-a456-426614174013",
            "performedAt": "2024-01-15T10:30:00Z",
            "details": "Creado asiento contable por $1,500.00"
         }
      ]
   }
}
```

### Accounts

Chart of accounts management with support for hierarchical structure.

#### Activate Account

```http
PUT /accounts/{id}/activate
```

**Parameters:**
| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `id` | UUID | Yes | Account ID

**Sample Application:**
```bash
 curl -X 'PUT' 
  'https://localhost:5050/accounts/cacfc56f-82ca-4cd5-b694-540b5e1b2e03/activate' 
  -H 'accept: application/json'
```

**Sample Answer:**
```json
{
  "isSuccess": true
}
```
#### Create Account

```http
POST /accounts
```

**Parameters:**
| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `code` | string | No | Multi-level account code
| `name` | string | Yes | Account name
| `accountTypeId` | string | Yes | Account type (Liability, asset, equity, etc.)
| `parentAccountId` | string | Yes | Parent account ID
| `isActive` | boolean | No | Account is active
| `isMovable` | boolean | Yes | Accepts daily seat movements

**Body of the Application:**
```json
{
   "account": {
      "code": "1100",
      "name": "Efectivo y Equivalentes",
      "accountTypeId": "123e4567-e89b-12d3-a456-426614174004",
      "parentAccountId": null,
      "isActive": true,
      "isMovable": true
   }
}
```

**Sample Application:**
```bash
curl -X POST "https://api.accounting.com/v1/accounts" \
     -H "Authorization: Bearer YOUR_API_KEY" \
     -H "Content-Type: application/json" \
     -d '{
       "account": {
         "code": "1100",
         "name": "Efectivo y Equivalentes",
         "accountTypeId": "123e4567-e89b-12d3-a456-426614174004",
         "isActive": true,
         "isMovable": true
       }
     }
```     

**Sample Answer:**
```json
{
   "id": "123e4567-e89b-12d3-a456-426614174005"
}
```

#### List Accounts

```http
GET /accounts
```

**Parameters:**

| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `PageIndex` | integer | 0 | Page number
| `PageSize` | integer | 10 | Elements per page


#### Update Account

```http
PUT /accounts
```

**Parameters:**
| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `id` | UUID | Yes | Account ID
| `code` | string | No | Multi-level account code
| `name` | string | Yes | Account name
| `accountTypeId` | string | Yes | Account type (Liability, asset, equity, etc.)
| `parentAccountId` | string | No | Parent account ID
| `isActive` | boolean | No | Account is active
| `isMovable` | boolean | No | Accepts daily seat movements

**Sample Application:**
```bash
curl -X 'PUT' 
 'https://localhost:5050/accounts' 
 -H 'accept: application/json' 
 -H 'Content-Type: application/json' 
 -d '{
 "account": {
   "id": "00445bc5-6287-4b1c-8af9-76df7fb37ac2",
   "code": "7006",
   "name": "Gastos varios",
   "accountTypeId": "71bbd6e0-abf4-4f2a-afec-9199bb404b08",
   "isActive": true
 }
'
```

**Sample Answer:**
```json
{
  "isSuccess": true
}
```

#### Delete Account

```http
DELETE /accounts/{id}
```
**Parameters:**
| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `Id` | UUID | Yes | Account ID to physical delete

Delete a phisical record account.

**Sample Application:**
```bash
curl -X 'DELETE' 
 'https://localhost:5050/accounts/0065ecee-ee6b-4d42-bb8b-0e24b20c213f' 
 -H 'accept: application/json'
```

**Sample Answer:**
```json
{
  "isSuccess": true
}
```

#### Deactivate Account

```http
PUT /accounts/{id}/desactivate
```

**Parameters:**
| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `Id` | UUID | Yes | Account ID to deactivate

**Sample Application:**
```bash
 curl -X 'PUT' 
  'https://localhost:5050/accounts/cacfc56f-82ca-4cd5-b694-540b5e1b2e03/desactivate' 
  -H 'accept: application/json'
```

**Sample Answer:**
```json
{
  "isSuccess": true
}
```

#### Get Account by ID

```http
GET /accounts/{accountId}
```

**Parameters:**
| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `accountId` | UUID | Yes | Account ID to get

**Sample Application:**
```bash
curl -X 'GET' \
  'https://localhost:5050/accounts/7573e77c-110b-4a3b-9bda-7be306bb14b4' \
  -H 'accept: application/json'
```

**Sample Answer:**
```json

  "accountDetail": {
    "id": "7573e77c-110b-4a3b-9bda-7be306bb14b4",
    "code": "1",
    "name": "Activo",
    "accountTypeId": "9546c264-0869-44d9-8031-371159b76d3f",
    "parentAccountId": null,
    "isActive": true,
    "level": 1,
    "isMovable": false
  }
```

#### Get Account Tree

```http
GET /accounts/tree
```

Returns accounts in a hierarchical structure.

**Sample Application:**
```bash
 curl -X 'GET' 
  'https://localhost:5050/accounts/tree' 
  -H 'accept: application/json'
```

**Sample Response:**
```json
{
   "accountTree": 
   [
      {
         "id": "123e4567-e89b-12d3-a456-426614174005",
         "code": "1000",
         "name": "Activos",
         "accountTypeId": "123e4567-e89b-12d3-a456-426614174006",
         "parentAccountId": null,
         "children": [
            {
               "id": "123e4567-e89b-12d3-a456-426614174007",
               "code": "1100",
               "name": "Activos Corrientes",
               "accountTypeId": "123e4567-e89b-12d3-a456-426614174006",
               "parentAccountId": "123e4567-e89b-12d3-a456-426614174005",
               "children": []
            }
         ]
      }
   ]
}
```

### Account Types

Manage account types (Assets, Liabilities, Equity, etc.).

#### List Account Types

```http
GET /accounts/types
```

**Sample Application:**
```bash
 curl -X 'GET' 
  'https://localhost:5050/accounts/tree' 
  -H 'accept: application/json'
```

**Sample Answer:**
```json
{
   "accountTypes": [
      {
         "id": "123e4567-e89b-12d3-a456-426614174008",
         "name": "Activos",
         "description": "Recursos controlados por la entidad"
      },
      {
         "id": "123e4567-e89b-12d3-a456-426614174009",
         "name": "Pasivos",
         "description": "Obligaciones presentes de la entidad"
      },
      {
         "id": "123e4567-e89b-12d3-a456-426614174010",
         "name": "Patrimonio",
         "description": "Participación residual en los activos"
      }
   ]
}
```

### Reports

Generation of accounting reports.

#### Income statament report

```http
GET /reports/income-statement?periodId={periodId}&companyId={companyId}
 ```

**Parameters:**
| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `periodId` | UUID | Yes | ID of the period
| `companyId` | UUID | Yes | ID of the company

**Sample Application:**
```bash
 curl -X 'GET' 
  'https://localhost:6060/reports/income-statement?periodId=e44ed594-272c-4978-a3b5-11fb47e9ca12&companyId=41607051-4bd8-4a54-a5e2-cb713aef6ca2' 
  -H 'accept: application/json'
```

**Sample Answer:**
```json
{
  "incomeStatementDto": [
    {
      "code": "5.02.01",
      "name": "Sueldos y Salarios",
      "accountType": "Gasto",
      "balance": -800000.00
    },
    {
      "code": "5.02.02",
      "name": "Servicios Públicos",
      "accountType": "Gasto",
      "balance": -500000.00
    }
  ]
}
```

#### Trial balance report

```http
GET /reports/trial-balance?periodId={periodId}&companyId={companyId}
 ```

**Parameters:**
| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `periodId` | UUID | Yes | ID of the period
| `companyId` | UUID | Yes | ID of the company

**Sample Application:**
```bash
 curl -X 'GET' 
  'https://localhost:6060/reports/trial-balance?periodId=e44ed594-272c-4978-a3b5-11fb47e9ca12&companyId=41607051-4bd8-4a54-a5e2-cb713aef6ca2' 
  -H 'accept: application/json'
```

**Sample Answer:**

```json
{
  "trialBalanceDto": [
    {
      "code": "1",
      "name": "Activo",
      "totalDebit": 4000.00,
      "totalCredit": 0.00,
      "balance": 4000.00
    },
    {
      "code": "1.01",
      "name": "Activo Corriente",
      "totalDebit": 0.00,
      "totalCredit": 4000.00,
      "balance": -4000.00
    }
  ]
}
```

#### Account balance

```http
GET /reports/account-balance/{accountId}
 ```

**Parameters:**
| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `accountId` | UUID | Yes | ID account

**Sample Application:**
```bash
 curl -X 'GET' 
  'https://localhost:6060/reports/account-balance/a04ee151-03d4-4840-8697-600360d6977d' 
  -H 'accept: application/json'
```

**Sample Answer:**

```json
{
  "balance": -1000000.00
}
```

#### Account balance by period

```http
GET /reports/account-balance/period?periodId={periodId}&accountId={accountId}
 ```

**Parameters:**
| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `periodId` | UUID | Yes | ID of the period
| `accountId` | UUID | Yes | ID account

**Sample Application:**
```bash
 curl -X 'GET' 
  'https://localhost:6060/reports/account-balance?periodId=e44ed594-272c-4978-a3b5-11fb47e9ca12&accountId=a04ee151-03d4-4840-8697-600360d6977d' 
  -H 'accept: application/json'
```

**Sample Answer:**

```json
{
  "balance": -1000000.00
}

```

#### General ledger report
```http
GET /reports/general-ledger?periodId={periodId}&companyId={companyId}
 ```

**Parameters:**
Parameter | Type | Required | Description
|-----|-----|-----|-----
| `periodId` | UUID | Yes | ID of the period
| `companyId` | UUID | Yes | ID of the company

**Sample Application:**
```bash
 curl -X 'GET' 
  'https://localhost:6060/reports/account-balance?periodId=e44ed594-272c-4978-a3b5-11fb47e9ca12&accountId=23b79d9f-96d3-4c20-ac8e-4387a70152b3' 
  -H 'accept: application/json'
```

**Sample Answer:**
```json
{
  "balance": -1000000.00
}
```

#### Balance sheet report

```http
GET /reports/balance-sheet?periodId={periodId}&companyId={companyId}
 ```

**Parameters:**
| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `periodId` | UUID | Yes | ID of the period
| `companyId` | UUID | Yes | ID of the company

**Sample Application:**
```bash
 curl -X 'GET' 
  'https://localhost:6060/reports/balance-sheet?periodId=e44ed594-272c-4978-a3b5-11fb47e9ca12&companyId=41607051-4bd8-4a54-a5e2-cb713aef6ca2' 
  -H 'accept: application/json'
```

**Sample Answer:**
```json
{
  "balanceSheet": [
    {
      "code": "1",
      "name": "Activo",
      "accountType": "Activo",
      "balance": 4000.00
    },
    {
      "code": "1.01",
      "name": "Activo Corriente",
      "accountType": "Activo",
      "balance": -4000.00
    }
  ]
}
```

## Data Models

### CompanyDto

```json
{
  "id": "string (UUID)",
  "name": "string",
  "taxId": "string",
  "country": "string",
  "currencyCode": "string",
  "isActive": "boolean"
  }
```

### PeriodDto
```json
{
  "id": "string (UUID)",
  "companyId": "string (UUID)",
  "year": "integer",
  "month": "integer",
  "startDate": "string (datetime)",
  "endDate": "string (datetime)",
  "isClosed": "boolean"
}
```

### CreatePeriodDto
```json
{
  "companyId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

### JournalEntryDto

```json
{
  "id": "string (UUID)",
  "date": "string (datetime)",
  "description": "string",
  "periodId": "string (UUID)",
  "companyId": "string (UUID)",
  "currencyCode": "string",
  "exchangeRate": "number (double, nullable)",
  "exchangeRateDate": "string (date, nullable)",
  "isPosted": "boolean",
  "isReversed": "boolean",
  "lines": [
    {
      "id": "string (UUID)",
      "journalEntryId": "string (UUID)",
      "accountId": "string (UUID)",
      "debit": "number (double)",
      "credit": "number (double)",
      "lineNumber": "integer"
    }
  ]
}
```

### AccountDto
```json
{
  "id": "string (UUID)",
  "code": "string",
  "name": "string",
  "accountTypeId": "string (UUID)",
  "parentAccountId": "string (UUID, nullable)",
  "isActive": "boolean",
  "level": "integer",
  "isMovable": "boolean"
}
```

### CurrencyExchangeRateDto

```json
{
  "id": "string (UUID)",
  "currencyCode": "string",
  "date": "string (date)",
  "buyRate": "number (double)",
  "sellRate": "number (double)"
}
```

### DocumentReferenceDto
```json
{
  "id": "string (UUID)",
  "journalEntryId": "string (UUID)",
  "sourceType": "string",
  "sourceId": "string (UUID)",
  "referenceNumber": "string",
  "description": "string"
}
```

### AccountTypeDto
```json
{
  "id": "string (UUID)",
  "name": "string",
  "description": "string"
}
```

### AuditLogDto
```json
{
  "id": "string (UUID)",
  "entity": "string",
  "action": "string",
  "performedBy": "string (UUID)",
  "performedAt": "string (datetime)",
  "details": "string"
}
```

### BalanceSheetDto
```json
{
  "code": "string",
  "name": "string",
  "accountType": "string",
  "balance": 0
}
```

### IncomeStatementDto
```json
{
  "code": "string",
  "name": "string",
  "accountType": "string",
  "balance": 0
}
```

### GeneralLedgerDto
```json
{
  "date": "2025-08-21T03:54:09.061Z",
  "description": "string",
  "debit": 0,
  "credit": 0,
  "movement": 0
}
```

### TrialBalanceDto
```json
{
  "code": "string",
  "name": "string",
  "totalDebit": 0,
  "totalCredit": 0,
  "balance": 0
}
```

## Usage Examples

### Full Multi-Company Flow

```bash
# 1. Create a company
curl -X POST "https://api.accounting.com/v1/companies" \
     -H "Authorization: Bearer YOUR_API_KEY" \
     -H "Content-Type: application/json" \
     -d '{
       "company": {
         "name": "Mi Empresa S.A.",
         "taxId": "20123456789",
         "country": "PE",
         "currencyCode": "PEN",
         "isActive": true
       }
     }'

# 2. Create accounting period
curl -X POST "https://api.accounting.com/v1/periods" \
     -H "Authorization: Bearer YOUR_API_KEY" \
     -H "Content-Type: application/json" \
     -d '{
       "period": {
         "companyId": "company-id-from-step-1"
       }
     }'

# 3. Create exchange rate
curl -X POST "https://api.accounting.com/v1/currencies" \
     -H "Authorization: Bearer YOUR_API_KEY" \
     -H "Content-Type: application/json" \
     -d '{
       "currencyExchangeRate": {
         "currencyCode": "USD",
         "date": "2024-01-15",
         "buyRate": 3.74,
         "sellRate": 3.76
       }
     }'

# 4. Create cash account
curl -X POST "https://api.accounting.com/v1/accounts" \
     -H "Authorization: Bearer YOUR_API_KEY" \
     -H "Content-Type: application/json" \
     -d '{
       "account": {
         "code": "1100",
         "name": "Cash",
         "accountTypeId": "asset-type-id",
         "isActive": true,
         "isMovable": true
       }
     }'

# 5. Create sales account
curl -X POST "https://api.accounting.com/v1/accounts" \
     -H "Authorization: Bearer YOUR_API_KEY" \
     -H "Content-Type: application/json" \
     -d '{
       "account": {
         "code": "1200",
         "name": "Sales",
         "accountTypeId": "asset-type-id",
         "isActive": true,
         "isMovable": true
       }
     }'

# 6. Create multi-currency accounting entry
curl -X POST "https://api.accounting.com/v1/journal-entries" \
     -H "Authorization: Bearer YOUR_API_KEY" \
     -H "Content-Type: application/json" \
     -d '{
       "journalEntry": {
         "date": "2024-01-15T10:30:00Z",
         "description": "Venta en dólares",
         "periodId": "period-id",
         "companyId": "company-id",
         "currencyCode": "USD",
         "exchangeRate": 3.75,
         "exchangeRateDate": "2024-01-15",
         "isPosted": true,
         "lines": [
           {
             "accountId": "cash-account-id",
             "debit": 1000.00,
             "credit": 0.00,
             "lineNumber": 1
           },
           {
             "accountId": "sales-account-id",
             "debit": 0.00,
             "credit": 1000.00,
             "lineNumber": 2
           }
         ]
       }
     }'

# 7. Associate supporting document
curl -X POST "https://api.accounting.com/v1/journal-entries/journal-entry-id/documents" \
     -H "Authorization: Bearer YOUR_API_KEY" \
     -H "Content-Type: application/json" \
     -d '{
       "documentReference": {
         "journalEntryId": "journal-entry-id",
         "sourceType": "Invoice",
         "sourceId": "invoice-id",
         "referenceNumber": "INV-2024-001",
         "description": "Factura de venta"
       }
     }'
```     
	 
### Example of Entry Reversal

```bash
Reverse an accounting entry

curl -X PUT "https://api.accounting.com/v1/journal-entries/{journal-entry-id}/reverse"
-H "Authorization: Bearer YOUR_API_KEY"

This will automatically create a reversing entry with the debits and credits reversed.
```

## SDKs and Libraries

### Official SDKs
- **JavaScript/Node.js**: `npm install @accounting-api/client`
- **Python**: `pip install accounting-api-client`
- **C#/.NET**: `dotnet add package AccountingApi.Client`

### Example with JavaScript SDK

```javascript
import { AccountingApiClient } from '@accounting-api/client';

const client = new AccountingApiClient({
  apiKey: 'YOUR_API_KEY',
  baseUrl: 'https://api.accounting.com/v1'
});

// Create a company
const company = await client.companies.create({
  name: 'ABC S.A.',
  taxId: '20123456789',
  country: 'PE',
  currencyCode: 'PEN',
  isActive: true
});

// Create multi-currency accounting entry
const journalEntry = await client.journalEntries.create({
  date: '2024-01-15T10:30:00Z',
  description: 'Sale in dollars',
  periodId: 'period-id',
  companyId: company.id,
  currencyCode: 'USD',
  exchangeRate: 3.75,
  lines: [
    { accountId: 'account-1', debit: 1000, credit: 0, lineNumber: 1 },
    { accountId: 'account-2', debit: 0, credit: 1000, lineNumber: 2 }
  ]
});

// Reverse seat
await client.journalEntries.reverse(journalEntry.id);
```

### Error Validation Example

```bash
# Attempt to create unbalanced settlement (must ≠ have)
curl -X POST "https://api.accounting.com/v1/journal-entries" \
     -H "Authorization: Bearer YOUR_API_KEY" \
     -H "Content-Type: application/json" \
     -d '{
       "journalEntry": {
         "date": "2024-01-15T10:30:00Z",
         "description": "Asiento desbalanceado",
         "periodId": "period-id",
         "lines": [
           {
             "accountId": "account-1",
             "debit": 1000.00,
             "credit": 0.00,
             "lineNumber": 1
           },
           {
             "accountId": "account-2",
             "debit": 0.00,
             "credit": 500.00,
             "lineNumber": 2
           }
         ]
       }
     }'

# Error response:
# {
#   "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
#   "title": "Bad Request",
#   "status": 400,
#   "detail": "El total de débitos (1000.00) no coincide con el total de créditos (500.00)",
#   "instance": "/journal-entries"
# }
```

## Changelog

### Version 1.0 (Current)
- ✅ Multi-company management with complete tax information
- ✅ Multi-currency support with automatic exchange rates
- ✅ Accounting periods with opening/closing control by company
- ✅ Accounting journal entries with automatic validation and currency support
- ✅ Journal entry reversals for accounting corrections
- ✅ Hierarchical chart of accounts with account types
- ✅ Document references for complete traceability
- ✅ Audit system with detailed history
- ✅ Pagination on all listing endpoints
- ✅ Robust validations for accounting integrity
- ✅ **Financial Reports** (Balance Sheet, Income Statement)
- 
### Upcoming Features
- 🔄 **Automatic Bank Reconciliation**
- 🔄 **Budgets** and Budget Control
- 🔄 **Cost Centers** for Detailed Analysis
- 🔄 **Mass Import** of Accounting Entries
- 🔄 **Webhook Notifications** for Important Events
- 🔄 **Reporting API** with Customizable Formats
---

## Support

- **Documentation**: [https://docs.accounting-api.com](https://docs.accounting-api.com)
- **Support Email**: support@accounting-api.com
- **Status Page**: [https://status.accounting-api.com](https://status.accounting-api.com)
- **Community Forum**: [https://community.accounting-api.com](https://community.accounting-api.com)
- **GitHub**: [https://github.com/accounting-api](https://github.com/accounting-api)

### Support Hours
- **Technical Support**: Monday to Friday, 9:00 AM - 6:00 PM (UTC-5)
- **Emergency Support**: 24/7 for Enterprise customers

---

*Last updated: January 15, 2024*
*Document version: 1.0*
