using Microsoft.EntityFrameworkCore;
using WeGout;
using WeGout.MapperProfiles;
using WeGout.Interfaces;
using WeGout.Services;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WGContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("WGContext"),ob=>ob.UseNetTopologySuite()));

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


builder.Services.AddAutoMapper(
    typeof(UserProfile),
    typeof(PlaceProfile)
    );
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPlaceService, PlaceService>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services
    .GetRequiredService<IServiceScopeFactory>()
    .CreateScope())
{
    using (var context = serviceScope.ServiceProvider.GetService<WGContext>())
    {
        context.Database.Migrate();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


