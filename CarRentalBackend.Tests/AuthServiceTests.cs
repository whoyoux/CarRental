using CarRentalBackend.Data;
using CarRentalBackend.ModelsDto;
using CarRentalBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace CarRentalBackend.Tests
{
    public class AuthServiceTests : TestBase
    {

        [Fact]
        public async Task RegisterAsync_WithValidData_ShouldCreateUser()
        {
            using var context = GetInMemoryContext();
            var authService = new AuthService(context);
            var registerDto = new RegisterUserDto
            {
                Email = "test@example.com",
                Password = "password123",
                ConfirmPassword = "password123"
            };

            var result = await authService.RegisterAsync(registerDto);

            Assert.NotNull(result);
            Assert.Equal("test@example.com", result.Email);
            Assert.NotEmpty(result.PasswordHash);
            var userInDb = await context.Users.FirstOrDefaultAsync(u => u.Email == "test@example.com");
            Assert.NotNull(userInDb);
        }

        [Fact]
        public async Task LoginAsync_WithValidCredentials_ShouldReturnLoginResponse()
        {
            Environment.SetEnvironmentVariable("JWT_ISSUER", "CarRentalAPI");
            Environment.SetEnvironmentVariable("JWT_AUDIENCE", "CarRentalClient");
            Environment.SetEnvironmentVariable("JWT_KEY", "your_super_secret_jwt_key_that_should_be_at_least_32_characters_long!");
            
            using var context = GetInMemoryContext();
            var authService = new AuthService(context);
            
            var registerDto = new RegisterUserDto
            {
                Email = "test@example.com",
                Password = "password123",
                ConfirmPassword = "password123"
            };
            await authService.RegisterAsync(registerDto);

            var loginDto = new UserDto
            {
                Email = "test@example.com",
                Password = "password123"
            };

            var result = await authService.LoginAsync(loginDto);

            Assert.NotNull(result);
            Assert.Equal("test@example.com", result.Email);
            Assert.NotEmpty(result.AccessToken);
            Assert.NotEmpty(result.RefreshToken);
        }
    }
}
