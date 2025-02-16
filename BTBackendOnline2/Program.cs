using BTBackendOnline2.DB;
using BTBackendOnline2.Services.Implements;
using BTBackendOnline2.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectString = builder.Configuration.GetConnectionString("DefaultConnect");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectString);
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRole, RoleService>();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IAllowAccess, AllowAccessService>();

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

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ISC BT Online 2");
    c.RoutePrefix = "";
});

app.Run();
