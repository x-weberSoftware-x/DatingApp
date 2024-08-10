using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

//not something you would add in a normal application this is just for error examples
public class BuggyController(DataContext context) : BaseApiController
{
    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetAuth()
    {
        return "secret text";
    }

    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFound()
    {   
        //try to find a user with the id of -1, this id does not extist so we will get the error
        var thing = context.Users.Find(-1);
        if( thing == null ) return NotFound();

        return thing;
    }

    [HttpGet("server-error")]
    public ActionResult<AppUser> GetServerError()
    {
        //null refernce error
        var thing = context.Users.Find(-1) ?? throw new Exception("A bad thing has happened");
        return thing;
    }

    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("This was not a good requestP");
    }
}
