using PasswordManagerService.Repository.Models;
using PasswordManagerService.Repository;
using PasswordManagerService.Domain;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<PasswordManagerContext>(ServiceLifetime.Transient);
builder.Services.AddScoped<IPasswordManagerProcessor, PasswordManagerProcessor>();
builder.Services.AddTransient<IPasswordManagerCommandRepository, PasswordManagerCommandRepository>();
builder.Services.AddTransient<IPasswordManagerQueryRepository, PasswordManagerQueryRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});



builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Password Manager Service",
        Description = "API for Password Manager"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Password Manager Service");
    });
}

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
