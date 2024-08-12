using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseApiController
{
    //create api endpoints, we want these asynchronous code so that way multiple requests can happen at once

    [HttpGet]
    //get all users
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
    {
        //get list of users from our db using our created repository interface
        var users = await userRepository.GetMembersAsync();

        return Ok(users);
    }

    //get a single user, using the id. so this would be /api/users/whatever id is
    //{} makes it dynamic so it has the /api/users before the /username otherwise it would just be /username
    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDTO>> GetUser(string username)
    {
        //get user from our db by finding the user with the username
        var user = await userRepository.GetMemberAsync(username);

        //if the user does not exist then return a 404 not found, can do this because our function is a ActionResult
        if (user == null) return NotFound();

        return user;
    }
}
