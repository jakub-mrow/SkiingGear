using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SkiingGearBlazor.Data;

var builder = WebApplication.CreateBuilder(args);

//string dataSourcePath = "SkiingGear.DBSQL.dll";
string dataSourcePath = "DBMock.dll";

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<BL.BL>(blc =>
{
    return BL.BL.GetInstance(dataSourcePath);
});
builder.Services.AddSingleton<SkiingGearBLService>();


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

app.Run();
