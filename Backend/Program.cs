using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
{
    throw new ArgumentNullException("Jwt:Key", "La clé JWT est absente dans appsettings.json !");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });


// Ajoute le service CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5025")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

// Utilise la chaîne de connexion de la base de données Bonita
builder.Services.AddDbContext<BonitaDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BonitaConnection")));

// Utilise la chaîne de connexion de la base de données Importation
builder.Services.AddDbContext<ImportationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ImportationConnection")));
/*
// Configure le DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BonitaConnection")));

*/

var app = builder.Build();

// Active Swagger en mode développement
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Active CORS AVANT les autres middlewares
app.UseCors("AllowFrontend");

//  **Correction : ordre des middlewares**
app.UseAuthentication();  //  Mettre Authentication avant Authorization
app.UseAuthorization();

// Mappe les routes vers les contrôleurs
app.MapControllers();

app.Run();
