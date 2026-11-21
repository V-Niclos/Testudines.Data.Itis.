using Microsoft.EntityFrameworkCore;
using Testudines.Data.Itis;
using Testudines.Data.Itis.Context;

using Testudines.Data.Itis.TestBlazor.Components;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//--------------------------------------
var itisConnection = builder.Configuration.GetConnectionString("ItisConnection")
    ?? throw new InvalidOperationException("Connection string 'ItisConnection' not found.");

builder.Services.AddDbContextFactory<ItisDbContext>(options =>
    options.UseMySql(itisConnection, ServerVersion.AutoDetect(itisConnection)));

//--------------------------------------


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
