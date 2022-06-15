using System.Text.Json.Serialization;
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
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning( op =>
{
    op.ReportApiVersions = true;
    op.AssumeDefaultVersionWhenUnspecified = true;
    op.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    op.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
});

builder.Services.AddVersionedApiExplorer(op => op.GroupNameFormat = "'v'VVV");

builder.Services.AddSwaggerGen( op =>
{
    op.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Web API v1", Version = "v1" });
    op.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Web API v2", Version = "v2" });
});

builder.Services.AddCors(op =>
{
    op.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://localhost:44393")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
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

    //Setting OpenAPI
    app.UseSwagger();
    app.UseSwaggerUI(op =>
    {
        op.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1");
        op.SwaggerEndpoint("/swagger/v2/swagger.json", "WebAPI v2");
    });
}

app.UseStaticFiles();

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.Run();
