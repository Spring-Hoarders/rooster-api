using Rooster.Application;
using Rooster.Application.Common;
using Rooster.Application.Common.Common;
using Rooster.Application.Common.Common.Data;
using Rooster.Application.Common.Data;
using Rooster.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

var settings = Settings.GetSettingsFromEnvironment();
builder.Services
    .AddApplication(settings)
    .AddInfrastructure(settings);

builder.Services.AddMvc();
builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
));


var app = builder.Build();

using var scope = app.Services.CreateScope();

scope.ServiceProvider.GetRequiredService<IDataMigrator>().Migrate();
scope.ServiceProvider.GetRequiredService<IDataSeeder>().SeedAsync().Wait();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();