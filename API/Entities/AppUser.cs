namespace API.Entities;

public class AppUser
{
    //have to be made public to work with entity framework, also calling this Id tells entity framework this is our primary key
    //if we wanted a different primary key we could put the attribute [Key] over the variable we want to use
    //also since Id is an int this tells entity framework to auto increment it in the db
    public int Id { get; set; }
    //required means we cannot create an AppUser without a username, will not take a empty string either
    public required string UserName { get; set; }
}
