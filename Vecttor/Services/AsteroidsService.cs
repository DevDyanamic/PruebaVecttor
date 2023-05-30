using Vecttor.ClientHttp;
using Vecttor.Domain;
using Vecttor.Models;

namespace Vecttor.Services
{
    public class AsteroidsService : IAsteroidsService
    {
        private readonly VecttorClient _client;
        public AsteroidsService( VecttorClient vecttorClient)
        {
            _client = vecttorClient;
        }
        public Task<IEnumerable<AsteroidApiResponse>> GetAsteroids(int days)
        {
            var asteroids = _client.GetAsteroids(days);
            return asteroids;
        }
    }
}
