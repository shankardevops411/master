using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting; 
using Serilog;

namespace Scheduleservice.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog((context, config) =>
                {
                    if (context.Configuration.GetConnectionString("LoggerConnectionString") != null && context.Configuration["LoggerTableName"] != null)
                    {
                        var con = context.Configuration.GetConnectionString("LoggerConnectionString");
                        var tableName = context.Configuration["LoggerTableName"];

                        var sinkOptions = new Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options.SinkOptions
                        {
                            TableName = tableName
                        };

                        //Log Error and above level to DB
                        config.WriteTo.MSSqlServer(con, sinkOptions, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error);

                        //Log Info and above level to Console
                        config.WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information);
                    }
                });
    }
}
