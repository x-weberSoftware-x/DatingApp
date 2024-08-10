using API.Extensions;
using API.MiddleWare;


var builder = WebApplication.CreateBuilder(args);

//use our extension methods
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleWare>();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("http://localhost:4200", "https://localhost:4200" ));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
