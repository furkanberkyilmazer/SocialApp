using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialApp.API.Helpers;
using SocialApp.DataAccessLayer.Concrete;
using SocialApp.EntityLayer.Concrete;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
//builder.Services.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));
//builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
//builder.Services.AddScoped<IUserService,UserService>();
//builder.Services.AddScoped<IUserDal, EfUserDal>();
//autofac ekledim altta
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new DalServiceModule()));


builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<SocialContext>(x =>
{
    x.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(SocialContext)).GetName().Name);
    });

});
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<SocialContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true; //@ , _ gibi deðerler olmasý gerekir
    options.Password.RequiredLength = 6;


    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5; //5 kere yanlýþ þifre girere hesabý beþ dakika kitle
    options.Lockout.AllowedForNewUsers = true;

    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";  //username de kullanýmlasýna izin verilen karakterler.
    options.User.RequireUniqueEmail = true; //Her kullanýcýnýn farklý emaili olmalý.

});




//Token için
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.
        Configuration.GetSection("AppSettings:Secret").Value)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});



var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
   


    
}
else
{
    app.UseExceptionHandler(appError => {

        appError.Run(async context => {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var exception = context.Features.Get<IExceptionHandlerFeature>();
            if (exception != null)
            {
                // loglama=> nlog,elmah
                await context.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = exception.Error.Message
                }.ToString());
            }
        });

    });
}

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); /* Api eriþimi için*/

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
