using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DataStore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<DataContext>(op =>
{
    op.UseInMemoryDatabase("PizzaOrders");
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning( op =>
{
    op.ReportApiVersions = true;
    op.AssumeDefaultVersionWhenUnspecified = true;
    op.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    op.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
else
{
    app.UseDeveloperExceptionPage();
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<DataContext>();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.Run();
