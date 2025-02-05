using UI.Components;
using UI.Extensions;
using Refit;
using Microsoft.Extensions.Options;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddOptions<TaskTrackerSettings>()
//    .BindConfiguration(TaskTrackerSettings.ConfigurationSection)
//    .ValidateDataAnnotations()
//    .ValidateOnStart();

builder.Services.AddApi(builder.Configuration);
builder.Services.AddMudServices();


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();
//app.UseAuthentication();
//app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
