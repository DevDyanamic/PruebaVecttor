using Newtonsoft.Json;
using System.Text.Json;
using Vecttor.Domain;

namespace Vecttor.Models
{
    public class AteroidApiNasaResponse
    {
        [JsonProperty("links")]
        public Links Links { get; set; } = new Links();
        [JsonProperty("element_count")]
        public int ElementCount { get; set; }
        [JsonProperty("near_earth_objects")]
        public NearEarthObjects NearEarthObjects { get; set; } = new NearEarthObjects();
    }
}
