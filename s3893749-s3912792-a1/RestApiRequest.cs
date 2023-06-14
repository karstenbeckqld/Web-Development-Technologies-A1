using Newtonsoft.Json;
using s3893749_s3912792_a1.controller;
using s3893749_s3912792_a1.model;

namespace s3893749_s3912792_a1;

/* The RestApiRequest class contacts the web service to get the customers present in the provided JSON file. It collects
 * all present data, including Accounts, Transactions and Login data and stores it in a List of type WebServiceObjectList
 * which gets returned by the class. */
public class RestApiRequest
{
    /* Here we define the url to the web service as a constant. */
    private const string url = "https://coreteaching01.csit.rmit.edu.au/~e103884/wdt/services/customers/";

    public RestApiRequest()
    {
        WebServiceObjectList = RestCall();
    }
    
    /* To provide other classes access to the WebServiceObjectList, we define a property that is accessible from outside the
     * class.
     */
    public List<WebServiceObject> WebServiceObjectList { get; private set; }

    /* The RestCall() method performs the HTTP GET call to the web service to obtain the JSON object and deserialize it,
     * so that the content can get assigned to the respective classes. It returns a List of WebServiceObjects.
     * Adapted from Week 2 lecture. */
    private static List<WebServiceObject> RestCall()
    {
        /* We first establish a new HttpClient. Using the 'using' keyword omits the requirement to dispose of the client. */
        using var client = new HttpClient();
        
        /* Now we obtain the JSON string by calling the GetStringAsync() method on the client and providing the url of
         * the web service.
         */
        var json = client.GetStringAsync(url).Result;

        /* Now we can convert the JSON string to a list of WebServiceObjects using the DeserializeObject() method and
         * providing it the JSON string.
         */
        var jsonData = JsonConvert.DeserializeObject<List<WebServiceObject>>(json, new JsonSerializerSettings
        {
         DateFormatString = "dd/MM/yyyy"
        });

        /* Finally, the method returns the list of WebServiceObjects. */
        return jsonData;
    }
}