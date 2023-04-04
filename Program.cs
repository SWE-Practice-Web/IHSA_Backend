using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IHSA_Backend.Services;
using IHSA_Backend.Collections;
using IHSA_Backend.Mapping;
using IHSA_Backend.BLL;

var builder = WebApplication.CreateBuilder(args);
var appSettings = new AppSettings(builder.Configuration);

{
    // Add services to the container.
    var services = builder.Services;

    services.AddControllers();
    services.AddAutoMapper(typeof(Program));

    // Swagger
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddSingleton<IAppSettings>(appSettings);
    services.AddSingleton<IFirestore, Firestore>();

    // Collections
    services.AddSingleton<IUserCollection, UserCollection>();
    services.AddSingleton<IRiderCollection, RiderCollection>();
    services.AddSingleton<ISchoolCollection, SchoolCollection>();
    services.AddSingleton<IEventCollection, EventCollection>();

    // Request Handlers
    services.AddSingleton<ISchoolRequestHandler, SchoolRequestHandler>();
    services.AddSingleton<IEventRequestHandler, EventRequestHandler>();
    services.AddSingleton<IRiderRequestHandler, RiderRequestHandler>();

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

    app.UseCors();

    app.UseHttpsRedirection();

    // JWT
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
}

app.Run();