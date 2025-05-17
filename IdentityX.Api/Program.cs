using EntityFramework.Exceptions.SqlServer;
using IdentityX.Api.Middlewares;
using IdentityX.BusinessLogic.ServiceContracts;
using IdentityX.BusinessLogic.Services;
using IdentityX.DataAccess.DBContext;
using IdentityX.DataAccess.Identity;
using IdentityX.DataAccess.Repository;
using IdentityX.DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
    {
        options.Password.RequiredLength = 5;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = true;
        options.Password.RequireDigit = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
    .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();


builder.Services.AddTransient<IApplicationService, ApplicationService>();
builder.Services.AddTransient<IApplicationRepository, ApplicationRepository>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();