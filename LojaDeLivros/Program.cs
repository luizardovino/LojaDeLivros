using LojaDeLivros.Models;
using LojaDeLivros.Pages;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LojaDbContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddScoped<DbInitializer>();

builder.Services.AddScoped<ServicoBancoDeDados>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<DbInitializer>();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
