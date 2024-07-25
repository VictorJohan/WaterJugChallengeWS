using WaterJugChallengeWS.Helpers;
using WaterJugChallengeWS.Interfaces;
using WaterJugChallengeWS.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();


// Add services to the container.
//Transient
builder.Services.AddTransient<WaterJugService>();
builder.Services.AddTransient<GlobalExceptionHandler>();

//Singleton
builder.Services.AddSingleton<ICacheService, CacheService>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionHandler>();
app.UseAuthorization();

app.MapControllers();

app.Run();
