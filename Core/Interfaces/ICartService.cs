using Core.Dtos;
using Data.Entities;

namespace Core.Interfaces
{
    public interface ICartService
    {
        int GetCount();
        void AddItem(int id);
        void RemoveItem(int id);
        List<CarDto> GetCars();
        List<Car> GetCarsEntity();
        /*CarDto GetCar();
        Car GetCarEntity();*/
        void Clear();
    }
}
