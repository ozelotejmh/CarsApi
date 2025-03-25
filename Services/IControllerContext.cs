using CarsApi.Model;

namespace CarsApi.Services
{
    public interface IControllerContext
    {
        public List<CarsCharacter> GetCharacters();
        public bool PostCharacter(string nombre, string color, string imagen, string descripcion);
        public bool PutCharacter(int carsId, string nombre, string color, string imagen, string descripcion);
        public bool DeleteCharacter(int carsId); 

    }
}
