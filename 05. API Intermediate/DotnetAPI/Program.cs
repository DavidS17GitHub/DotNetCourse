using System.Text;
using DotnetAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

// Create a new WebApplication instance.
var builder = WebApplication.CreateBuilder(args);

// Add MVC controllers to the service container.
builder.Services.AddControllers();

// Add services for Swagger/OpenAPI documentation.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS policies for different environments.
builder.Services.AddCors((options) => 
    {
        // Development CORS policy allowing requests from specified origins with any method, header, and credentials.
        options.AddPolicy("DevCors", (corsBuilder) =>
            {
                corsBuilder.WithOrigins("http://localhost:4200, http://localhost:3000, http://localhost:8000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        // Production CORS policy allowing requests only from the production site with any method, header, and credentials.
        options.AddPolicy("ProdCors", (corsBuilder) =>
            {
                corsBuilder.WithOrigins("https://myProductionSite.com")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
    });

// Add scoped service for user repository.
builder.Services.AddScoped<IUserRepository, UserRepository>(); 
// This adds the scope that connects the interface to the Class it belongs to

// Retrieve token key string from configuration.
string? tokenKeyString = builder.Configuration.GetSection("AppSettings:TokenKey").Value;

// Configure JWT authentication.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                tokenKeyString != null ? tokenKeyString : ""
            )),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Build the application.
var app = builder.Build();

// Configure the HTTP request pipeline based on environment
if (app.Environment.IsDevelopment())
{
    // Use the Development CORS policy
    app.UseCors("DevCors");
    // Enable Swagger UI for API documentation
    app.UseSwagger();
    app.UseSwaggerUI();
}
else {
    // Use the Production CORS policy.
    app.UseCors("ProdCors");
    // Redirect HTTP requests to HTTPS.
    app.UseHttpsRedirection();
}

// Use authentication and authorization middleware.
app.UseAuthentication();
app.UseAuthorization();

// Map controllers.
app.MapControllers();

// Run the application.
app.Run();

