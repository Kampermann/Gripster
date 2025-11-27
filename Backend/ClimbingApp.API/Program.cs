using ClimbingApp.Model.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ClimbRepository>();
builder.Services.AddScoped<GymRepository>();
builder.Services.AddScoped<AdminRepository>();
builder.Services.AddScoped<GradeRepository>();
builder.Services.AddScoped<SessionRepository>();
builder.Services.AddScoped<SessionRouteRepository>();
builder.Services.AddScoped<UserRouteRepository>();
builder.Services.AddScoped(sp => new UserSessionRepository(sp.GetRequiredService<IConfiguration>()));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.Run();
app.Run();
