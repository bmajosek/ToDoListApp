using DocumentFormat.OpenXml.Spreadsheet;
using ToDoList.Model;

namespace ToDoList.Services.Interfaces;

public interface IUserRepository
{
    Task<ApiUser> GetUserById(string userId);

    Task<List<string>> GetFamilyMembersEmails(string userId);
}