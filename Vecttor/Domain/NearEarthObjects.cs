using Newtonsoft.Json;

namespace Vecttor.Domain
{
    public class NearEarthObjects
    {
        public List<NearEarthObject> CurrentDay { get; set; } = new List<NearEarthObject>();
        public List<NearEarthObject> NextDay { get; set; } = new List<NearEarthObject>();
        public List<NearEarthObject> TreeDay { get; set; } = new List<NearEarthObject>();
        public List<NearEarthObject> ForDay { get; set; } = new List<NearEarthObject>();
        public List<NearEarthObject> FiveDay { get; set; } = new List<NearEarthObject>();
        public List<NearEarthObject> SixDay { get; set; } = new List<NearEarthObject>();
        public List<NearEarthObject> LastDay { get; set; } = new List<NearEarthObject>();
    }
}
