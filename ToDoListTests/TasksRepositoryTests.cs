using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Data;
using ToDoList.Model;
using ToDoList.Services;
using DocumentFormat.OpenXml.InkML;

namespace ToDoListTests;

public class TasksRepositoryTests
{
    private readonly ApiDbContext _contextMock;
    private TasksRepository _tasksRepository;

    public TasksRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApiDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _contextMock = new ApiDbContext(options);

        var testData = new List<TaskToDo>
        {
            new TaskToDo { UserId = "user1", Description = "Task 1", FamilyId = "string" },
            new TaskToDo { UserId = "user1", Description = "Task 2", FamilyId = "string" },
            new TaskToDo { UserId = "user2", Description = "Task 3", FamilyId = "string" }
        };
        _contextMock.TasksToDo.AddRange(testData);
        _contextMock.SaveChanges();
    }

    [Fact]
    public async Task GetTasksByUserId_ReturnsTasks()
    {
        // Arrange
        _tasksRepository = new TasksRepository(_contextMock);

        // Act
        var result = await _tasksRepository.GetTasksByUserId("user1");

        // Assert
        Assert.Equal(2, result.Count);
        Assert.All(result, task => Assert.Equal("user1", task.UserId));
    }

    [Fact]
    public async Task DeleteTask_TaskIsDeleted()
    {
        // Arrange
        _tasksRepository = new TasksRepository(_contextMock);
        int taskIdToDelete = 1;

        // Act
        await _tasksRepository.DeleteTask(taskIdToDelete);
        var task = await _tasksRepository.GetByTaskId(taskIdToDelete);

        // Assert
        Assert.Null(task); // Task should no longer exist in the database
    }
}