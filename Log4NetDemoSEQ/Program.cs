using log4net;
using Log4NetDemoSEQ;

var builder = WebApplication.CreateBuilder(args);
ILog logger = LogManager.GetLogger(typeof(LoggerManager));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ILoggerManager, LoggerManager>();



try
{
    logger.InfoFormat("Log4NetDemoSEQ - Application Starting Up");
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    //app.UseSerilogRequestLogging();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
    logger.InfoFormat("Log4NetDemoSEQ - Application started successfully");
}
catch (Exception ex)
{
    logger.Error("Log4NetDemoSEQ - The application failed to start correctly", ex);
}
finally
{
}
