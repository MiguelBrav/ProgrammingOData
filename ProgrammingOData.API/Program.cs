using Microsoft.AspNetCore.OData;
using Microsoft.OpenApi.Models;
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

builder.Services.AddScoped<BasicAdminAuthFilter>();
builder.Services.AddScoped<BasicEditorAuthFilter>();
builder.Services.AddScoped<BasicDefaultAuthFilter>();

builder.Services.AddControllers().AddOData(opt =>
{
    opt.Select().Filter().OrderBy().SetMaxTop(100).SkipToken();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProgrammingOData.API", Version = "v1" });

    c.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Basic",
        In = ParameterLocation.Header,
        Description = "Authorization: Basic <base64>"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Basic"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddHttpContextAccessor();

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
