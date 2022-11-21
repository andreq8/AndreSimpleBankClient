using Microsoft.AspNetCore.Components.Authorization;
using OpenBankClient;
using OpenBankClient.Data;
using OpenBankClient.Data.Providers;
using OpenBankClient.Data.Services;
using OpenBankClient.Data.Services.Base;
using OpenBankClient.Data.Services.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IAccountsService, AccountsService>();
builder.Services.AddTransient<ITransfersService, TransfersService>();
builder.Services.AddTransient<IClient, Client>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]) });
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddLocalization();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
var supportedCultures = new[] { "en-US", "pt-PT" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[1])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
//app.UseRequestLocalization(new RequestLocalizationOptions()
//    .AddSupportedCultures(new[] { "en-US", "pt-PT" })
//    .AddSupportedUICultures(new[] { "en-US", "pt-PT" }));


app.Run();
