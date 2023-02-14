using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IHSA_Backend.Services;
using IHSA_Backend.Collections;

var builder = WebApplication.CreateBuilder(args);
var appSettings = new AppSettings(builder.Configuration);

{
    // Add services to the container.
    var services = builder.Services;

    services.AddControllers();

    // Swagger
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddSingleton<IAppSettings>(appSettings);
    services.AddSingleton<IFirestore, Firestore>();

    // Collections
    services.AddSingleton<IUserCollection, UserCollection>();
    services.AddSingleton<IRiderCollection, RiderCollection>();

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