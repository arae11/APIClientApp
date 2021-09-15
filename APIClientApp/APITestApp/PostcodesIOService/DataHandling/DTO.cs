using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace APITestApp.PostcodesIOService
{
    //Constraints specify that the DTO can only be of IResponse type, AND specifies the new() specifies 
    //that a type argument in a generic class declaration MUST have a public parameterless constructor
    public class DTO<ResponseType> where ResponseType : IResponse, new()
    {
        //A property which represents the model
        public ResponseType Response { get; set; }

        //Method that creates the above objects using the response from the API
        public void DeserializeResponse(string postcodeResponse)
        {
            Response = JsonConvert.DeserializeObject<ResponseType>(postcodeResponse);
        }

    }
}
