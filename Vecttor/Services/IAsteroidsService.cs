using Vecttor.Domain;
using Vecttor.Models;

namespace Vecttor.Services
{
    public interface IAsteroidsService
    {
        /// <summary>
        /// Get the Asteroids closer to the earth
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        Task<IEnumerable<AsteroidApiResponse>> GetAsteroids(int days);
    }
}
