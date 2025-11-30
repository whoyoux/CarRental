using CarRentalBackend.Data;
using CarRentalBackend.Models;
using CarRentalBackend.ModelsDto;
using CarRentalBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace CarRentalBackend.Tests
{
    public class CarServiceTests : TestBase
    {

        [Fact]
        public async Task CreateCarAsync()
        {
            using var context = GetInMemoryContext();
            var carService = new CarService(context);
            var carDto = new CarDto
            {
                Brand = "Toyota",
                Model = "Corolla",
                Year = 2023,
                PricePerDay = 100.50m,
                Description = "Reliable sedan",
                ImageUrl = "https://example.com/car.jpg"
            };

            var result = await carService.CreateCarAsync(carDto);

            Assert.NotNull(result);
            Assert.True(result.Id > 0);
            Assert.Equal("Toyota", result.Brand);
            Assert.Equal("Corolla", result.Model);
            Assert.Equal(2023, result.Year);
            Assert.Equal(100.50m, result.PricePerDay);
            
            var carInDb = await context.Cars.FindAsync(result.Id);
            Assert.NotNull(carInDb);
            Assert.Equal("Toyota", carInDb.Brand);
        }

        [Fact]
        public async Task UpdateCarAsync_WithValidData()
        {
            using var context = GetInMemoryContext();
            var car = new Car
            {
                Brand = "Ford",
                Model = "Focus",
                Year = 2022,
                PricePerDay = 90.00m,
                Description = "Old description",
                ImageUrl = "https://example.com/old.jpg"
            };
            context.Cars.Add(car);
            await context.SaveChangesAsync();

            var carService = new CarService(context);
            var updatedDto = new CarDto
            {
                Brand = "Ford",
                Model = "Focus",
                Year = 2023,
                PricePerDay = 95.00m,
                Description = "New description",
                ImageUrl = "https://example.com/new.jpg"
            };

            var result = await carService.UpdateCarAsync(car.Id, updatedDto);

            Assert.NotNull(result);
            Assert.Equal(car.Id, result.Id);
            Assert.Equal(2023, result.Year);
            Assert.Equal(95.00m, result.PricePerDay);
            Assert.Equal("New description", result.Description);
            
            var carInDb = await context.Cars.FindAsync(car.Id);
            Assert.NotNull(carInDb);
            Assert.Equal(2023, carInDb.Year);
            Assert.Equal("New description", carInDb.Description);
        }

        [Fact]
        public async Task DeleteCarAsync_WithExistingCar()
        {
            using var context = GetInMemoryContext();
            var car = new Car
            {
                Brand = "BMW",
                Model = "320i",
                Year = 2023,
                PricePerDay = 150.00m
            };
            context.Cars.Add(car);
            await context.SaveChangesAsync();

            var carService = new CarService(context);

            var result = await carService.DeleteCarAsync(car.Id);

            Assert.True(result);
            var carInDb = await context.Cars.FindAsync(car.Id);
            Assert.Null(carInDb);
        }
    }
}
