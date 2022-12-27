using WebSignalRServer;
using WebSignalRServer.Core;
using WebSignalRServer.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//GET SYS CONFIG
GlobalContext.SystemConfig = builder.Configuration.GetSection("SystemConfig").Get<SystemConfig>();

//FOR SIGNALR
builder.Services.AddSignalR();
//FOR SIGNALR
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

/*
 Typically, browsers load connections from the same domain as the requested page. However, there are occasions when a connection to another domain is required.

When making cross domain requests, the client code must use an absolute URL instead of a relative URL. For cross domain requests, change .withUrl("/chathub") to .withUrl("https://{App domain name}/chathub").

To prevent a malicious site from reading sensitive data from another site, cross-origin connections are disabled by default. To allow a cross-origin request, enable CORS:

UseCors must be called before calling MapHub.
 */
// global cors policy
app.UseCors(x => x
	.AllowAnyOrigin()
	.AllowAnyMethod()
	.AllowAnyHeader());


app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

//FOR SIGNALR
app.MapHub<ChatHub>("/chatHub");


app.Run();
