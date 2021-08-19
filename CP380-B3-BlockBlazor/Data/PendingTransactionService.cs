using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using CP380_B1_BlockList.Models;

namespace CP380_B3_BlockBlazor.Data
{
    public class PendingTransactionService
    {
        // TODO: Add variables for the dependency-injected resources
        //       - httpClient
        //       - configuration
        //
        static HttpClient _httpCl;
        private readonly IConfiguration _conf;

        private readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        //
        // TODO: Add a constructor with IConfiguration and IHttpClientFactory arguments
        //
        public PendingTransactionService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpCl = httpClientFactory.CreateClient();
            _conf = configuration.GetSection("PayloadService");
        }
        //
        // TODO: Add an async method that returns an IEnumerable<Payload> (list of Payloads)
        //       from the web service
        //
        public async Task<IEnumerable<Payload>> GetPayloads()
        {
            var responce = await _httpCl.GetAsync(_conf["url"]);

            if  (responce.IsSuccessStatusCode)
            {
                JsonSerializerOptions options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
                return await JsonSerializer.DeserializeAsync<IEnumerable<Payload>>(
                        await responce.Content.ReadAsStreamAsync(), options
                    );
            }
            return Array.Empty<Payload>();
        }
        //
        // TODO: Add an async method that returns an HttpResponseMessage
        //       and accepts a Payload object.
        //       This method should POST the Payload to the web API server
        //
        public async Task<HttpResponseMessage> SubmitPayload(Payload payload)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(payload, JsonSerializerOptions),
                System.Text.Encoding.UTF8,
                "application/json"
                );

            var res = await _httpCl.PostAsync(_conf["url"], content);
            return res;
        }
    }
}
