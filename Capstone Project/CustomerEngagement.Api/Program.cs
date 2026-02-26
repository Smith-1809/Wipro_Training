using CustomerEngagement.Application.Interfaces;
using CustomerEngagement.Application.Services;
using CustomerEngagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------
// Add Services
// ---------------------------

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ITicketService, TicketService>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

builder.Services.AddScoped<IAgentService, AgentService>();
builder.Services.AddScoped<IAgentRepository, AgentRepository>();

// ---------------------------
// Enable CORS (VERY IMPORTANT)
// ---------------------------

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// ---------------------------
// Logging
// ---------------------------

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// ---------------------------
// Development Tools
// ---------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ---------------------------
// Middleware Pipeline
// ---------------------------

app.UseMiddleware<CustomerEngagement.Api.Middlewares.ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");   // MUST be before MapControllers

app.UseAuthorization();

app.MapControllers();

app.Run();