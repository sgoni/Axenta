# 📄 Axenta API Documentation

## Table of Contents

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
  - [Accounts](#accounts)
  - [Account Types](#account-types)
  - [Document References](#document-references)
  - [Audit Logs](#audit-logs)
  - [Currency Exchange Rate](#currency-exchange-rates)
  - [Companies](#companies)  
- [Data Models](#data-models)
- [Examples](#examples)
- [SDKs and Libraries](#sdks-and-libraries)
- [Changelog](#changelog)

## Overview

The Accounting API provides a comprehensive solution for managing accounting operations including periods, journal entries, accounts, and account types. This RESTful API follows standard HTTP conventions and returns JSON responses.

### Key Features

- ✅ Accounting period management
- ✅ Journal entry creation and validation
- ✅ Account hierarchy management
- ✅ Account type classification
- ✅ Pagination support
- ✅ Comprehensive error handling

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
   1. Chart of Accounts, Account Types, Hierarchies
2. General Accounting
   1. Accounting Entries, Journals, General Ledger
3. Accounts Payable
   1. Vendors, Invoices, Payments
4. Accounts Receivable
   1. Customers, Invoices, Collections
5. Bank Reconciliations
6. Fixed Assets
7. Taxes and Withholdings
8. Financial Reports
   1. Balance Sheet, Income Statement
9. User and Role Management
   1. Authentication and Authorization
10. Event Audit
    1. Logs, Critical Data Changes

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
localhost: https://localhost:5050
docker: https://localhost:6060
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
| `date` | UUID | Yes | ID of the period to open
| `description` | UUID | Yes | ID of the company
| `periodId` | UUID | Yes | ID of the company
| `companyId` | UUID | Yes | ID of the company
| `currencyCode` | UUID | Yes | ID of the company
| `exchangeRate` | UUID | Yes | ID of the company
| `exchangeRateDate` | UUID | Yes | ID of the company
| `accountId` | UUID | Yes | ID of the company
| `debit` | UUID | Yes | ID of the company
| `credit` | UUID | Yes | ID of the company
| `lineNumber` | UUID | Yes | ID of the company

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

#### List Accounting Entries

```http
GET /journal-entries
```

**Parameters:**

| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `PageIndex` | integer | 0 | Número de página
| `PageSize` | integer | 10 | Elementos por página

#### Update Accounting Entry

```http
PUT /journal-entries
```

**Note:** Entries can only be modified if the period is open.

#### Delete Accounting Entry

```http
DELETE /journal-entries/{id}
```

**Parameters:**

| Parameter | Type | Required | Description
|-----|-----|-----|-----
| `id` | UUID | Yes | ID of the seat to be deleted

#### Get Seat by ID

```http
GET /journal-entries/{journalEntryId}
```

### Accounts

Chart of accounts management with support for hierarchical structure.

#### Create Account

```http
POST /accounts
```

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

#### Get Account Tree

```http
GET /accounts/tree
```

Returns accounts in a hierarchical structure.

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

#### Update Account

```http
PUT /accounts
```

#### Get Account by ID

```http
GET /accounts/{accountId}
```

#### Activate Account

```http
PUT /accounts/{id}/activate
```

#### Deactivate Account

```http
PUT /accounts/{id}/desactivate
```

#### Delete Account

```http
DELETE /accounts/{id}
```

### Account Types

Manage account types (Assets, Liabilities, Equity, etc.).

#### List Account Types

```http
GET /accounts/types
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

### Document References

Management of references to external documents associated with accounting entries.

#### Create Document Reference

```http
POST /journal-entries/{id}/documents
```

**Body of the Request::**
```json
{
   "documentReference": {
      "journalEntryId": "123e4567-e89b-12d3-a456-426614174004",
      "sourceType": "Invoice",
      "sourceId": "123e4567-e89b-12d3-a456-426614174011",
      "referenceNumber": "INV-2024-001",
      "description": "Factura de compra de suministros"
   }
}
```

#### Get References by Seat

```http
GET /journal-entries/{id}/documents
```

### Audit Logs

View the history of changes and operations performed in the system.

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

#### Get Audit Log by ID

```http
GET /audit-logs/{id}
```

### Currency Exchange Rates

### Companies

## Data Models

### PeriodDto
```json
{
   "id": "string (UUID)",
   "year": "integer",
   "month": "integer",
   "startDate": "string (datetime)",
   "endDate": "string (datetime)",
   "isClosed": "boolean"
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

### JournalEntryDto

```json
{
   "id": "string (UUID)",
   "date": "string (datetime)",
   "description": "string",
   "isReversed": "boolean",
   "periodId": "string (UUID)",
   "lines": [
      {
         "id": "string (UUID)",
         "journalEntryId": "string (UUID)",
         "accountId": "string (UUID)",
         "debit": "number (decimal)",
         "credit": "number (decimal)",
         "lineNumber": "integer"
      }
   ]
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

## Usage Examples

### Complete Workflow

Here's a complete example of creating a period, accounts, and journal entries:

```bash
# 1. Create an accounting period
curl -X POST "[https://api.accounting.com/v1/periods](https://api.accounting.com/v1/periods)
   -H "Authorization: Bearer YOUR_API_KEY" -H "Content-Type: application/json" -d '{
      "year": 2024,
      "month": 1,
      "startDate": "2024-01-01T00:00:00Z",
      "endDate": "2024-01-31T23:59:59Z"
   }'
```

```bash
# 2. Create cash account
curl -X POST "[https://api.accounting.com/v1/accounts](https://api.accounting.com/v1/accounts)
   -H "Authorization: Bearer YOUR_API_KEY" -H "Content-Type: application/json" -d '{
   "account": {
      "code": "1100",
      "name": "Efectivo",
      "accountTypeId": "asset-type-id",
      "isActive": true,
      "isMovable": true
   }}'
```

```bash
# 3. Create capital account

curl -X POST "[https://api.accounting.com/v1/accounts](https://api.accounting.com/v1/accounts)
   -H "Authorization: Bearer YOUR_API_KEY" -H "Content-Type: application/json" -d '{
   "account": {
      "code": "3100",
      "name": "Capital Social",
      "accountTypeId": "equity-type-id",
      "isActive": true,
      "isMovable": true
   }}'
```

```bash
# 4. Create opening entry

curl -X POST "[https://api.accounting.com/v1/journal-entries](https://api.accounting.com/v1/journal-entries)
   -H "Authorization: Bearer YOUR_API_KEY" -H "Content-Type: application/json" -d '{
   "journalEntry": {
   "date": "2024-01-01T00:00:00Z",
   "description": "Aporte inicial de capital",
   "periodId": "period-id",
   "lines": [
         {
            "accountId": "cash-account-id",
            "debit": 10000.00,
            "credit": 0.00,
            "lineNumber": 1
         },
         {
            "accountId": "capital-account-id",
            "debit": 0.00,
            "credit": 10000.00,
            "lineNumber": 2
         }
       ]
      }
   }'
```

### Error Validation Example

```bash
# Intento de crear asiento desbalanceado (debe ≠ haber)
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

## SDKs and Libraries

### Official SDKs

- **JavaScript/Node.js**: `npm install @accounting-api/client`
- **Python**: `pip install accounting-api-client`
- **C#/.NET**: `dotnet add package AccountingApi.Client`


### Community SDKs

- **PHP**: Available on Packagist
- **Ruby**: Available as a gem
- **Go**: Available on GitHub


### Example with JavaScript SDK

```javascript
import { AccountingApiClient } from '@accounting-api/client';

const client = new AccountingApiClient({
apiKey: 'YOUR_API_KEY',
baseUrl: '[https://api.accounting.com/v1](https://api.accounting.com/v1)

// Create period
const period = await client.periods.create({
      year: 2024,
      month: 1,
      startDate: '2024-01-01T00:00:00Z',
      endDate: '2024-01-31T23:59:59Z'
   });

// Create accounting entry
const journalEntry = await client.journalEntries.create({
      date: '2024-01-15T10:30:00Z',
      description: 'Pago de renta',
      periodId: period.periodId,
      lines: [
         { accountId: 'account-1', debit: 1500, credit: 0, lineNumber: 1 },
         { accountId: 'account-2', debit: 0, credit: 1500, lineNumber: 2 }
      ]
   });
```

## Changelog

### Version 1.0 (Current)
- ✅ Initial Release
- ✅ Full accounting period management
- ✅ Account journal entries with automatic validation
- ✅ Account hierarchy support
- ✅ Account type management
- ✅ Full audit trail
- ✅ Pagination support
- ✅ Document references

### Upcoming Features
- 🔄 Financial report generation
- 🔄 Batch transactions
- 🔄 Webhook notifications
- 🔄 Advanced filtering options
- 🔄 Accounts Payable
- 🔄 Accounts Receivable
- 🔄 Bank reconciliation API'

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