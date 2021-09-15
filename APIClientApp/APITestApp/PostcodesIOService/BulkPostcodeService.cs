//using System;
//using System.Threading.Tasks;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using RestSharp;

//namespace APITestApp.PostcodesIOService
//{
//    public class BulkPostcodeService
//    {
//        #region Properties
//        // RestSharp Object which handles comms with the API
//        public RestClient Client;
//        // A newtonsoft object representing the json response
//        public JObject ResponseContent { get; set; }
//        // the postcode used in this API request
//        public string[] PostcodeSelected { get; set; }
//        // store the status code
//        public int StatusCode { get; set; }
//        // an object model of the response
//        public BulkPostcodeResponse ResponseObject { get; set; }
//        #endregion

//        // Constructor - creates the restclient object
//        public BulkPostcodeService()
//        {
//            Client = new RestClient { BaseUrl = new Uri(AppConfigReader.BaseUrl) };
//        }

//        public async Task MakeRequestAsync(string[] postcodes)
//        {
//            // Setup the request
//            var request = new RestRequest(Method.POST);
//            request.AddHeader("Content-Type", "application/json");
//            PostcodeSelected = postcodes;
//            JObject postcodeArray = new JObject
//            {
//                new JProperty("postcodes", new JArray(postcodes))
//            };

//            // Define the request resource path
//            request.AddParameter("application/json", postcodeArray.ToString(), ParameterType.RequestBody);

//            // Make the request
//            IRestResponse bulkPostcodeResponse = await Client.ExecuteAsync(request);

//            // Parse JSON in response content
//            ResponseContent = JObject.Parse(bulkPostcodeResponse.Content);

//            // Capture status code
//            StatusCode = (int)bulkPostcodeResponse.StatusCode;

//            // Parse the JSON string into an object tree
//            ResponseObject = JsonConvert.DeserializeObject<BulkPostcodeResponse>(bulkPostcodeResponse.Content);
//        }
//    }
//}
