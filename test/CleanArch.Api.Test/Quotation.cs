using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;
using Xunit;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace CleanArch.Api.Test
{
    public class Quotation
    {
        private readonly TestServer server;
        private readonly HttpClient client;
       
        public Quotation()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    IHostingEnvironment env = builderContext.HostingEnvironment;
                   // config.AddJsonFile("autofac.json")
                    //.AddEnvironmentVariables();
                })
                .ConfigureServices(services => services.AddAutofac());

            server = new TestServer(webHostBuilder);
            client = server.CreateClient();
        }
        [Fact]
        public async Task GetSavedQuoteList()
        {
            var request = new
            {
                Email="pannepu@agility.com",
                SearchString="",
                Status="ACTIVE",
                PageNo="1"
            };

            string requestData = JsonConvert.SerializeObject(request);
            StringContent content = new StringContent(requestData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Quotation/GetSavedQuoteList", content);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.NotNull(responseString);
        }
    }
}
