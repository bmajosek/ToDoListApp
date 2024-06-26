﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Class.DTO;
using ToDoList.Model;

namespace ToDoList.Interface;

public interface ITasksRepository
{
    Task<List<TaskToDoReadDTO>> GetTasksByUserId(string userId);

    Task<List<TaskToDoReadDTO>> GetFamilyTasks(string userId);

    Task<TaskToDo?> GetByTaskId(int taskId);

    Task AddTask(ApiUser user, TaskToDoDTO task);

    Task DeleteTask(int taskId);

    Task UpdateTask(TaskToDo task, TaskToPatchDTO taskToPatch);
}