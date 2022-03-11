using CryptoBL;
using CryptoDL;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Need a scope once we get repository and DL/BL
builder.Services.AddScoped<IRepository>(repo => new Repository(builder.Configuration.GetConnectionString("Reference2DB")));
builder.Services.AddScoped<ICryptoClassBL, CryptoClassBL>();
var app = builder.Build();

//Creating and configuring logger
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("./logs/user.txt") //Configured logger to save in this file 
    .CreateLogger();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
