using Microsoft.AspNetCore.OData;
using Microsoft.OpenApi.Models;
using ProgrammingOData.API.Aggregator;
using ProgrammingOData.API.Aggregator.Interfaces;
using ProgrammingOData.API.Commands;
using ProgrammingOData.API.Helpers;
using ProgrammingOData.API.Queries;
using ProgrammingOData.API.Queries.Admin;
using ProgrammingOData.Domain.Interfaces;
using ProgrammingOData.Infrastructure.Repositories;
using UseCaseCore.UseCases;

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
// Register UseCase handlers and dispatcher instead of MediatR
builder.Services.AddSingleton<UseCaseDispatcher>();

// Aggregators
builder.Services.AddScoped<IPrFrameworksAggregator, PrFrameworksAggregator>();
builder.Services.AddScoped<IPrFrameworksDescriptionsAggregator, PrFrameworksDescriptionsAggregator>();
builder.Services.AddScoped<IPrlanguagesAggregator, PrlanguagesAggregator>();
builder.Services.AddScoped<IPrlanguagesDescriptionsAggregator, PrlanguagesDescriptionsAggregator>();
builder.Services.AddScoped<ISupportedLocalesAggregator, SupportedLocalesAggregator>();
builder.Services.AddScoped<IUsersAggregator, UsersAggregator>();

// Command handlers
builder.Services.AddTransient<CreatePrFrameWorkCommandHandler>();
builder.Services.AddTransient<CreatePrFrameWorkDescCommandHandler>();
builder.Services.AddTransient<CreatePrLanguageCommandHandler>();
builder.Services.AddTransient<CreatePrLanguageDescCommandHandler>();
builder.Services.AddTransient<CreateUserCommandHandler>();
builder.Services.AddTransient<LoginUserCommandHandler>();
builder.Services.AddTransient<UserRoleCommandHandler>();
builder.Services.AddTransient<ChangePswUserCommandHandler>();
builder.Services.AddTransient<ConfirmPswUserCommandHandler>();
builder.Services.AddTransient<CreateLocaleCommandHandler>();
builder.Services.AddTransient<DeleteLocaleCommandHandler>();
builder.Services.AddTransient<UpdateLocaleCommandHandler>();
builder.Services.AddTransient<DeletePrFrameworkCommandHandler>();
builder.Services.AddTransient<DeletePrLanguageCommandHandler>();
builder.Services.AddTransient<DeletePrLanguageDescCommandHandler>();
builder.Services.AddTransient<UpdatePrFrameWorkCommandHandler>();
builder.Services.AddTransient<UpdatePrFrameWorkDescCommandHandler>();
builder.Services.AddTransient<UpdatePrLanguageCommandHandler>();
builder.Services.AddTransient<UpdatePrLanguageDescCommandHandler>();

// Query handlers
builder.Services.AddTransient<AllPrFrameworkQueryHandler>();
builder.Services.AddTransient<ByIdPrFrameworkQueryHandler>();
builder.Services.AddTransient<AllPrFrameworkDescQueryHandler>();
builder.Services.AddTransient<ByIdPrFrameworkDescriptionQueryHandler>();
builder.Services.AddTransient<AllPrLanguageQueryHandler>();
builder.Services.AddTransient<ByIdPrLanguageQueryHandler>();
builder.Services.AddTransient<AllPrLanguageDescriptionsQueryHandler>();
builder.Services.AddTransient<ByIdPrLanguageDescriptionQueryHandler>();
builder.Services.AddTransient<AllSupportedLocalesQueryHandler>();
builder.Services.AddTransient<ByIdLocaleQueryHandler>();
builder.Services.AddTransient<AllUsersQueryHandler>();
builder.Services.AddTransient<ByIdUserQueryHandler>();
builder.Services.AddTransient<UserInformationQueryHandler>();
builder.Services.AddTransient<GlobalStatsQueryHandler>();

builder.Services.AddScoped<IPRLanguageRepository, PRLenguageRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleUserRepository, RoleUserRepository>();
builder.Services.AddScoped<ISupportedLocaleRepository, SupportedLocaleRepository>();
builder.Services.AddScoped<IPRLanguageDescriptionRepository, PRLanguageDescriptionRepository>();
builder.Services.AddScoped<IPRFrameworkRepository, PRFrameworkRepository>();
builder.Services.AddScoped<IPRFrameworkDescriptionRepository, PRFrameworkDescriptionRepository>();
builder.Services.AddScoped<IStatsRepository, StatsRepository>();

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

builder.Services.AddHealthChecks().AddCheck("mysql-dapper", 
    new MySqlDapperHealthCheck(builder.Configuration.GetConnectionString("MySql")));


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

app.MapHealthChecks("/healthz", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = HealthCheckResponse.WriteResponse
});

app.Run();
