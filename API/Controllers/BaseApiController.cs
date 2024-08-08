using System;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
//this means our route will be api/users, 
//[controller] gets replaced with part of class before Controller
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{

}
