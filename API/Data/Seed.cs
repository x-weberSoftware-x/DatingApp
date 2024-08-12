using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{
    public static async Task SeedUsers(DataContext context)
    {
        //if there are any users we want to return out of here since this is a seed (db starting file)
        if (await context.Users.AnyAsync()) return;

        var userData = await File.ReadAllTextAsync("data/UserSeedData.json");

        var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
        var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);

        if (users == null) return;

        foreach (var user in users)
        {
            using var hmac = new HMACSHA512();
            user.UserName = user.UserName.ToLower();
            //password is being hardcoded just since this is a course, woudl not do this professionally
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
            user.PasswordSalt = hmac.Key;

            //tell entity framework to add/track user
            context.Users.Add(user);
        }

        //tell entity framework to save the above changes (users) to the db
        await context.SaveChangesAsync();
    }
}
