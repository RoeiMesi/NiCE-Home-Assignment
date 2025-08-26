using FluentValidation;
using FluentValidation.AspNetCore;
using NiCE_Home_Assignment.Models.Domain;
using NiCE_Home_Assignment.Services;
using NiCE_Home_Assignment.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<ExternalTaskService>();
builder.Services.AddSingleton<MatchUtteranceService>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<UserDetailsValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var taskDictionary = new Dictionary<string, string>
{
 { "reset password", "ResetPasswordTask" },
 { "forgot password", "ResetPasswordTask" },
 { "check order", "CheckOrderStatusTask" },
 { "track order", "CheckOrderStatusTask" }
};

builder.Services.AddSingleton<IDictionary<string, string>>(taskDictionary);

var app = builder.Build();

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

public partial class Program { }