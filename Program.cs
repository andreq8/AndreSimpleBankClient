using OpenBankClient.Data;
using OpenBankClient.Data.Services;
using OpenBankClient.Data.Services.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddTransient<UsersService>();
builder.Services.AddTransient<AccountsService>();
builder.Services.AddTransient<IClient, Client>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7268/") });
builder.Services.AddLocalization();



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
//app.UseRequestLocalization(new RequestLocalizationOptions()
//    .AddSupportedCultures(new[] { "en-US", "pt-PT" })
//    .AddSupportedUICultures(new[] { "en-US", "pt-PT" }));

var supportedCultures = new[] { "en-US", "pt-PT" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[1])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);
app.Run();
