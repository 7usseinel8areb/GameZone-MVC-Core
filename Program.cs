

var builder = WebApplication.CreateBuilder(args);
//Connection string
var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("No connection string was found");
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<applicationDbContext>(options =>
    options.UseSqlServer(ConnectionString));

builder.Services.AddScoped<ICategoriesServices, CategoriesServices>();
builder.Services.AddScoped<IDevicesServices, DevicesServices>();
builder.Services.AddScoped<IGameServices, GameServices>();

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
