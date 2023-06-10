using Newtonsoft.Json;
using s3893749_s3912792_a1.model;

namespace s3893749_s3912792_a1;

public class RestApiRequest
{
    private const string url = "https://coreteaching01.csit.rmit.edu.au/~e103884/wdt/services/customers/";

    public List<AccountHolder> RestCallAccountHolder()
    {
        using var client = new HttpClient();
        var json = client.GetStringAsync(url).Result;

        Console.WriteLine(json);

        // Convert JSON string to AccountHolder objects.
        var accountHolders = JsonConvert.DeserializeObject<List<AccountHolder>>(json);

        return accountHolders;
    }
}