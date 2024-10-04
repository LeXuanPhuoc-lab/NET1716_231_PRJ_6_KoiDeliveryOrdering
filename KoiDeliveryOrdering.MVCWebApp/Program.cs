using KoiDeliveryOrdering.MVCWebApp.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorPagesOptions(opt =>
    {
        opt.RootDirectory = "/Authentication";
    });

// Add Session
builder.Services.AddSession();

// Add CORS 
builder.Services.AddCors(p => p.AddPolicy("Cors", policy =>
{
	policy.WithOrigins("*")
		  .AllowAnyHeader()
		  .AllowAnyMethod();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseCors("Cors");
app.UseRouting();
app.UseSession();

// Register the custom session check middleware
app.UseMiddleware<SessionCheckMiddleware>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();