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
    public class BlockService
    {
        // TODO: Add variables for the dependency-injected resources
        //       - httpClient
        //       - configuration
        //
        static HttpClient _httpCl;
        private readonly IConfiguration _conf;

        //
        // TODO: Add a constructor with IConfiguration and IHttpClientFactory arguments
        //
        public BlockService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpCl = httpClientFactory.CreateClient();
            _conf = configuration.GetSection("BlockService");
        }
        //
        // TODO: Add an async method that returns an IEnumerable<Block> (list of Blocks)
        //       from the web service
        //
        public async Task<IEnumerable<Block>> GetBlock()
        {

            if ((await _httpCl.GetAsync(_conf["url"])).IsSuccessStatusCode)
            {
                JsonSerializerOptions options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
                return await JsonSerializer.DeserializeAsync<IEnumerable<Block>>(
                        await (await _httpCl.GetAsync(_conf["url"])).Content.ReadAsStreamAsync(), options
                    );
            }

            return Array.Empty<Block>();
        }
        public async Task<Block> SubmitNewBlockAsync(Block block)
        {
            var content = new StringContent(
               JsonSerializer.Serialize(block, JsonSerializerOptions),
               System.Text.Encoding.UTF8,
               "application/json"
               );

            var response = await _httpCl.PostAsync(_conf["url"], content);
            if (res.IsSuccessStatusCode)
                return block;
            else
                return null;
        }
    }
}

   
