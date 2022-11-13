using Microsoft.EntityFrameworkCore;
using MyWebApi.Ef;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

//configure postgres
builder.Services.AddDbContext<BlogDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//https://www.hanselman.com/blog/exploring-a-minimal-web-api-with-aspnet-core-6
//https://khalidabuhakmeh.com/simple-redirects-with-aspnet-core-endpoint-routing

app.MapGet("/", ctx =>
{
    ctx.Response.Redirect("/swagger", true);
    return Task.CompletedTask;
});

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();

//    endpoints.MapGet("/", ctx => { 
//        ctx.Response.Redirect("/swagger", true); 
//        return Task.CompletedTask; 
//    });
//});


app.Run();
