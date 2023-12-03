using EmployeeHubAPI.Data;
using EmployeeHubAPI.Entities;
using EmployeeHubAPI.Exceptions;
using EmployeeHubAPI.Extensions;
using EmployeeHubAPI.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpContextAccessor();

// Identity
builder.Services.AddAuthorization();
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Admin", policy =>
    {
        policy.RequireRole("Admin");
    })
    .AddPolicy("Supervisor", policy =>
    {
        policy.RequireRole("Admin", "Supervisor");
    })
    .AddPolicy("Users", policy =>
    {
        policy.RequireRole("Admin", "Supervisor", "User");
    });



builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DataContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
    options.Lockout.MaxFailedAccessAttempts = 2;
    options.Lockout.AllowedForNewUsers = true;

    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;

    options.User.RequireUniqueEmail = true;
});
// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = """Standard Authorization heading - Example: "bearer {token}" """,
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.OperationFilter<SecurityRequirementsOperationFilter>();
});
// Helpers
builder.Services.AddAutoMapper(typeof(Program));

// Exceptions
builder.Services.AddExceptionHandler<ExceptionHandler>();

builder.Services.AddServices();



var app = builder.Build();

app.UseExceptionHandler(_ => { });

app.MapIdentityApi<ApplicationUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetService<Seeder>();

    if (seeder != null)
        await seeder.Seed();
}



app.UseAuthorization();

app.MapControllers();

app.Run();
