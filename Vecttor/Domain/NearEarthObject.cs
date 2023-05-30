using Vecttor.Models;

namespace Vecttor.Domain
{
    public class NearEarthObject
    {
        public Links Links { get; set; }
        public string Id { get; set; }
        public string neo_reference_id { get; set; }
        public string name { get; set; }
        public string nasa_jpl_url { get; set; }
        public double absolute_magnitude_h { get; set; }
        public EstimatedDiameter estimated_diameter { get; set; } = new EstimatedDiameter();
        public bool is_potentially_hazardous_asteroid { get; set; }
        public List<CloseApproachData> close_approach_data { get; set; }
        public bool IsSentryObject { get; set; }

    }
}
