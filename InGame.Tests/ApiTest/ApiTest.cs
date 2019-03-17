﻿using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using InGame.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace InGame.Tests.ApiTest
{
    public class ApiTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ApiTest()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(@"../../.."))
                .AddJsonFile("appsettings.json", optional: false)
                //.AddUserSecrets<Startup>()
                .Build();

            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(configuration));
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task UnAuthorizedAccess()
        {
            var response = await _client.GetAsync("/api/Product/GetProductList");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetToken()
        {
            var bodyString = @"{Email: ""zzeki@gmail.com"", Password: ""Pass@word1""}";
            var response = await _client.PostAsync("/api/Token",
                new StringContent(bodyString, Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var token = await response.Content.ReadAsStringAsync();
            //var responseJson = JObject.Parse(responseString);
            Assert.NotNull(token);
        }
         

        [Fact]
        public async Task GetProductList()
        {
            var bodyString = @"{Email: ""zzeki@gmail.com"", password: ""Pass@word1""}";
            var response = await _client.PostAsync("/api/token",
                new StringContent(bodyString, Encoding.UTF8, "application/json"));
            var token = await response.Content.ReadAsStringAsync();
            //var token = JObject.Parse(responseString);
            //var token = (string)responseJson["token"];

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/Product/GetProductList");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var productListResponse = await _client.SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, productListResponse.StatusCode);
        }


        [Fact]
        public async Task GetProductById()
        {
            var bodyString = @"{Email: ""zzeki@gmail.com"", password: ""Pass@word1""}";
            var response = await _client.PostAsync("/api/token",
                new StringContent(bodyString, Encoding.UTF8, "application/json"));
            var token = await response.Content.ReadAsStringAsync();
            //var token = JObject.Parse(responseString);
            //var token = (string)responseJson["token"];

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/Product/Get/1");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var productListResponse = await _client.SendAsync(requestMessage);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, productListResponse.StatusCode);
        }
    }
}

