using MercatorBankAPIProj.Controllers;
using MercatorBankAPIProj.Data;
using MercatorBankAPIProj.Data.Repositories.Implementations;
using MercatorBankAPIProj.Data.Repositories.Interface;
using MercatorBankAPIProj.Models.Entities;
using MercatorBankAPIProj.Services.Implementation;
using MercatorBankAPIProj.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Fast api",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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


builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IMerchantUserService, MerchantUserService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IMerchantUserRepository, MerchantUserRepository>();

builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MercatorBankAPIConnection")));
builder.Services.AddIdentity<MerchantUser, IdentityRole>().AddEntityFrameworkStores<MyDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Key").Value);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateLifetime = true,
        ValidateAudience = false,
        ValidateIssuer = false
    };
});


var app = builder.Build();

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
