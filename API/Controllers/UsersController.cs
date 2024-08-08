using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class UsersController(DataContext context) : BaseApiController
{
    //create api endpoints, we want these asynchronous code so that way multiple requests can happen at once

    [AllowAnonymous]
    [HttpGet]
    //get all users
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        //get list of users from our db
        var users = await context.Users.ToListAsync();

        return users;

    }

    [Authorize]
    //get a single user, using the id. so this would be /api/users/whatever id is
    //{} makes it dynamic so it has the /api/users before the /id otherwise it would just be /id
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        //get user from our db by finding the user with the id
        var user = await context.Users.FindAsync(id);

        //if the user does not exist then return a 404 not found, can do this because our function is a ActionResult
        if (user == null) return NotFound();

        return user;
    }
}
