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
using ToDoList.Class.DTO;

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
        Assert.All(result, task => Assert.False(task.IsCompleted));
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
        Assert.Null(task);
    }

    [Fact]
    public async Task AddTask_AddsTaskSuccessfully()
    {
        // Arrange
        _tasksRepository = new TasksRepository(_contextMock);
        var newTask = new TaskToDoDTO { Description = "New Task" };

        // Act
        await _tasksRepository.AddTask(new ApiUser { Id = "user3", FamilyId = "family3" }, newTask);
        var addedTask = await _tasksRepository.GetTasksByUserId("user3");

        // Assert
        Assert.Single(addedTask);
        Assert.Equal("New Task", addedTask.First().Description);
    }

    [Fact]
    public async Task UpdateTask_UpdatesTaskCorrectly()
    {
        // Arrange
        _tasksRepository = new TasksRepository(_contextMock);
        var taskToUpdate = await _contextMock.TasksToDo.FirstAsync(t => t.Id == 1);
        var updatedInfo = new TaskToPatchDTO { Description = "Updated Task 1", IsCompleted = true };

        // Act
        await _tasksRepository.UpdateTask(taskToUpdate, updatedInfo);
        var updatedTask = await _tasksRepository.GetByTaskId(1);

        // Assert
        Assert.Equal("Updated Task 1", updatedTask.Description);
        Assert.True(updatedTask.IsCompleted);
    }
}