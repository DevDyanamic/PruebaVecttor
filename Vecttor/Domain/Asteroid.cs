namespace Vecttor.Domain
{
    public class Asteroid
    {
        public Links Links { get; set; }
        public string Id { get; set; }
        public string NeoReferenceId { get; set; }
        public string Name { get; set; }
        public string NasaJplUrl { get; set; }
        public double AbsoluteMagnitudeH { get; set; }
        public EstimatedDiameter EstimatedDiameter { get; set; }
        public bool IsPotentiallyHazardousAsteroid { get; set; }
        public List<CloseApproachData> CloseApproachData { get; set; }
        public bool IsSentryObject { get; set; }
    }
}
