using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IHSA_Backend.Services;
using IHSA_Backend.Collections;
using IHSA_Backend.Mapping;
using IHSA_Backend.BLL;
using IHSA_Backend.Filters;
using IHSA_Backend.Helpers;
using Microsoft.OpenApi.Models;
using IHSA_Backend.Middleware;

var builder = WebApplication.CreateBuilder(args);
var appSettings = new AppSettings(builder.Configuration);

{
    // Add services to the container.
    var services = builder.Services;

    services.AddControllers();
    services.AddAutoMapper(typeof(Program));

    // Swagger
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please insert JWT with Bearer into field",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
    });

    services.AddSingleton<IAppSettings>(appSettings);
    services.AddSingleton<IFirestore, Firestore>();

    // Collections
    services.AddSingleton<IRiderCollection, RiderCollection>();
    services.AddSingleton<ISchoolCollection, SchoolCollection>();
    services.AddSingleton<IEventCollection, EventCollection>();
    services.AddSingleton<IUserCollection, UserCollection>();

    // Request Handlers (BLL)
    services.AddSingleton<ISchoolRequestHandler, SchoolRequestHandler>();
    services.AddSingleton<IEventRequestHandler, EventRequestHandler>();
    services.AddSingleton<IRiderRequestHandler, RiderRequestHandler>();

    services.AddSingleton<IAuthRequestHandler, AuthRequestHandler>();

    // JWT
    services.AddSingleton<IJWTUtils, JWTUtils>();
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
    services.AddAuthorization();

    // CORS
    services.AddCors(options =>
    {
        options.AddDefaultPolicy(
            builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }
        );
    });

    // Filters
    services.AddControllers(options =>
    {
        options.Filters.Add<HttpResponseExceptionFilter>();
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
        app.UseCors();
    }

    app.UseHttpsRedirection();

    // JWT
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseMiddleware<JWTMiddleware>();

    app.MapControllers();
}

app.Run();