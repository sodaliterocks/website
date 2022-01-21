using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Localization;
using SoloX.BlazorJsonLocalization;
using System.Globalization;
using SodaliteWeb.Data;

var supportedCultures = new List<CultureInfo>
{
    new CultureInfo("de"),    // German
    new CultureInfo("en"),    // English
    new CultureInfo("en-GB"), // English (United Kingdom)
    new CultureInfo("fr"),    // French
    new CultureInfo("pt")     // Portuguese
};

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddJsonLocalization(
    builder => builder.UseEmbeddedJson(
        options => options.ResourcesPath = "I18n"),
        ServiceLifetime.Singleton);

builder.Services.Configure<RequestLocalizationOptions>(options => {
    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRequestLocalization();
app.UseRouting();
app.UseStaticFiles();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();