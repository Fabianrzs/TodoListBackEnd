using DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NgrokApi;
using Site.Config;
using Site.Middleware;
using System.Configuration;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


var services = builder.Services;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnnetion");
services.AddDbContext<TodoListContext>(p => p.UseSqlServer(connectionString));

//services.AddCors(options => options.AddPolicy("CorsPolicy",
//    builder =>
//    {
//        builder
//            .WithOrigins("http://localhost:3000")
//            .AllowAnyMethod()
//            .AllowAnyHeader();
//    }));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000/");
        });
});

Ioc.AddDependecy(services);

#region    configure strongly typed settings objects
var appSettingsSection = builder.Configuration.GetSection("AppSetting");
services.Configure<AppSetting>(appSettingsSection);
#endregion

#region Configure jwt authentication inteprete el token 
var appSettings = appSettingsSection.Get<AppSetting>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);
services.AddControllers();
services.AddCors();


services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
#endregion

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service", Version = "v1" });
    c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "bearerAuth"
                                }
                            },
                            new string[] {}
                    }
                });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseMiddleware<Authentication>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//swagger
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestService");
});

//JWT
#region global cors policy activate Authentication/Authorization
app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
.SetIsOriginAllowed(origin => true) // allow any origin or allwais
.AllowCredentials());

app.UseAuthentication();
#endregion

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
