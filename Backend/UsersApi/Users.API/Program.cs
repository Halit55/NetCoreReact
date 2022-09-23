using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Users.API.AutoMappers;
using Users.Businness.Abstract;
using Users.Businness.Concreate;
using Users.DataAccess.Abstract;
using Users.DataAccess.Concreate.Adonet;
using Users.DataAccess.Concreate.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Tüm orijin lere izin ver
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddAutoMapper(typeof(UserProfile), typeof(DepartmentProfile), typeof(LoginProfile));

//Sadece json döndürmek için kullanýlýr.
builder.Services.AddControllers().AddNewtonsoftJson();

//Tanımlanan servisler
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IUserDal, EfUserDal>();
//builder.Services.AddScoped<IUserDal, AdoUserDal>(); //Ado Orm

builder.Services.AddScoped<IDepartmentService, DepartmentManager>();
builder.Services.AddScoped<IDepartmentDal, EfDepartmentDal>();
//builder.Services.AddScoped<IDepartmentDal, AdoDepartmentDal>(); Ado Orm

//Token oluþturmak için kullanýlan service
builder.Services.AddScoped<ITokenService,TokenManager>();

//Gelen http isteklerindeki JWT token bu middleware da doðrulanýr
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer =builder.Configuration["JwtIssuer"],
        ValidAudience =builder.Configuration["JwtAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSecurityKey"]))
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("corsapp");

app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();
