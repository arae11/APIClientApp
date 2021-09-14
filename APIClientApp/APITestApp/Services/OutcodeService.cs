using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace APITestApp.Services
{
    public class OutcodeService
    {
        #region Properties
        // RestSharp Object which handles comms with the API
        public RestClient Client;
        // A newtonsoft object representing the json response
        public JObject ResponseContent { get; set; }
        // the postcode used in this API request
        public string OutcodeSelected { get; set; }
        // store the status code
        public int StatusCode { get; set; }
        // an object model of the response
        public OutcodeResponse ResponseObject { get; set; }
        #endregion

        // Constructor - creates the restclient object
        public OutcodeService()
        {
            Client = new RestClient { BaseUrl = new Uri(AppConfigReader.BaseUrl) };
        }

        public async Task MakeRequestAsync(string postcode)
        {
            // Setup the request
            var outcodeRequest = new RestRequest(Method.GET);
            outcodeRequest.AddHeader("Content-Type", "application/json");
            var outcode = "SL5";

            // Define the request resource path
            outcodeRequest.Resource = $"outcodes/{outcode.ToLower()}";

            // Make the request
            var outcodeResponse = await Client.ExecuteAsync(outcodeRequest);

            // Parse JSON in response content
            var outcodeJsonResponse = JObject.Parse(outcodeResponse.Content);

            // Capture status code
            StatusCode = (int)outcodeResponse.StatusCode;

            // Parse the JSON string into an object tree
            ResponseObject = JsonConvert.DeserializeObject<OutcodeResponse>(outcodeResponse.Content);
        }
    }
}
