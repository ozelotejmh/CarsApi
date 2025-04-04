using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarsApi.Model;
using CarsApi.Services;

namespace CarsApi.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/pokemons")]
    public class ControllerCarsv2 : ControllerBase
    {
        private readonly IControllerContext _context;
        public ControllerCarsv2(IControllerContext context)
        {
            _context = context;
        }

        // Obtener todos los coches
        [HttpGet]
        public ActionResult<IEnumerable<CarsCharacter>> GetAll()
        {
            var cars = _context.GetCharacters();
            if (cars == null || !cars.Any())
                return NotFound("No hay coches disponibles.");
            cars.ForEach(c =>
            {
                c.Nombre = c.Nombre?.ToUpper();
                c.Color = c.Color?.ToUpper();
                c.Imagen = c.Imagen?.ToUpper();
                c.Descripcion = c.Descripcion?.ToUpper();
            });
            return Ok(cars);
        }
    }
}
