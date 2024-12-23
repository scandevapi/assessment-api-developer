using assessment_api_developer.API.Middlewares;
using assessment_api_developer.Domain.Interfaces;
//using assessment_api_developer.Infra.DataContext;
using assessment_api_developer.Infra.Repositories;
using assessment_api_developer.Services.Services;
//using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add DBContext Service
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// Add Services
//builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();


// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy => policy.WithOrigins("https://localhost:7212")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});


var app = builder.Build();

// Use CORS
app.UseCors("AllowSpecificOrigin");

// Add Rate Limiting Middleware (Allow 60 requests per minute)
app.UseMiddleware<RateLimitingMiddleware>(60); 

// Add Error Handling Middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
