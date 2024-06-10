using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Data;
using ToDoList.Model;
using ToDoList.Interface;
using ToDoList.Services.Interfaces;

namespace ToDoList.Services;

public class UserRepository : IUserRepository
{
    private readonly ApiDbContext _context;

    public UserRepository(ApiDbContext context)
    {
        _context = context;
    }

    public async Task<ApiUser> GetUserById(string userId)
    {
        return await _context.Users.SingleAsync(x => x.Id == userId);
    }

    public async Task<List<string>> GetFamilyMembersEmails(string userId)
    {
        var user = await _context.Users.SingleAsync(u => u.Id == userId);
        return await _context.Users
            .Where(u => u.FamilyId == user.FamilyId)
            .Select(u => u.Email)
            .ToListAsync();
    }
}