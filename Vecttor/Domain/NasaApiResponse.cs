namespace Vecttor.Domain
{
    public class NasaApiResponse
    {
        public Links links { get; set; } = new Links();
        public int element_count { get; set; }
        public NearEarthObjects near_earth_objects { get; set; } = new NearEarthObjects();
    }
}
