using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Interface;
using StudentManagementAPI.Repository;
using StudentManagementAPI.DbContext;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using StudentManagementAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Token only"
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
            new string[] {}
        }
    });
});

// DB Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("constr")));

// Dependency Injection
builder.Services.AddScoped<IStudentInterface, StudentRepository>();

// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();   // VERY IMPORTANT
app.UseAuthorization();

app.MapControllers();

app.Run();





















//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;
//using StudentManagementAPI.Interface;
//using StudentManagementAPI.Repository;
//using StudentManagementAPI.DbContext;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;
//using StudentManagementAPI.Models;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using Microsoft.OpenApi.Models;



//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
////var jwtSetting = builder.Configuration.GetSection("jwt");

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<AplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("constr")));
//builder.Services.AddScoped<IStudentInterface, IStudentInterface>();
//builder.Services.AddScoped<IStudentInterface, StudentRepository>();

//builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContex>().AddDefaultTokenProviders();
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = "JwtBearer";
//    options.DefaultChallengeScheme = "JwtBearer";
//}
//).AddJwtBearer("jwtBearer", Options =>
//{
//Options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//{
//    ValidateUser = true,
//    ValidateAudience = true,
//    ValidateLifetime = true,
//    ValidateIssuerSigningKey = true,
//    ValidIssuer = builder.Configuration["Jwt.Issuer"],
//    ValidateAudience = builder.Configuration["Jwt.Audience"],
//    IssuerSigningKey = new SymetricSecurityKey(Encodeing.UTFB.GetBytes(builder.Configuration("JwtBearer")))
//}
//)
//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

////dotnet ef migration add InitialCreate
////dotnet add package  microsoft .EntityFrame
////Microsoft.EntityFrameworkCore.Design