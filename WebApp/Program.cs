using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;
using EFCore;
//Login
using System.Net.Http;
using EFCore.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebApp.DataServices.Services.TMDB;
using WebApp.DataServices.Services.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
AddServices(builder);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<HttpContextAccessor>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);


//Services that were added for the same reason. ---------
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
    options.Cookie.SameSite = SameSiteMode.Lax;
});

builder.Services.Configure<CookiePolicyOptions>(options => {
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});



builder.Services.AddAuthentication().AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Google:ClientId"];
    options.ClientSecret = builder.Configuration["Google:ClientSecret"];
    
    
    //This is to get the profile picture and name.
    options.ClaimActions.MapJsonKey("urn:google:profile", "link");
    options.ClaimActions.MapJsonKey("urn:google:image", "picture");
});

if (builder.Environment.EnvironmentName=="Development")
    Config.Init(ConfigVariables.Variables("Default"));
if (builder.Environment.EnvironmentName=="Production")
    Config.Init(ConfigVariables.Variables("Default"));

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

//ALSO ADDED ---------
app.UseCookiePolicy();
app.UseAuthentication();


app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

static void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<ITMDBClient,TMDBClient>();
    builder.Services.AddTransient<IDataService,DataService>();
    builder.Services.AddTransient<IUserService,UserService>();
    builder.Services.AddTransient<IToplistService,ToplistService>();
}
    
