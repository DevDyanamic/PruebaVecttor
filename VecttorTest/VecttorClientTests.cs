using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Vecttor.ClientHttp;
using Vecttor.Domain;
using Vecttor.Models;
using Vecttor.Utils;

namespace Vecttor.Tests
{
    [TestClass]
    public class VecttorClientTests
    {
        private const string NasaApiKey = "zdUP8ElJv1cehFM0rsZVSQN7uBVxlDnu4diHlLSb";

        [TestMethod]
        public async Task GetAsteroids_ReturnsHazardousAsteroids()
        {
            // Arrange
            var httpClientMock = new Mock<IHttpClientWrapper>();
            var expectedStartDate = DateTime.Now.Date;
            var expectedEndDate = expectedStartDate.AddDays(3);

            var expectedUrl = $"https://api.nasa.gov/neo/rest/v1/feed?start_date={expectedStartDate:yyyy-MM-dd}&end_date={expectedEndDate:yyyy-MM-dd}&api_key={NasaApiKey}";

            var responseJson = @"{
                'near_earth_objects': {
                    'CurrentDay': [
                        {
                            'name': 'Asteroid 1',
                            'is_potentially_hazardous_asteroid': true,
                            'estimated_diameter': {
                                'kilometers': {
                                    'estimated_diameter_max': 0.7
                                }
                            },
                            'close_approach_data': [
                                {
                                    'relative_velocity': {
                                        'kilometers_per_second': '10'
                                    },
                                    'close_approach_date': '2023-05-31',
                                    'orbiting_body': 'Earth'
                                }
                            ]
                        }
                    ],
                    'NextDay': [],
                    'LastDay': []
                }
            }";

            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseJson)
            };

            httpClientMock.Setup(client => client.GetAsync(expectedUrl))
                .ReturnsAsync(httpResponseMessage);

            var vecttorClient = new VecttorClient(httpClientMock.Object);

            // Act
            var result = await vecttorClient.GetAsteroids(3);

            // Assert
            Assert.AreEqual(1, result.Count());

            var asteroid = result.First();
            Assert.AreEqual("Asteroid 1", asteroid.nombre);           
            Assert.AreEqual("5.40", asteroid.diametro.ToString("N2").Replace(",", ".")); 

            Assert.AreEqual("10", asteroid.velocidad);
            Assert.AreEqual("2023-05-31", asteroid.fecha);
            Assert.AreEqual("Earth", asteroid.planeta);
        }

        [TestMethod]
        public async Task GetAsteroids_ThrowsException_WhenHttpClientThrowsException()
        {
            // Arrange
            var httpClientMock = new Mock<IHttpClientWrapper>();
            var expectedStartDate = DateTime.Now.Date;
            var expectedEndDate = expectedStartDate.AddDays(3);

            var expectedUrl = $"https://api.nasa.gov/neo/rest/v1/feed?start_date={expectedStartDate:yyyy-MM-dd}&end_date={expectedEndDate:yyyy-MM-dd}&api_key={NasaApiKey}";

            var exceptionMessage = "An error occurred";
            httpClientMock.Setup(client => client.GetAsync(expectedUrl))
                .ThrowsAsync(new Exception(exceptionMessage));

            var vecttorClient = new VecttorClient(httpClientMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await vecttorClient.GetAsteroids(3));
        }
    }
}
