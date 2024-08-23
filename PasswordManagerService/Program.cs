using PasswordManagerService.Repository.Models;
using PasswordManagerService.Repository;
using PasswordManagerService.Domain;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PasswordManagerContext>(ServiceLifetime.Transient);
builder.Services.AddScoped<IPasswordManagerProcessor, PasswordManagerProcessor>();
builder.Services.AddTransient<IPasswordManagerCommandRepository, PasswordManagerCommandRepository>();
builder.Services.AddTransient<IPasswordManagerQueryRepository, PasswordManagerQueryRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
