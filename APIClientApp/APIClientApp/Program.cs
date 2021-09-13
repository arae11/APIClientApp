using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace APIClientApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ///// SET UP REQUEST/////
            // Client Property which is equal to a new 'RestSharp'.
            // We are going to create a URI objects which encapsulates
            var restClient = new RestClient(@"https://api.postcodes.io/");

            // Set up the request
            var restRequest = new RestRequest(Method.GET); // default parameter is method.get(dont need to put in parameters if this is the case)
            // Set method as GET
            restRequest.Method = Method.GET; // optional
            // Added Header info
            restRequest.AddHeader("Content-Type", "application/json");
            // Set timeout
            restRequest.Timeout = -1;
            var postcode = "EC2Y 5AS";
            // Define request resource path
            restRequest.Resource = $"postcodes/{postcode.ToLower().Replace(" ", "")}";


            ///// EXECUTE REQUEST /////
            var singlePostcodeResponse = restClient.Execute(restRequest);

            //Console.WriteLine("Response Content as string");
            //Console.WriteLine(singlePostcodeResponse.Content);

            ///// SETUP BULKPOSTCODE REQUEST/////
            var client = new RestClient("https://api.postcodes.io/postcodes");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            JObject postcodes = new JObject
            {
                new JProperty("postcodes", new JArray(new string[]{"OX49 5NU", "M32 0JG", "NE30 1DP" }))
            };
            //request.AddParameter("application/json", postcodes.ToString(), ParameterType.RequestBody);
            request.AddJsonBody(postcodes.ToString());
            IRestResponse bulkPostcodeResponse = await client.ExecuteAsync(request);
            //Console.WriteLine(bulkPostcodeResponse.Content);
            //Console.WriteLine($"Status Code: {bulkPostcodeResponse.StatusCode}");
            //Console.WriteLine($"Status Code: {(int)bulkPostcodeResponse.StatusCode}");

            //var course = new JObject
            //{
            //   new JProperty("name", "eng91"),
            //   new JProperty("trainees", new JArray(new string[]{"Ringo", "Paul", "George", "John" })),
            //   new JProperty("total", 4)
            //};

            ///// QUERY OUR RESPONSE AS A JOBJECT /////
            var bulkJsonResponse = JObject.Parse(bulkPostcodeResponse.Content);
            var singleJsonResponse = JObject.Parse(singlePostcodeResponse.Content);
            //Console.WriteLine(singleJsonResponse["status"]);
            //Console.WriteLine(singleJsonResponse["result"]["codes"]["parish"]);
            //Console.WriteLine(bulkJsonResponse["result"][0]["query"]);
            //Console.WriteLine(bulkJsonResponse["result"][1]["result"]["country"]);

            ///// QUERY OUR RESPONSE AS A C# OBJECT /////
            var singlePostCode = JsonConvert.DeserializeObject<SinglePostcodeResponse>(singlePostcodeResponse.Content);
            var bulkPostCode = JsonConvert.DeserializeObject<BulkPostcodeResponse>(bulkPostcodeResponse.Content);

            //Console.WriteLine(singlePostCode.result.country);
            //Console.WriteLine(singlePostCode.result.codes.admin_county);

            //foreach (var result in bulkPostCode.result)
            //{
            //    Console.WriteLine(result.Query);
            //    Console.WriteLine(result.postcode.region);
            //}
            //var result2 = bulkPostCode.result.Where(p => p.Query == "OX49 5NU").Select(p => p.postcode.parish).FirstOrDefault();

            ///// OUTCODE /////
            // SETUP
            // Set up the request
            var outcodeRequest = new RestRequest(Method.GET);
            // Added Header info
            outcodeRequest.AddHeader("Content-Type", "application/json");
            // Set timeout
            outcodeRequest.Timeout = -1;
            var outcode = "SL5";

            // Define request resource path
            outcodeRequest.Resource = $"outcodes/{outcode.ToLower()}";

            // EXECUTE REQUEST
            var outcodeResponse = restClient.Execute(outcodeRequest);

            ///// QUERY OUR RESPONSE AS A JOBJECT /////
            var outcodeJsonResponse = JObject.Parse(outcodeResponse.Content);
            //Console.WriteLine(outcodeJsonResponse["result"]["admin_county"]);

            ///// QUERY OUR RESPONSE AS A C# OBJECT /////
            var OutcodeCResponse = JsonConvert.DeserializeObject<OutcodeResponse>(outcodeResponse.Content);
            //Console.WriteLine(OutcodeCResponse.result.admin_county);
            foreach (var item in OutcodeCResponse.result.admin_county)
            {
                Console.WriteLine(item);
            }
        }
    }
}
