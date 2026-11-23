using ClinicBook.Api.Endpoints;
using ClinicBook.Application;
using ClinicBook.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// ---------- Services (DI container) ----------
builder.Services.AddOpenApi();          // OpenAPI 3.1 document at /openapi/v1.json
builder.Services.AddProblemDetails();   // RFC 9457 error responses everywhere
builder.Services.AddHealthChecks();     // Phase 6 adds a database check tagged "ready"

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// ---------- Middleware pipeline (order matters) ----------
app.UseExceptionHandler();   // unhandled exceptions -> 500 ProblemDetails
app.UseStatusCodePages();    // empty 404/405 etc. -> ProblemDetails body

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();   // UI at /scalar/v1
}

app.UseHttpsRedirection();

// ---------- Endpoints ----------
app.MapHealthEndpoints();
app.MapSystemEndpoints();

app.Run();

// Lets WebApplicationFactory<Program> in the test project see this class.
public partial class Program { }
