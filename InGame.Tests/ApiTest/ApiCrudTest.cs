using System;
using InGame.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using InGame.Core.Entities;
using InGame.Core.Interfaces;
using InGame.Core.Services;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace InGame.Tests.ApiTest
{
    public class ApiCrudTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        private Mock<IAsyncRepository<Product>> _mockProductRepo;

    public ApiCrudTest()
        {
            _mockProductRepo = new Mock<IAsyncRepository<Product>>();

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
        public async Task InsertProductWithApi()
        {
            var bodyString = @"{Email: ""zzeki@gmail.com"", Password: ""Pass@word1""}";
            var response = _client.PostAsync("/api/Token",
                new StringContent(bodyString, Encoding.UTF8, "application/json")).Result;

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var token = await response.Content.ReadAsStringAsync();
            Assert.NotNull(token);

            // Arrange
            var request = new Product
            {
                Name = "silah",
                Price = 3000,
                Description = "ucuz item"
            };

            var productService = new ProductService(_mockProductRepo.Object, null, null);

            await productService.AddAsync(request);
            _mockProductRepo.Verify(x => x.AddAsync(request), Times.Once);
        }

        [Fact]
        public async Task UpdateProductWithApi()
        {
            var bodyString = @"{Email: ""zzeki@gmail.com"", Password: ""Pass@word1""}";
            var response = _client.PostAsync("/api/Token",
                new StringContent(bodyString, Encoding.UTF8, "application/json")).Result;

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var token = await response.Content.ReadAsStringAsync();
            Assert.NotNull(token);

            // Arrange
            var request = new Product
            {
                Name = "silah",
                Price = 5000,
                Description = "ucuz item"
            };

            var productService = new ProductService(_mockProductRepo.Object, null, null);

            await productService.UpdateAsync(request);
            _mockProductRepo.Verify(x => x.UpdateAsync(request), Times.Once);
        }


        [Fact]
        public async Task DeleteProductWithApi()
        {
            var bodyString = @"{Email: ""zzeki@gmail.com"", Password: ""Pass@word1""}";
            var response = _client.PostAsync("/api/Token",
                new StringContent(bodyString, Encoding.UTF8, "application/json")).Result;

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var token = await response.Content.ReadAsStringAsync();
            Assert.NotNull(token);

            var product = new Product();
            _mockProductRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(product);

            var productService = new ProductService(_mockProductRepo.Object, null, null);

            await productService.DeleteAsync(product);
            _mockProductRepo.Verify(x => x.DeleteAsync(product), Times.Once);
        }
    }
}
