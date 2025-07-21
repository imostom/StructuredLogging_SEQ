using Microsoft.Extensions.Logging.Configuration;
using Serilog;
using Serilog.Formatting.Json;
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

#region
//SINGLE FILE, SINGLE ENTRY ON SEQ......ADDS JARGONS
//Log.Logger = new LoggerConfiguration()
//    .WriteTo.Console()
//    .WriteTo.File("C:\\Logs\\SerilogDemoSEQ\\AppLogs\\log-.txt", rollingInterval: RollingInterval.Day,).WriteTo.Seq("http://localhost:8088", Serilog.Events.LogEventLevel.Information)
//    .CreateLogger();
//    .CreateLogger();
#endregion

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .CreateLogger();

//DOUBLE FILES, SINGLE ENTRY ON SEQ......NO JARGONS
//Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config)
//    .WriteTo.File(seriConfigDTO.WriteTo[1].Args.path, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, fileSizeLimitBytes: 52428800)
//    .WriteTo.File(new JsonFormatter(), seriConfigDTO.WriteTo[2].Args.path, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, fileSizeLimitBytes: 52428800)
//    //.WriteTo.File("C:\\Logs\\SEQ\\SerilogDemoSEQ\\log-.txt", rollingInterval: RollingInterval.Day)
//    .WriteTo.Seq("http://localhost:5341")
//    .CreateLogger();

//SINGLE FILE, ENTRY ON SEQ, JSON FILE ISN'T RIGHT.....ADDS JARGONS 
//Log.Logger = new LoggerConfiguration()
//    .WriteTo.File(seriConfigDTO.WriteTo[1].Args.path, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, fileSizeLimitBytes: 52428800)
//    .WriteTo.File(seriConfigDTO.WriteTo[2].Args.path, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, fileSizeLimitBytes: 52428800)
//    .WriteTo.Seq("http://localhost:5341")
//    .CreateLogger();

try
{
    Log.Information("SeriloggDemoSEQ - Application Starting Up");
    var txtx = seriConfigDTO.WriteTo[3].Args.path;
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
