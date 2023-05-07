using Microsoft.Extensions.Logging.Configuration;
using Serilog;
using SerilogDemoSEQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog();

var config = builder.Configuration.AddJsonFile("appsettings.json").Build();
var seriConfigDTO = new SeriConfigDTO();
config.GetSection("Serilog").Bind(seriConfigDTO);

//Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();
//Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).WriteTo.Seq("http://localhost:8088").CreateLogger();
//Log.Logger = new LoggerConfiguration().WriteTo.Seq("http://localhost:8088", Serilog.Events.LogEventLevel.Information).CreateLogger();
//Log.Logger = new LoggerConfiguration().WriteTo.File(@"C:\Logs\SerilogDemoSEQ\AppLogs\log.txt", rollingInterval: RollingInterval.Day).CreateLogger();
//Log.Logger = new LoggerConfiguration().WriteTo.File(config["SerilogPath"], rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, fileSizeLimitBytes: 123456).CreateLogger();
//Log.Logger = new LoggerConfiguration().WriteTo.File(seriConfigDTO.WriteTo[1].Args.path, rollingInterval: RollingInterval.Day).CreateLogger();
//Log.Logger = new LoggerConfiguration().MinimumLevel.Override("Warning", Serilog.Events.LogEventLevel.Information).WriteTo.File(seriConfigDTO.WriteTo[1].Args.path, rollingInterval: RollingInterval.Day).CreateLogger();
//Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
//                .ReadFrom.Configuration(config)
//                .WriteTo.File(config["SerilogPath"], rollingInterval: RollingInterval.Day).CreateLogger();

//SINGLE FILE, SINGLE ENTRY ON SEQ......ADDS JARGONS
//Log.Logger = new LoggerConfiguration()
//    .WriteTo.Console()
//    .WriteTo.File("C:\\Logs\\SerilogDemoSEQ\\AppLogs\\log-.txt", rollingInterval: RollingInterval.Day,).WriteTo.Seq("http://localhost:8088", Serilog.Events.LogEventLevel.Information)
//    .CreateLogger();
//    .CreateLogger();

//DOUBLE FILES, SINGLE ENTRY ON SEQ......NO JARGONS
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config)
    .WriteTo.File("C:\\Logs\\SerilogDemoSEQ\\AppLogs\\log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

//SINGLE FILE, NO ENTRY ON SEQ......ADDS JARGONS 
//Log.Logger = new LoggerConfiguration()
//    .WriteTo.File("C:\\Logs\\SerilogDemoSEQ\\AppLogs\\log-.txt", rollingInterval: RollingInterval.Day)
//    .CreateLogger();

try
{
    Log.Information("SeriloggDemoSEQ - Application Starting Up");
    var txtx = seriConfigDTO.WriteTo[9].Args.path;
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseSerilogRequestLogging();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
    Log.Information("SeriloggDemoSEQ - Application started successfully");
}
catch (Exception ex)
{
    Log.Fatal(ex, "SeriloggDemoSEQ - The application failed to start correctly");
}
finally
{
    Log.CloseAndFlush();
}
