# Geographical Service

This service is a read-only service providing geographical data.

## Geographical Areas

### Zip Code (US)

Zip codes can span multiple states 

Zip codes can span multiple counties 

Zip codes can span multiple cities

Zip Code is the identifier for a record

#### Operations

Get by Zip Code

Get by County

Get by State

Get by County and State

### City (US)

Cities are limited to 1 state

Cities can span multiple counties 

Cities can have multiple zip codes

City ID is the identifier for a record

#### Operations

Get by ID

Get by Name

Get by State

Get by Name and State

Get by County

Get by Zip Code

### County (US)

Counties are limited to 1 state (by construction of FIPS)

County FIPS (full) is a 5-digit value comprised of: State FIPS (2) + Local County FIPS (3)

County FIPS (full) is the identifier for a record

Counties contain multiple cities

Counties contain multiple zip codes

#### Operations

Get by full FIPS

Get by Name

Get by Name and State

Get by Zip Code

### State (US)

States are limited to one Country

State ISO-3166 (2 digits) is the identifier for a record

#### Operations

Get All

Get by Abbreviation

## Responses 

Responses contain hierarchical information from the entity up to the higher level of geography.

For example, a Zip Code will contain Counties, States, and Country information while a State will only include Country information.