using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using back_end.Contracts.Requests.User;

namespace back_end.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController:ControllerBase
  {
    private readonly ILogger<UsersController> _logger;
    public UsersController(ILogger<UsersController> logger)
    {
      _logger = logger;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Login([FromBody]AuthenticateRequest user)
    {
      
      try{
        return Ok(new {user.Username,user.Password});
      }catch(Exception e)
      {
        _logger.LogCritical("Exception ",e.Message);
        return StatusCode(500);
      }
    }

    [AllowAnonymous]
    [HttpPost("subscribe")]
    public async Task<ActionResult> Subscribe([FromBody] UserSubscribeRequest user)
    {
      return Ok("Created");
    }
    
  }
}