using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using back_end.Models;
using Microsoft.Extensions.Logging;

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
    public async Task<ActionResult> Login([FromBody]AuthenticateModel user)
    {
      try{
        return Ok(new {user.Email,user.Password});
      }catch(Exception e)
      {
        _logger.LogCritical("Exception ",e.Message);
        return StatusCode(500);
      }
    }

    [AllowAnonymous]
    [HttpPost("subscribe")]
    public async Task<ActionResult> Subscribe([FromBody] SubscriptionModel user)
    {
      return Ok("Created");
    }
    
  }
}