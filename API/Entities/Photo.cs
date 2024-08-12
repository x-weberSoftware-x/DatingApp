using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

//this will tell entity framework to name this table Photos instead of photo
[Table("Photos")]
public class Photo
{
    public int Id { get; set;}
    public required string Url { get; set; }
    public bool IsMain { get; set; }
    public string? PublicId { get; set; }

    //navigation properties
    //this is how we tell entity framework that these are required one to many relations to the users table
    //this essentially creates a not null AppUserId field in our photos table
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;
}