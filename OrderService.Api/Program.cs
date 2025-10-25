using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Api;
using OrderService.Application.Orders.Commands;
using OrderService.Application.Orders.Queries;
using OrderService.Infrastructure;
using Serilog;


// Create a WebApplicationBuilder instance

var builder = WebApplication.CreateBuilder(args); // Initializes the web application builder, DI container, configuration, logging, and web server

// Serilog // Configure Serilog logging
// shows the logs in console and files with structured logging
builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration)); // Uses Serilog for logging, reads configuration from appsettings.json, allows structured logs to console/files

// Register EF Core DbContext in the dependency injection container
// Registers OrderDbContext for DI, tells EF Core to use SQLite database stored in 'orders.db'
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlServerConnection")
    )
);


// Register MediatR handlers in the DI container
//// Scans assembly containing CreateOrderCommand for MediatR handlers and registers them
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<CreateOrderCommand>();
});


// Register controllers in the DI container
builder.Services.AddControllers();

// Register Swagger services in the DI container
builder.Services.AddEndpointsApiExplorer();

// Enable Swagger/OpenAPI support
builder.Services.AddSwaggerGen();

// Build the WebApplication instance
var app = builder.Build(); // Compiles builder into a web app, ready to configure middleware, endpoints, and run

// Just run one time data migration at startup..
//DataMigrator.Migrate();

// Use Swagger UI only in Development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();  // This is used to map controller routes to endpoints for e.g HTTP requests 


app.Run();   // Runs Kestrel web server, listens for HTTP requests
