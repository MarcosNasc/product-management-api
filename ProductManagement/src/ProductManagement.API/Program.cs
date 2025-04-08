using Microsoft.EntityFrameworkCore;
using ProductManagement.API.DepedencyInjection;
using ProductManagement.Infrastructure.DependencyInjection;
using ProductManagement.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddUseCasesHandlers();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Isso aplica as migrations pendentes se rodar local
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate(); 
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
