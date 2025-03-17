using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Controllers
{
    public class SongControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {


        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient httpClient;

        public SongControllerTest()
        {

            var factory = new WebApplicationFactory<Program>();
            _factory = factory;
            httpClient = _factory.CreateClient();

        }


        [Fact]
        public async void TestSongPageLoads()
        {

            //Arrange
            var client = _factory.CreateClient();

            //Act https://localhost:7148/Artists
            var response = await client.GetAsync("/Songs/Index");
            //Assert
            int code = (int)response.StatusCode;
            Assert.Equal(200, code);
        }


        // test to see if songName exist 



        [Theory]
        [InlineData("Purple Rain")]
        [InlineData("Starman")]
        [InlineData("Easy On Me")]


        public async Task TestForSongData(string songName)
        {



            //Act

            var response = await httpClient.GetAsync("/Songs/Index");
            var pageContent = await response.Content.ReadAsStringAsync();
            var contentString = pageContent.ToString();


            //Assert

            Assert.Contains(songName, contentString);


        }

        /// test for search by song name 

        [Theory]
        [InlineData("Purple Rain")]
        public async Task TestForSongSearch(string songName)
        {



            //Act

            var response = await httpClient.GetAsync("/Songs?SongName=PurpleRain");
            var pageContent = await response.Content.ReadAsStringAsync();
            var contentString = pageContent.ToString();


            //Assert

            Assert.Contains(songName, contentString);


        }


        // test for details

        [Theory]
        [InlineData("Purple Rain")]


        public async Task TestForArtistDetails(string songName)
        {

            // Arrange

            //Act

            var response = await httpClient.GetAsync("/Songs/Details/4");
            var pageContent = await response.Content.ReadAsStringAsync();
            var contentString = pageContent.ToString();


            //Assert

            Assert.Contains(songName, contentString);

        }


    }
}