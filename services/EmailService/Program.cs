using System.Net;
using System.Net.Mail;
using Scalar.AspNetCore;
using EmailService.services.Interfaces;
using EmailService.services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IEmailService, EmailServiceHelper>();

// Add SmtpClient to the services collection
builder.Services.AddSingleton<SmtpClient>(sp => // MOVE THIS TO SERVICEHELPER
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var smtpSection = configuration.GetSection("Smtp");
    var smtpClient = new SmtpClient
    {
        Port = smtpSection.GetValue<int>("Port"),
        Host = smtpSection.GetValue<string>("Host")!,
        EnableSsl = true,
        DeliveryMethod = SmtpDeliveryMethod.Network,
        UseDefaultCredentials = false,
        Credentials = new NetworkCredential(
            smtpSection.GetValue<string>("Username"), // USE VALUE FOR FROM
            smtpSection.GetValue<string>("Password"))
    };
    return smtpClient;
});

// Add controller services
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// Map controllers
app.MapControllers();

// app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}