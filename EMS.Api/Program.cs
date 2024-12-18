using EMS.Application;
using EMS.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers(); // Registers the controllers for API endpoints

// Register application services 
builder.Services.AddApplicationServices();

// Register infrastructure services 
builder.Services.AddInfrastructureServices(builder.Configuration.GetConnectionString("DefaultConnection"));

// Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable CORS for local development
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Allow any origin (for development; consider more restrictive policies for production)
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Enable Swagger UI in development mode
    app.UseSwaggerUI();
}

// Use HTTPS Redirection (if applicable)
app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS

// Enable CORS policy
app.UseCors("AllowAll"); // Apply the "AllowAll" CORS policy

// Map API controllers to routes
app.MapControllers();

// Optionally, add ForwardedHeaders for reverse proxy scenarios (e.g., when deploying on servers with proxies/load balancers)
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
});

// Run the application
app.Run();
