using Microsoft.EntityFrameworkCore;
using ToDoWebApi.Context;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ToDoContext>(options => options.UseInMemoryDatabase("ToDoDb"));
// InMemory DB, uygulama her çalýþtýrýldýðýnda sýfýrdan bir veritabaný oluþturur. Uygulama kapatýlýrsa veritabaný silinir. TEMPORARY yani geçicidir.

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseAuthorization();

app.Run();
