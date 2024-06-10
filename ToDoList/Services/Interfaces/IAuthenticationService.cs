using ToDoList.Model;

namespace ToDoList.Interface;

public interface IAuthenticationService
{
    Task<AuthResult> Register(UserRegistrationRequest registrationRequest);

    Task<AuthResult> Login(UserLoginRequest loginRequest);
}