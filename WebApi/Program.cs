using WebApi.Extensions;
using WebApi.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEntityFramework(builder.Configuration);
builder.Services.AddJwtBearerProperties(builder.Configuration);
builder.Services.AddRepositoryService();
builder.Services.AddBllServices();
builder.Services.AddSwaggerProperties();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//builder.Host.AddLogger();

var app = builder.Build();

app.Services.InitializeDatabase();

app.UseMiddleware<ExeptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
