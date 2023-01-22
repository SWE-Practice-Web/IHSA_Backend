using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IHSA_Backend.Services;

var builder = WebApplication.CreateBuilder(args);
var _appSettings = new AppSettings(builder.Configuration);

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
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _appSettings.JWTIssuer,
                ValidAudience = _appSettings.JWTAudience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_appSettings.JWTSecret))
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