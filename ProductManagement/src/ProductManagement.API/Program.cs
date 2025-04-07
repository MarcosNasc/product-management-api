using Microsoft.EntityFrameworkCore;
using ProductManagement.API.DepedencyInjection;
using ProductManagement.Infrastructure.DependencyInjection;
using ProductManagement.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddUseCasesHandlers();
builder.Services.AddInfrastructure(builder.Configuration);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate(); // Isso aplica as migrations pendentes
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
