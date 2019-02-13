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
using CleanArch.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Test
{
    public class UserManagement
    {
        private readonly TestServer server;
        private readonly HttpClient client;

        public UserManagement()
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
        public async Task SignUp()
        {
            SignupModel request = new SignupModel()
            {
                Email = "pannepu@agility.com",
                FirstName="pavan",
                LastName="Kumar"
                
            };

            string requestData = JsonConvert.SerializeObject(request);
            StringContent content = new StringContent(requestData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/UserManagement/PostSignUp", content);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.NotNull(responseString);
        }
    }
}
