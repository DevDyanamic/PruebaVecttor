using ApiVecttot.DTOs;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace ApiVecttot.Endpoints
{
    public class AsteroidsEndpoints : EndpointBaseAsync
                            .WithRequest<int?>
                            .WithActionResult<ResponseDTO<AsteroidsDTO>>
    {
        private readonly ILogger<AsteroidsEndpoints> _logger;
        public AsteroidsEndpoints(ILogger<AsteroidsEndpoints> logger
)
        {
            _logger = logger;
        }

        [HttpGet("/vecttor/v1/asteroids")]
        public async override Task<ActionResult<ResponseDTO<AsteroidsDTO>>> HandleAsync(int? page, CancellationToken cancellationToken = default)
        {
            var response = new AsteroidsDTO
            {
                nombre = "",
                diametro = "",
                velocidad = "",
                fecha = "",
                planeta = "",
            };
            try
            {
                return ResponseDTO<AsteroidsDTO>.Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseDTO<AsteroidsDTO>.Failed("Se ha producido un error inesperado");
            }
        }

    }
}
