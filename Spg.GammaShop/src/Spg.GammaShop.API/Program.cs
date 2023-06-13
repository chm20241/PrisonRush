using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Spg.GammaShop.API.Helper;
using Spg.GammaShop.Application.Services;
using Spg.GammaShop.Application.Validators;
using Spg.GammaShop.DbExtentions;
using Spg.GammaShop.Domain.DTO;
using Spg.GammaShop.Domain.Interfaces.ProductServiceInterfaces;
using Spg.GammaShop.Domain.Interfaces.UserInterfaces;
using Spg.GammaShop.Domain.Interfaces.UserMailConfirmInterface;
using Spg.GammaShop.Domain.Models;
using Spg.GammaShop.Infrastructure;
using Spg.GammaShop.Repository2.Repositories;
using Spg.GammaShop.ServiceExtentions;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);


string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add Transient for Services and Repos
builder.Services.AddAllTransient();

builder.Services.AddFluentValidationAutoValidation();

//DB
builder.Services.ConfigureSQLite(connectionString);
// Add services to the container.

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
    s.ResolveConflictingActions(apiDescriptions => apiDescriptions.First())
    );

// NuGet: Microsoft.AspNetCore.Mvc.Versioning
builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(2, 0);
    o.ReportApiVersions = true;
    o.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver"));
});

builder.Services.AddVersionedApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services.AddSwaggerGen(s =>
{

    s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "GammaShop Shop - v1",
        Description = "Description about GammaShop",
        Contact = new OpenApiContact()
        {
            Name = "Bernd Chmelik und Marvin Kantusch",
            Email = "chm20241@spengergasse.at",
            Url = new Uri("http://www.spengergasse.at")
        },

        License = new OpenApiLicense()
        {
            Name = "Spenger-Licence",
            Url = new Uri("http://www.spengergasse.at/licence")
        },
        Version = "v1"
    });

    s.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "GammaShop Shop - v2",
        Description = "Description about GammaShop",
        Contact = new OpenApiContact()
        {
            Name = "Bernd Chmelik und Marvin Kantusch",
            Email = "chm20241@spengergasse.at",
            Url = new Uri("http://www.spengergasse.at")
        },

        License = new OpenApiLicense()
        {
            Name = "Spenger-Licence",
            Url = new Uri("http://www.spengergasse.at/licence")
        },
        Version = "v2"
    });

});




builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "myAllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("https://localhost:7058");
    });
});

string jwtSecret = builder.Configuration["AppSettings:Secret"] ?? AuthService.GenerateRandom(1024);

//Authorizatio
builder.Services.AddJwtAuthentication(jwtSecret, setDefault: false);
builder.Services.AddCookieAuthentication(setDefault: true);

//Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOrAdmin", policy =>
    {
        policy.Requirements.Add(new CustomAuthorizationRequirement("User", "Admin"));
    });

    options.AddPolicy("SalesmanOrAdmin", policy =>
    {
        policy.Requirements.Add(new CustomAuthorizationRequirement("Salesman", "Admin"));
    });

    options.AddPolicy("UserOrSalesmanOrAdmin", policy =>
    {
        policy.Requirements.Add(new CustomAuthorizationRequirement("User", "Salesman", "Admin"));
    });
});

builder.Services.AddTransient<IAuthorizationHandler, CustomAuthorizationHandler>();



//AuthService

builder.Services.AddTransient<AuthService>(services =>
{
    var userRepository = services.GetRequiredService<IUserRepository>();
    return new AuthService(jwtSecret, userRepository);
    }
);

var app = builder.Build();



// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x => {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        x.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.UseCors("myAllowSpecificOrigins");

app.MapControllers();
app.MapGet("/api", () =>
{
    return "Hello world";
}
);


app.Run();

