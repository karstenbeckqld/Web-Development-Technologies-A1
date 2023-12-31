﻿using MyBankDbAccess.Models;
using Newtonsoft.Json;

namespace MyBankDbAccess.Utils;

// The RestApiRequest class contacts the web service to get the customers present in the provided JSON file. It collects
// all present data, including Accounts, Transactions and Login data and stores it in a List of type CustomerObjectList
// which gets returned by the class.
public static class RestApiRequest
{
    // Here we define the url to the web service as a constant.
    private const string Url = "https://coreteaching01.csit.rmit.edu.au/~e103884/wdt/services/customers/";

    // The RestCall() method performs the HTTP GET call to the web service to obtain the JSON object and deserialize it,
    // so that the content can get assigned to the respective classes. It returns a List of Customers.
    // Adapted from Week 2 lecture. 
    public static Task<List<Customer>> RestCallAsync()
    {
        // We first establish a new HttpClient. Using the 'using' keyword omits the requirement to dispose of the client.
        using var client =  new HttpClient();

        // Now we obtain the JSON string by calling the GetStringAsync() method on the client and providing the url of
        // the web service.
        var json = client.GetStringAsync(Url).Result;

        // Now we can convert the JSON string to a list of WebServiceObjects using the DeserializeObject() method and
        // providing it the JSON string.
        var jsonData =  JsonConvert.DeserializeObject<List<Customer>>(json, new JsonSerializerSettings
        {
            // The date format string <day>/<month>/<year> <hour>:<minute>:<second> <AM or PM> of the input date and time.
            DateFormatString = "dd/MM/yyyy hh:mm:ss tt"
        });

        // Finally, the method returns the list of WebServiceObjects.
        return Task.FromResult(jsonData);
    }
}