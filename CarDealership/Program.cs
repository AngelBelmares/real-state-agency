using RealStateAgency.Configurations;
using RealStateAgency.Controllers;
using RealStateAgency.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddScoped<HousesController>();
builder.Services.AddScoped<UsersController>();

// Custom
builder.Services.AddJwtAuth(builder.Configuration);
builder.Services.AddDbContextServices(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));

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
