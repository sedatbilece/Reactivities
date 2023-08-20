using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//add db context
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// add cors policy
builder.Services.AddCors(opt=>{
    opt.AddPolicy("CorsPolicy",policy=>{
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

//add cors policy
app.UseCors("CorsPolicy");

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

//migrate in try catch block
try
{
    var context = services.GetRequiredService<DataContext>();
     await context.Database.MigrateAsync();
     await SeedData.SeedDataFunction(context);

      var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Migration successful");
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();
