using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

//primary constructor is a cleaner contructor that goes up with the class declaration, instead of having a seperate constructor inside the class
public class DataContext(DbContextOptions options) : DbContext(options)
{
    //entity framework now knows to create a table called Users
    public DbSet<AppUser> Users { get; set; }
}
