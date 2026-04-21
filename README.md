# Address Microservice

# About
The Address Microservice, implemented as a RESTful API, is a wrapper around the IPO’s current Melissa address look-up service. This is a service to query address details using a country and simple search term.

# Installation guide
### System Requirements
- IDE capable of running .NET 10 or above i.e. Visual Studio

### Prerequisites
- API key (MelissaId) for Melissa RESTful API
- A passthrough service to handle requests and responses to and from the Melissa RESTful API. This is currently implemented as a separate API, but could be implemented directly in the gateways project within this solution.

### Installation instructions
1. Clone the repository to your local machine.

2. Open the 'IPO.Address.sln' solution file in Visual Studio.

3. Update the MelissaId value in the 'appsettings.json' file with your personal key.

4. Build the solution.

5. Set the Web API (IPO.Address.API) as the Startup project in Visual Studio and run in debug configuration.

7. A command window will launch, in which you will see the Console output.

8. The swagger page will launch in your default browser ready to test the endpoints.

# Usage Instructions
   *baseUrl* - The base URL is dependant on your setup.

   **Note:** All endpoints with Full Request and Response contracts can be viewed in the Swagger documentation.

### Service Endpoints

1. GET Request - address search

   This endpoint takes a countryCode and searchTerm and formats a request to the Melissa address lookup RESTful API. The standard response from the Melissa address lookup service is then formatted and returned.
   Internally this endpoint calls Melissa endpoint, example uri: 

   ```TEXT
     http://expressentry.melissadata.net/web/GlobalExpressFreeForm?id={{MelissaId}}&country=GB&ff=np10%208qq&nativecharset=false&maxrecords=200&format=json&suitecompression=false
   ```

   To run the above Melissa Endpoint directly in a web browser the MelissaId needs to be provided.

   Example URL:

   ```TEXT
     {{baseUrl}}/{{countryCode}}/addresses/{{postCode or searchTerm}}
   ```

   Example GET Address: 

   ```TEXT
     {{baseUrl}}/GB/addresses/NP10%208QQ
   ```

   Example GET address results
   ```JSON
    [
      {
        "address": "The Intellectual Property Office, Concept House, Cardiff Road, NEWPORT, NP10 8QQ",
        "address1": "Concept House",
        "address2": "Cardiff Road",
        "address3": "Newport",
        "address4": "NP10 8QQ",
        "address5": "",
        "address6": "",
        "address7": "",
        "address8": "",
        "isO3166_2": "GB",
        "superAdministrativeArea": "Wales",
        "administrativeArea": "Newport",
        "subAdministrativeArea": "",
        "locality": "Newport",
        "dependentLocality": "",
        "postalCode": "NP10 8QQ",
        "organization": "The Intellectual Property Office",
        "mak": "8247267536"
      }
    ]
    ```

### System Endpoints
All of these endpoint paths assume that {baseurl} corresponds to a fully qualified url pointing to the location of the microservice.

1. GET request - version

    Use this end point to return the version of the microservice api.
    
    Example URL:
   ```TEXT
     {{baseUrl}}/version
   ```

2. GET request - health <span style="color:red">(Deprecated)</span>

    Use this end point to return the health status of the microservice api.
    
    Example URL:
   ```TEXT
     {{baseUrl}}/health
   ```

3. GET request - health, live

    Use this end point to return the live status of the microservice api.
    
    Example URL:
   ```TEXT
     {{baseUrl}}/health/live
   ```

4. GET request - health, ready

    Use this end point to return the ready status of the microservice api.
    
    Example URL:
   ```TEXT
     {{baseUrl}}/health/ready
   ```

5. GET request - error list

    Use this end point to return a JSON array containing a list of all the possible error codes and descriptions that the microservice could encounter and return.
    
    Example URL:

   ```TEXT
     {{baseUrl}}/error/list
   ```

    Example Get Address Error List Results
   ```JSON
    [
        {
            "code": "E000",
            "description": "Internal Error"
        },
        {
            "code": "E001",
            "description": "Validation Error"
        }
    ]
    ```

# Licence
Unless stated otherwise, the codebase is released under [License](./License.md). This covers both the codebase and any sample code in the documentation.

The documentation is © Crown copyright and available under the terms of the Open Government 3.0 licence.