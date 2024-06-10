using Xunit;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Model;
using ToDoList.Services;
using System.Threading.Tasks;
using System.Linq;

namespace ToDoListTests;

public class UserRepositoryTests
{
    private readonly ApiDbContext _contextMock;
    private UserRepository _userRepository;

    public UserRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApiDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _contextMock = new ApiDbContext(options);

        // Seed the database
        var users = new List<ApiUser>
        {
            new ApiUser { Id = "user1", Email = "user1@example.com", FamilyId = "family1" },
            new ApiUser { Id = "user2", Email = "user2@example.com", FamilyId = "family1" },
            new ApiUser { Id = "user3", Email = "user3@example.com", FamilyId = "family2" }
        };
        _contextMock.Users.AddRange(users);
        _contextMock.SaveChanges();
    }

    [Fact]
    public async Task GetUserById_ReturnsCorrectUser()
    {
        // Arrange
        _userRepository = new UserRepository(_contextMock);
        // Act
        var user = await _userRepository.GetUserById("user1");

        // Assert
        Assert.NotNull(user);
        Assert.Equal("user1@example.com", user.Email);
    }

    [Fact]
    public async Task GetFamilyMembersEmails_ReturnsFamilyMembers()
    {
        // Arrange
        _userRepository = new UserRepository(_contextMock);

        // Act
        var emails = await _userRepository.GetFamilyMembersEmails("user1");

        // Assert
        Assert.Equal(2, emails.Count); // Expect two users in the same family
        Assert.Contains("user1@example.com", emails);
        Assert.Contains("user2@example.com", emails);
    }
}