using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Security.Authentication;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class RegisterUserController : ControllerBase
{
    private readonly IUserAuthenticator _userAuthenticator;
    
    public RegisterUserController(IUserAuthenticator userAuthenticator)
    {
        _userAuthenticator = userAuthenticator;
    }
    
    // POST: api/Login
    [HttpPost]
    public IActionResult Post([FromBody] RegisterUser model)
    {
        string username = model.Username;
        string password = model.Password;

        if (_userAuthenticator.CreateUser(username, password))
        {
            //Authentication succesful
            return Ok();
        }
        else
        {
            return Problem("Could not create user with name: " + username);
        }
    }
}