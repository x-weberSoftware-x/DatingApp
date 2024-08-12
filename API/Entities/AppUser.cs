using API.Extensions;

namespace API.Entities;

public class AppUser
{
    //have to be made public to work with entity framework, also calling this Id tells entity framework this is our primary key
    //if we wanted a different primary key we could put the attribute [Key] over the variable we want to use
    //also since Id is an int this tells entity framework to auto increment it in the db
    public int Id { get; set; }
    
    //required means we cannot create an AppUser without a username, will not take a empty string either
    public required string UserName { get; set; }

    //password auth variables, we are going to hash and salt, this is basic auth
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
    public DateOnly DateOfBirth { get; set; }
    public required string KnownAs { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public required string Gender { get; set; }
    public string? Introduction { get; set; }
    public string? Interests { get; set; }
    public string? LookingFor { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public List<Photo> Photos { get; set; } = [];

    //automapper will map this to the Age property in our MemberDTO since we call this GetAge
    // public int GetAge()
    // {
    //     return DateOfBirth.CalculateAge();
    // }
}
