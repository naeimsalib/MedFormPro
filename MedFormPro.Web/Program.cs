using Microsoft.EntityFrameworkCore;
using MedFormPro.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext with PostgreSQL
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
if (string.IsNullOrEmpty(connectionString))
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}

// Convert Heroku PostgreSQL URL to connection string if needed
if (connectionString.StartsWith("postgres://"))
{
    var uri = new Uri(connectionString);
    var db = uri.AbsolutePath.TrimStart('/');
    var user = uri.UserInfo.Split(':')[0];
    var passwd = uri.UserInfo.Split(':')[1];
    var port = uri.Port > 0 ? uri.Port : 5432;
    var host = uri.Host;

    connectionString = $"Host={host};Database={db};Username={user};Password={passwd};Port={port};SSL Mode=Require;Trust Server Certificate=true";
}

// Configure DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (connectionString.Contains("postgres"))
    {
        options.UseNpgsql(connectionString);
    }
    else
    {
        options.UseSqlite(connectionString);
    }
});

// Add Authentication
builder.Services.AddAuthentication("Cookies")
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequirePharmacistRole", policy =>
        policy.RequireRole("Pharmacist"));
    options.AddPolicy("RequireReviewTeamRole", policy =>
        policy.RequireRole("ReviewTeam"));
    options.AddPolicy("RequireDeliveryTeamRole", policy =>
        policy.RequireRole("DeliveryTeam"));
});

var app = builder.Build();

// Initialize the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate(); // This will apply any pending migrations
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
