using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers;

public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
{

    [HttpPost("register")] //api/account/register
    //user our register dto object we created that and pass it in to access the object props of username and password
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
    {
        //check if user already exists using our built function below, if it returns true then return that its taken
        if(await UserExists(registerDTO.Username)) return BadRequest("Username is taken");

        //this will use a hashing algorithm to encrypt text
        //using means we are using this in this function, once we are done using it it will be disposed of by the garbage collector
        // using var hmac = new HMACSHA512();

        // var user = new AppUser 
        // {
        //     UserName = registerDTO.Username.ToLower(),
        //     //encode password into bytes
        //     PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
        //     //salt password
        //     PasswordSalt = hmac.Key
        // };

        // //add user to db
        // context.Users.Add(user);
        // //save the changes and await the save since this is a async method
        // await context.SaveChangesAsync();

        // //return userDTO because we only want to return the user and the token not the passwordhashes or id's from the user
        // return new UserDTO
        // {
        //     username = registerDTO.Username,
        //     token = tokenService.CreateToken(user)
        // };
        return Ok();
    }

    [HttpPost("login")] //api/account/login
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == loginDTO.Username.ToLower());
        if(user == null) return Unauthorized("Invalid username");

        //pass in the salt so it will have the same hash as the one stored in the db
        using var hmac = new HMACSHA512(user.PasswordSalt);
        //now compute the hash
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
        //compare the hash to the one stored in the db to see if they can login, this is an array of bytes
        //so need to loop
        for (int i = 0; i < computedHash.Length; i++)
        {   
            //if at any char the computed hash does not equal the users password hash its the wrong password
            if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
        }

        //since it got past the for loop we know the password was right so return the //return userDTO because we only want to return the user and the token not the passwordhashes or id's from the user
        return new UserDTO
        {
            username = loginDTO.Username,
            token = tokenService.CreateToken(user)
        };
  
    }


    private async Task<bool> UserExists(string username)
    {   
        //return whether or not the username we passed already exists in our db, 
        //that way we can have uniwue usernames only
        //make them both lowercase to ensure capitilization does not let a usernam slip through
        return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
    }
}
