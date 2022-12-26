
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Microsoft.Extensions.DependencyInjection;
using FicheUEAuthApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FicheUEAuthApiContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("FicheUEAuthApiContext") ?? throw new InvalidOperationException("Connection string 'FicheUEAuthApiContext' not found.")));

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Ajout de la dépendance d'auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Ajout de l'authentification
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(bearer =>
{
    bearer.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = MyConstants.SIGN_KEY,
        ValidIssuer = MyConstants.ISSUER,
        ValidAudience = MyConstants.AUDIANCE,
        ValidateLifetime = true
    };
});


// Ajout de la d�pendance de la gestion des utilisateurs
builder.Services.AddScoped<IUserManagement, UserManagement>();

// Ajout de la d�pendance de l'authentification
builder.Services.AddScoped<IAuthentification, UserAuthenticate>();


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

app.MapControllers();

app.Run();
