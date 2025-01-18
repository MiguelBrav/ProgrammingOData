using Microsoft.AspNetCore.OData;
using ProgrammingOData.API.Helpers;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddMediatR(a => a.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddScoped<IPRLanguageRepository, PRLenguageRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleUserRepository, RoleUserRepository>();
builder.Services.AddScoped<ISupportedLocaleRepository, SupportedLocaleRepository>();
builder.Services.AddScoped<IPRLanguageDescriptionRepository, PRLanguageDescriptionRepository>();

builder.Services.AddScoped<BasicAuthFilter>();
builder.Services.AddControllers().AddOData(opt =>
{
    opt.Select().Filter().OrderBy().SetMaxTop(100).SkipToken();
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
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
