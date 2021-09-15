using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITestApp.PostcodesIOService
{
    public class CallManager
    {
        //RestSharp object which handles comms with the API
        private readonly IRestClient _client;
        //Capture status description
        public int StatusCode { get; set; }

        public CallManager()
        {
            _client = new RestClient(AppConfigReader.BaseUrl);
        }
        ///<summary>
        ///define and makes the APU request and stores the response
        ///</summary>
        ///<param name="postcode"></param>
        public async Task<string> MakePostcodeRequestAsync(string postcode)
        {
            //setup the request
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            //define the request resource path
            request.Resource = $"postcodes/{postcode.ToLower().Replace(" ", "")}";
            //make request
            IRestResponse response = await _client.ExecuteAsync(request);
            StatusCode = (int)response.StatusCode;
            return response.Content;
        }


    }
}
