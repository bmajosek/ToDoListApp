using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Data;
using ToDoList.Model;
using ToDoList.Interface;
using DocumentFormat.OpenXml.Spreadsheet;
using ToDoList.Services.Interfaces;
using ToDoList.Class.DTO;

namespace ToDoList.Services;

public class TasksRepository : ITasksRepository
{
    private readonly ApiDbContext _context;

    public TasksRepository(ApiDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskToDo>> GetTasksByUserId(string userId)
    {
        return await _context.TasksToDo.Where(t => t.UserId == userId).ToListAsync();
    }

    public async Task<TaskToDo?> GetByTaskId(int taskId)
    {
        return await _context.TasksToDo.FirstOrDefaultAsync(x => x.Id == taskId);
    }

    public async Task<List<TaskToDoReadDTO>> GetFamilyTasks(string userId)
    {
        var user = await _context.Users.SingleAsync(u => u.Id == userId);
        return await _context.TasksToDo
            .Where(t => t.FamilyId == user.FamilyId)
            .Select(t => new TaskToDoReadDTO { Description = t.Description, Id = t.Id, IsCompleted = t.IsCompleted })
            .ToListAsync();
    }

    public async Task AddTask(ApiUser user, TaskToDoDTO taskToDoDTO)
    {
        var task = new TaskToDo
        {
            Description = taskToDoDTO.Description,
            UserId = user.Id,
            FamilyId = user.FamilyId,
            IsCompleted = false,
            User = user,
        };
        _context.Add(task);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTask(int taskId)
    {
        var task = await _context.TasksToDo.SingleAsync(t => t.Id == taskId);
        _context.Remove(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTask(TaskToDo task)
    {
        _context.Update(task);
        await _context.SaveChangesAsync();
    }
}