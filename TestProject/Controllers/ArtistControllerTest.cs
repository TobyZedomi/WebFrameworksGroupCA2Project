using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.CodeAnalysis.Editing;
using WebFrameworksGroupCA2Project;
using System.Net.Http;
using WebFrameworksGroupCA2Project.Models;
using Humanizer.Localisation;
using Newtonsoft.Json;
using System.Text;


namespace TestProject.Controllers
{
    public class ArtistControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {

        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient httpClient;

        public ArtistControllerTest()
        {

            var factory = new WebApplicationFactory<Program>();
            _factory = factory;
            httpClient = _factory.CreateClient();

        }

        [Fact]
        public async void TestArtistLoads()
        {

            //Arrange
            var client = _factory.CreateClient();

            //Act https://localhost:7148/Artists
            var response = await client.GetAsync("/Artists/Index");
            //Assert
            int code = (int)response.StatusCode;
            Assert.Equal(200, code);
        }





        [Theory]
        [InlineData("Kendrick Lamar")]
        [InlineData("Prince")]
        [InlineData("David Bowie")]
        [InlineData("Jimi Hendrix")]
        [InlineData("Adele")]

        public async Task TestForArtist(string artistName)
        {

            // Arrange



            //Act

            var response1 = await httpClient.GetAsync("https://localhost:7148/Account/Login?");

            var response = await httpClient.GetAsync("/Artists/Index");
            var pageContent = await response.Content.ReadAsStringAsync();
            var contentString = pageContent.ToString();


            //Assert

            Assert.Contains(artistName, contentString);


        }


        // test for genre only

        [Theory]
        [InlineData("David Bowie")]
        [InlineData("Jimi Hendrix")]


        public async Task TestForArtistWithRockGenre(string artistName)
        {

            // Arrange

            //Act

            var response = await httpClient.GetAsync("/Artists?ArtistGenre=Rock");
            var pageContent = await response.Content.ReadAsStringAsync();
            var contentString = pageContent.ToString();


            //Assert

            Assert.Contains(artistName, contentString);


        }


        // test for artist name and genre search 

        [Theory]
        [InlineData("Jimi Hendrix")]


        public async Task TestForArtistWithParticularNameAndGenre(string artistName)
        {

            // Arrange

            //Act

            var response = await httpClient.GetAsync("/Artists?ArtistGenre=Rock&SearchString=Jimi+Hendrix");
            var pageContent = await response.Content.ReadAsStringAsync();
            var contentString = pageContent.ToString();


            //Assert

            Assert.Contains(artistName, contentString);

        }




        // test for artist details

        [Theory]
        [InlineData("Jimi Hendrix")]


        public async Task TestForArtistDetails(string artistName)
        {

            // Arrange

            //Act

            var response = await httpClient.GetAsync("/Artists/Details/1");
            var pageContent = await response.Content.ReadAsStringAsync();
            var contentString = pageContent.ToString();


            //Assert

            Assert.Contains(artistName, contentString);

        }

    }
}