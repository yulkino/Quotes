using Application;
using Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Presentation.View.Data;
using Quotes.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddRazorPages();
services.AddServerSideBlazor();
services.AddInfrastructure(builder.Configuration.GetConnectionString("DefaultConnectionString"));
services.AddApplication();
services.AddControllers();
services.AddAutoMapper(typeof(AuthorsController));
services.AddAutoMapper(typeof(QuotesController));

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
