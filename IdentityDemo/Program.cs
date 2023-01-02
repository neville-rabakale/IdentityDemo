using IdentityDemo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<AccountService>();

// Registrera Identitys DB-kontext
var connString = builder.Configuration
    .GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<IdentityDbContext>(
    o => o.UseSqlServer(connString));

// Konfigurera Identity att använda dess inbyggda klasser för att representera använare och roller mot vår DB-kontext
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
 .AddEntityFrameworkStores<IdentityDbContext>()
 .AddDefaultTokenProviders();

// Konfigurera var icke-autenticerade användare ska skickas
builder.Services.ConfigureApplicationCookie(
    o => o.LoginPath = "/login");

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication(); // Identifiering
app.UseAuthorization(); // Behörighet

app.UseEndpoints(o => o.MapControllers());

app.Run();