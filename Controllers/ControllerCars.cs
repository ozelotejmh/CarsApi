using CarsApi.Model;
using CarsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarsApi.Controllers
{
    [ApiController]
    [Route("api/cars")]
    public class ControllerCars : ControllerBase
    {
        private readonly IControllerContext _context;

        public ControllerCars(IControllerContext context)
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

            return Ok(cars);
        }

        // Obtener un coche por ID
        [HttpGet("{id}")]
        public ActionResult<CarsCharacter> GetById(int id)
        {
            var cars = _context.GetCharacters();
            var car = cars?.FirstOrDefault(c => c.CarsID == id);

            return car != null ? Ok(car) : NotFound($"Coche con ID {id} no encontrado.");
        }

        // Insertar un nuevo coche
        [HttpPost]
        public ActionResult<CarsCharacter> Create(CarsCharacter newCar)
        {
            bool result = _context.PostCharacter(newCar.Nombre, newCar.Color, newCar.Imagen, newCar.Descripcion);
            if (!result) return BadRequest("No se pudo insertar el coche.");

            return CreatedAtAction(nameof(GetAll), new { nombre = newCar.Nombre }, newCar);
        }

        // Actualizar un coche existente
        [HttpPut("{id}")]
        public ActionResult Update(int id, CarsCharacter updatedCar)
        {
            bool result = _context.PutCharacter(id, updatedCar.Nombre, updatedCar.Color, updatedCar.Imagen, updatedCar.Descripcion);
            if (!result) return NotFound($"No se encontró el coche con ID {id} para actualizar.");

            return NoContent();
        }

        // Eliminar un coche por su ID
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            bool result = _context.DeleteCharacter(id);
            if (!result) return NotFound($"No se encontró el coche con ID {id} para eliminar.");

            return NoContent();
        }
    }
}