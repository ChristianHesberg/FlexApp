using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Authentication;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly IUserAuthenticator _userAuthenticator;
    
    public LoginController(IUserAuthenticator userAuthenticator)
    {
        _userAuthenticator = userAuthenticator;
    }
    
    [AllowAnonymous]
    [HttpPost]
    public IActionResult Post([FromBody] LoginInput model)
    {
        string userToken;
        if (_userAuthenticator.Login(model.Username, model.Password, out userToken))
        {
            //Authentication successful
            return Ok(new
            {
                username = model.Username,
                token = userToken
            });
        }
        return Unauthorized("Unknown username and password combination");
    }
    
    [AllowAnonymous]
    [HttpGet]
    [Route("db")]
    public IActionResult ResetDb()
    {
        _userAuthenticator.ResetDb();
        return NoContent();
    }
}