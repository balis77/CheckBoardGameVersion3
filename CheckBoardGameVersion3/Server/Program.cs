
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.FileProviders;


var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration; // allows both to access and to set up the config
IWebHostEnvironment environment = builder.Environment;



// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


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

app.UseStaticFiles(new StaticFileOptions
{
    RequestPath = "/Files",
    FileProvider = new PhysicalFileProvider(Path.Combine(environment.ContentRootPath, "Files"))
});

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
