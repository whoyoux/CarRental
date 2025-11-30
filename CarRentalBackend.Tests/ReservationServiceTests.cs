using CarRentalBackend.Data;
using CarRentalBackend.Models;
using CarRentalBackend.ModelsDto;
using CarRentalBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace CarRentalBackend.Tests
{
    public class ReservationServiceTests : TestBase
    {

        [Fact]
        public async Task CreateReservationAsync_WithValidData_ShouldCreateReservation()
        {
            using var context = GetInMemoryContext();
            var car = new Car
            {
                Brand = "Toyota",
                Model = "Corolla",
                Year = 2023,
                PricePerDay = 100.00m
            };
            context.Cars.Add(car);
            await context.SaveChangesAsync();

            var reservationService = new ReservationService(context);
            var userId = Guid.NewGuid();
            var createDto = new CreateReservationDto
            {
                CarId = car.Id,
                StartDateTime = DateTime.UtcNow.AddDays(1),
                EndDateTime = DateTime.UtcNow.AddDays(3)
            };

            var result = await reservationService.CreateReservationAsync(userId, createDto);

            Assert.NotNull(result);
            Assert.Equal(car.Id, result.CarId);
            Assert.Equal("Toyota", result.CarBrand);
            Assert.Equal("Corolla", result.CarModel);
            Assert.Equal(200.00m, Decimal.Round(result.TotalPrice, 2));
            
            var reservationInDb = await context.Reservations.FindAsync(result.Id);
            Assert.NotNull(reservationInDb);
            Assert.Equal(userId, reservationInDb.UserId);
        }

        [Fact]
        public async Task CancelReservationAsync_WithValidReservation_ShouldReturnTrue()
        {
            using var context = GetInMemoryContext();
            var userId = Guid.NewGuid();
            var car = new Car
            {
                Brand = "Toyota",
                Model = "Corolla",
                Year = 2023,
                PricePerDay = 100.00m
            };
            context.Cars.Add(car);
            
            var reservation = new Reservation
            {
                CarId = car.Id,
                UserId = userId,
                StartDateTime = DateTime.UtcNow.AddDays(1),
                EndDateTime = DateTime.UtcNow.AddDays(3),
                TotalPrice = 200.00m,
                CreatedAt = DateTime.UtcNow
            };
            context.Reservations.Add(reservation);
            await context.SaveChangesAsync();

            var reservationService = new ReservationService(context);

            var result = await reservationService.CancelReservationAsync(reservation.Id, userId);

            Assert.True(result);
            var reservationInDb = await context.Reservations.FindAsync(reservation.Id);
            Assert.Null(reservationInDb);
        }
    }
}
