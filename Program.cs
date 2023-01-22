using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IHSA_Backend.Services;

var builder = WebApplication.CreateBuilder(args);
var appSettings = new AppSettings(builder.Configuration);

{
    // Add services to the container.
    var services = builder.Services;

    services.AddControllers();

    // Swagger
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    // JWT
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidIssuer = appSettings.JWTIssuer,
                ValidAudience = appSettings.JWTAudience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(appSettings.JWTSecret))
            };
        });
}

// Application Configuration
var app = builder.Build();

{
    // Swagger
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.UseAuthentication();

    app.MapControllers();
}

app.Run();