using CarRentalBackend.Data;
using CarRentalBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalBackend.Tests
{
    public abstract class TestBase
    {
        protected DataContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new DataContext(options)
            {
                Cars = null!,
                Users = null!,
                Reservations = null!,
                Reviews = null!,
                ReservationLogs = null!
            };

            context.Cars = context.Set<Car>();
            context.Users = context.Set<User>();
            context.Reservations = context.Set<Reservation>();
            context.Reviews = context.Set<Review>();
            context.ReservationLogs = context.Set<ReservationLog>();
            context.Database.EnsureCreated();
            return context;
        }
    }
}

