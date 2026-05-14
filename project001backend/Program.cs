using project001backend.Data;
using project001backend.Models;
using project001backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Bind MongoDbSettings from appsettings.json
        builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

        // Render provides a PORT environment variable
        var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
        builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

        var frontendUrl = builder.Configuration["UI:Url"] ?? "http://localhost:5173";

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.WithOrigins(frontendUrl.Split(','))
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });
        // 1️⃣ Register services
        builder.Services.AddControllers();
        builder.Services.AddSingleton<JWTService>();
        builder.Services.AddSingleton<Customer>();
        builder.Services.AddSingleton<AuthServices>();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   var config = builder.Configuration;
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidIssuer = config["Jwt:Issuer"],
                       ValidateAudience = true,
                       ValidAudience = config["Jwt:Audience"],
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])),
                       ValidateLifetime = true,
                   };
               });
        builder.Services.AddAuthorization();

        // Swagger (optional)
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "Enter 'Bearer' [space] and then your token. Example: Bearer eyJhbGciOiJIUzI1NiIsInR..."
            });

            c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
        });
        var app = builder.Build();

        // 2️⃣ Configure middleware pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("AllowFrontend");

        app.UseHttpsRedirection();
        app.UseAuthentication(); // must come BEFORE UseAuthorization
        app.UseAuthorization();

        app.MapControllers();
        app.Run();

    }
}