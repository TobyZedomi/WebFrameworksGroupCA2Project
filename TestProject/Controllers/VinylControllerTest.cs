using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Controllers
{
    public class VinylControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {


        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient httpClient;

        public VinylControllerTest()
        {

            var factory = new WebApplicationFactory<Program>();
            _factory = factory;
            httpClient = _factory.CreateClient();

        }



        [Fact]
        public async void TestVinylStoreLoads()
        {

            //Arrange
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/Vinyls/Index");
            //Assert
            int code = (int)response.StatusCode;
            Assert.Equal(200, code);
        }





        [Theory]
        [InlineData("Electric Ladyland")]
        [InlineData("30")]
        [InlineData("Purple Rain")]
        [InlineData("ZiggyStardust")]

        public async Task TestForVinylStore(string vinylName)
        {

            // Arrange



            //Act

            var response1 = await httpClient.GetAsync("https://localhost:7148/Account/Login?");

            var response = await httpClient.GetAsync("/Vinyls/Index");
            var pageContent = await response.Content.ReadAsStringAsync();
            var contentString = pageContent.ToString();


            //Assert

            Assert.Contains(vinylName, contentString);


        }


        // test for genre only

        [Theory]
        [InlineData("Electric Ladyland")]
        [InlineData("30")]
        [InlineData("Ziggy Stardust")]


        public async Task TestForVinyltWithPrice(string vinylName)
        {

            // Arrange

            //Act

            var response = await httpClient.GetAsync("/Vinyls?SearchString=&minPrice=5&maxPrice=30");
            var pageContent = await response.Content.ReadAsStringAsync();
            var contentString = pageContent.ToString();


            //Assert

            Assert.Contains(vinylName, contentString);


        }



        // test for vinyl details

        [Theory]
        [InlineData("30")]


        public async Task TestForVinylDetails(string artistName)
        {

            // Arrange

            //Act

            var response = await httpClient.GetAsync("/Vinyls/Details/3");
            var pageContent = await response.Content.ReadAsStringAsync();
            var contentString = pageContent.ToString();


            //Assert

            Assert.Contains(artistName, contentString);

        }

    }
}
