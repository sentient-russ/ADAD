using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using adad.Areas.Identity.Data;
using adad.Areas.Identity.Services;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using System.Security.Authentication;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.ResponseCompression;
using SignalRChat.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddSignalR();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor;
    options.ForwardedHeaders = ForwardedHeaders.XForwardedProto;
    options.ForwardedHeaders = ForwardedHeaders.All;
    options.RequireHeaderSymmetry = false;
    options.ForwardLimit = 2;
    options.KnownProxies.Add(IPAddress.Parse("127.0.0.1")); //reverse proxy, Kestrel defaults to port 5000 which is also set in apsettings.json
    options.KnownProxies.Add(IPAddress.Parse("162.205.232.101")); //server IP public

});
builder.WebHost.ConfigureKestrel((context, serverOptions) =>
{
    serverOptions.ConfigureHttpsDefaults(listenOptions =>
    {
        listenOptions.SslProtocols = SslProtocols.Tls13;
        listenOptions.ClientCertificateMode = ClientCertificateMode.AllowCertificate;//requires certificate from client
    });
});
var connectionString = "";
var MD_Email_Pass = "";
var Accuweather_Key = "";
var Google_Key = "";
var Google_Maps_API_Key = "";
var environ = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
if (environ == "Production")
{
    //pulls connection string from environment variables
    connectionString = Environment.GetEnvironmentVariable("MariaDbConnectionStringLocal");
    MD_Email_Pass = Environment.GetEnvironmentVariable("MD_Email_Pass");
    Accuweather_Key = Environment.GetEnvironmentVariable("Accuweather_Key");
    Google_Key = Environment.GetEnvironmentVariable("Google_Key");
    Google_Maps_API_Key = Environment.GetEnvironmentVariable("Google_Maps_API_Key");

}
else
{
    //pulls connection string from development local version of secrets.json
    connectionString = builder.Configuration.GetConnectionString("MariaDbConnectionStringRemote");
    MD_Email_Pass = builder.Configuration.GetConnectionString("MD_Email_Pass");
    Accuweather_Key = builder.Configuration.GetConnectionString("Accuweather_Key");
    Google_Key = builder.Configuration.GetConnectionString("Google_Key");
    Google_Maps_API_Key = builder.Configuration.GetConnectionString("Google_Maps_API_Key");

}
Environment.SetEnvironmentVariable("DbConnectionString", connectionString);
Environment.SetEnvironmentVariable("MD_Email_Pass", MD_Email_Pass);
Environment.SetEnvironmentVariable("Accuweather_Key", Accuweather_Key);
Environment.SetEnvironmentVariable("Google_Key", Google_Key);
Environment.SetEnvironmentVariable("Google_Maps_API_Key", Google_Maps_API_Key);


var serverVersion = new MySqlServerVersion(new Version(10, 6, 11));
//use this option for a stable normal configuration
builder.Services.AddDbContext<ApplicationDbContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(connectionString, serverVersion, options => options.EnableRetryOnFailure())

        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);
//use for code first migrations with mysql only
/*builder.Services.AddDbContext<ApplicationDbContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(connectionString, serverVersion, options => options.SchemaBehavior(Pomelo.EntityFrameworkCore.MySql.Infrastructure.MySqlSchemaBehavior.Ignore))
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);*/

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddHttpClient();
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = false;
});
builder.Services.AddAuthorization();
//addition of encryption methods for deployment on linux
builder.Services.AddDataProtection().UseCryptographicAlgorithms(
    new AuthenticatedEncryptorConfiguration
    {
        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
    });
builder.Services.AddResponseCompression(options =>
 options.MimeTypes = ResponseCompressionDefaults
 .MimeTypes.Concat(new[] { "application/octet-stream:" })
);
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});
builder.Services.AddMvc();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
/*app.UseHttpsRedirection();*/
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "portal",
    pattern: "{controller=PortalController}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "portal",
    pattern: "{controller=PortalController}/{action=Edit}/{id?}");
app.MapRazorPages();
app.MapHub<DataHub>("/DataHub");
app.Run();
