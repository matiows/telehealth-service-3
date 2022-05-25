using telehealth;
using telehealth.Context;
using telehealth.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3000")
                          .AllowAnyOrigin()
                          .AllowAnyHeader();
                      });
});

builder.Services.AddControllers(
    options => options
    .SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions
                .ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration
    .GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();


