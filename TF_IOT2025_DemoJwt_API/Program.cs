using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TF_IOT2025_DemoJwt_API.Entities.Contexts;
using TF_IOT2025_DemoJwt_API.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyProject", Version = "v1.0.0" });

    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securitySchema);

    var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                };

    c.AddSecurityRequirement(securityRequirement);

});

builder.Services.AddDbContext<MyDbContext>(o => o
    .UseSqlServer(builder.Configuration.GetConnectionString("Main")));

builder.Services.AddSingleton<JwtUtils>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer( o =>
{
    o.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration.GetSection("TokenInfo").GetSection("secret").Value!)),
        ValidateIssuer = false,
        ValidIssuer = builder.Configuration.GetSection("TokenInfo").GetSection("issuer").Value!,
        ValidateAudience = false,
        ValidAudience = builder.Configuration.GetSection("TokenInfo").GetSection("audience").Value!,
        ValidateLifetime = false
    };
});

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("Auth", p => p.RequireAuthenticatedUser());
    o.AddPolicy("Admin", p => p.RequireRole("Admin"));
});

builder.Services.AddCors(o =>
{
    o.AddPolicy("truc", p =>
    {
        p.WithOrigins("https://lemon-plant-076139e03.2.azurestaticapps.net");
        p.AllowAnyHeader();
        p.AllowAnyMethod();
        p.AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("truc");

app.MapControllers();

app.Run();
