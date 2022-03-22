using NUnit.Framework;
using RestSharp;
using System.Net;
using System.Threading.Tasks;
using System;
using System.Text.Json;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Assessment
{
    [TestFixture]
    public class DogAPITests
    {


        [Test]
        public async Task GeTAllDogBreedsCheckRetriever()
        {
            //Arrange
            var sutRestClient = new RestClient("https://dog.ceo/");
            var sutRestRequest = new RestRequest("api/breeds/list/all", Method.Get);


            //Act

            //get all breeds
            var sutApiResponse = await sutRestClient.ExecuteGetAsync(sutRestRequest);            
            var sutResults = JsonConvert.DeserializeObject<ApiAllBreedResponse<Dictionary<string, List<string>>>>(sutApiResponse.Content);

            string retrieverKey = "retriever";

            //get retriever subbreed method 1
            var sutSubBreeds = sutResults.Message[retrieverKey];

            //get retriever subbreed method 2
            var sutSubBreedsRequest = new RestRequest($"api/breed/{retrieverKey}/list", Method.Get);
            var sutSubBreedsResponse = await sutRestClient.ExecuteGetAsync(sutSubBreedsRequest);
            var sutSubBreedResults = JsonConvert.DeserializeObject<ApiAllBreedResponse<List<string>>>(sutSubBreedsResponse.Content);

            //get random image
            var sutSubBreedRandomImageRequest = new RestRequest($"api/breed/{retrieverKey}/golden/images/random", Method.Get);
            var sutSubBreedRandomImageResponse = await sutRestClient.ExecuteGetAsync(sutSubBreedRandomImageRequest);
            var sutSubBreedRandomImageResults = JsonConvert.DeserializeObject<ApiAllBreedResponse<string>>(sutSubBreedRandomImageResponse.Content);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, sutApiResponse.StatusCode);
            Assert.True(sutResults.Message.ContainsKey(retrieverKey));
            Assert.True(sutSubBreedResults.Message.Any());

        }

    }
}
public class ApiAllBreedResponse<T>
{
    [JsonProperty("message")]
    public T Message { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }
}


/*Task:
Your task is to automate the two test cases below. You are free to use any Open Source automation frameworks, but please do list the tools and resources used. Below is a list of patterns and practices that we are looking for in your solution:
 Hybrid approach with modularization
 Descriptive programming
 Regular expressions
 Parameterization
 At least two ways of storing and utilizing test data
 Report stores test evidence and results

Task 1 - API:
Public API - https://dog.ceo/dog-api/
Using the above mentioned API perform the following calls.
• Perform an API request to produce a list of all dog breeds. (Diagram 1)
• Using code, verify “retriever” breed is within the list. (Diagram 2)
• Perform an API request to produce a list of sub-breeds for “retriever”. (Diagram 3)
• Perform an API request to produce a random image / link for the sub-breed “golden” (Diagram 4)
*/

