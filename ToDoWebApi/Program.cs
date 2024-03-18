using Microsoft.EntityFrameworkCore;
using ToDoWebApi.Context;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ToDoContext>(options => options.UseInMemoryDatabase("ToDoDb"));
// InMemory DB, uygulama her �al��t�r�ld���nda s�f�rdan bir veritaban� olu�turur. Uygulama kapat�l�rsa veritaban� silinir. TEMPORARY yani ge�icidir.

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
