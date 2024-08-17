using System.Security.Claims;
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

    //update
    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDTO memberUpdateDTO)
    {
        //because the user has to be authorized here we can get the user name from the claimtypes
        //in out tokenservice we specified our name identifier to be our username
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(username == null) return BadRequest("No username found in token");

        var user = await userRepository.GetUserByUsernameAsync(username);
        if (user == null) return BadRequest("Could not find user");

        //map the member dto to our user we now have
        mapper.Map(memberUpdateDTO, user);
        //if this save succeeded then just return no content since this is update function so we dont return anything
        if( await userRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Failed to update the user");
    } 
}
