using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace APITestApp.PostcodesIOService
{
    public class SinglePostcodeService
    {
        #region Properties
        public CallManager CallManager { get; set; }
        public JObject Json_Response { get; set; }
        //the response data transfer object
        public DTO<SinglePostcodeResponse> SinglePostcodeDTO { get; set; }
        //the postcode used in the API request
        public string PostcodeSelected { get; set; }
        public string PostcodeResponse { get; set; }
        #endregion

        // Constructor - creates the restclient object
        public SinglePostcodeService()
        {
            CallManager = new CallManager();
            SinglePostcodeDTO = new DTO<SinglePostcodeResponse>();
        }

        public async Task MakeRequestAsync(string postcode)
        {
            PostcodeSelected = postcode;
            // Make request
            PostcodeResponse = await CallManager.MakePostcodeRequestAsync(postcode);

            // Parse JSON in response content
            Json_Response = JObject.Parse(PostcodeResponse);

            // Use DTO to convert JSON string into an object tree
            SinglePostcodeDTO.DeserializeResponse(PostcodeResponse);
        }
    }
}
