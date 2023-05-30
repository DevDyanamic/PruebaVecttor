using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Numerics;
using Vecttor.Domain;
using Vecttor.Models;
using Vecttor.Utils;

namespace Vecttor.ClientHttp
{
    public class VecttorClient
    {
        string NASA_API_KEY = "zdUP8ElJv1cehFM0rsZVSQN7uBVxlDnu4diHlLSb";
        private readonly string _url = "https://api.nasa.gov/neo/rest/v1/feed?start_date=";

        private readonly IHttpClientWrapper _httpClient;
        public VecttorClient(IHttpClientWrapper httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<AsteroidApiResponse>> GetAsteroids(int days)
        {
            DateTime startDate = DateTime.Now.Date;
            DateTime endDate = startDate.AddDays(days);

            string nasaApiUrl = $"{_url}{startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}&api_key={NASA_API_KEY}";
            try
            {
                using (HttpResponseMessage response = await _httpClient.GetAsync(nasaApiUrl))
                {
                    List<AsteroidApiResponse> hazardousAsteroids = new List<AsteroidApiResponse>();
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();

                        if (json != null)
                        {
                            JObject jsonObject = JsonConvert.DeserializeObject<JObject>(json)!;

                            if (jsonObject != null)
                            {
                                CleanJSON(days, startDate, jsonObject);

                                var asteroids = JsonConvert.DeserializeObject<NasaApiResponse>(jsonObject.ToString());

                                hazardousAsteroids = GetTopThreeHazardousAsteroids(asteroids!);

                            }
                        }
                    }
                    return hazardousAsteroids;
                }


            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }

        private void CleanJSON(int days, DateTime startDate, JObject jsonObject)
        {
            string[] ArrObjectDays = new[] { "CurrentDay", "NextDay", "TreeDay", "ForDay", "FiveDay", "SixDay", "LastDay" };

            for (int i = 0; i < days; i++)
            {
                ChangeKey(jsonObject, startDate.AddDays(i).ToString("yyyy-MM-dd"), ArrObjectDays[i]);
            }
        }

        private List<AsteroidApiResponse> GetTopThreeHazardousAsteroids(NasaApiResponse asteroidResponse)
        {
            List<NearEarthObject> NearEarth = asteroidResponse.near_earth_objects.CurrentDay
                .Union(asteroidResponse.near_earth_objects.NextDay)
                .Union(asteroidResponse.near_earth_objects.TreeDay)
                .Union(asteroidResponse.near_earth_objects.ForDay)
                .Union(asteroidResponse.near_earth_objects.FiveDay)
                .Union(asteroidResponse.near_earth_objects.SixDay)
                .Union(asteroidResponse.near_earth_objects.LastDay).ToList();


            List<AsteroidApiResponse> listAsteroid = (from earth in NearEarth
                                                      where earth.is_potentially_hazardous_asteroid
                                                      select new AsteroidApiResponse
                                                      {
                                                          nombre = earth.name,
                                                          diametro = 2 * (earth.estimated_diameter.kilometers.estimated_diameter_max + earth.estimated_diameter.kilometers.estimated_diameter_min) + 4,//2N
                                                          velocidad = earth.close_approach_data.Count > 0 ? earth.close_approach_data.First().relative_velocity.kilometers_per_second : "",
                                                          fecha = earth.close_approach_data.Count > 0 ? earth.close_approach_data.First().close_approach_date : "",
                                                          planeta = earth.close_approach_data.Count > 0 ? earth.close_approach_data.First().orbiting_body : "",

                                                      }).OrderBy(o => o.diametro).Take(3).ToList();

            return listAsteroid;
        }
        private void ChangeKey(JObject jsonObject, string oldKey, string newKey)
        {
            foreach (JProperty property in jsonObject.Properties().ToList())
            {
                if (property.Name.StartsWith(oldKey))
                {
                    jsonObject[newKey] = property.Value;
                    property.Remove();
                }
                else if (property.Value is JObject nestedObject)
                {
                    ChangeKey(nestedObject, oldKey, newKey);
                }
                else if (property.Value is JArray array)
                {
                    foreach (var item in array)
                    {
                        if (item is JObject nestedObjectInArray)
                        {
                            ChangeKey(nestedObjectInArray, oldKey, newKey);
                        }
                    }
                }
            }
        }
    }
}

