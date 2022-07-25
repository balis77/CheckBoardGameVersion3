
using CheckBoardGameVersion3.Server.Data;
using CheckBoardGameVersion3.Server.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration; // allows both to access and to set up the config
IWebHostEnvironment environment = builder.Environment;



// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddSignalR(opt =>
{
    //opt.SupportedProtocols;
    //opt.StreamBufferCapacity;
    opt.ClientTimeoutInterval = System.TimeSpan.FromSeconds(1);
    //opt.MaximumParallelInvocationsPerClient;
    //opt.EnableDetailedErrors;
    opt.HandshakeTimeout = System.TimeSpan.FromSeconds(1);
    opt.KeepAliveInterval = System.TimeSpan.FromSeconds(1);
    //opt.MaximumParallelInvocationsPerClient;
    //opt.MaximumReceiveMessageSize = 20480;
    //opt.StreamBufferCapacity;
    //opt.SupportedProtocols;

    //opt.StreamBufferCapacity = 2048;

    //opt.KeepAliveInterval = System.TimeSpan.FromSeconds(1);
    //opt.HandshakeTimeout = System.TimeSpan.FromSeconds(1);
    //opt.ClientTimeoutInterval = System.TimeSpan.FromSeconds(1);

    if (!environment.IsDevelopment())
        opt.EnableDetailedErrors = true;
});


builder.Services.AddSingleton<TableManager>();
builder.Services.AddServerSideBlazor();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

//app.UseStaticFiles(new StaticFileOptions
//{
//    RequestPath = "/Files",
//    FileProvider = new PhysicalFileProvider(Path.Combine(environment.ContentRootPath, "Files"))
//});


app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();


app.MapRazorPages();
app.MapHub<BoardHub>("/BoardHub");
app.MapControllers();
app.MapBlazorHub();

app.MapFallbackToFile("index.html");

app.Run();
