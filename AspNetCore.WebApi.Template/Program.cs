using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Elasticsearch;

namespace AspNetCore.WebApi.Template
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(new CustomElasticsearchJsonFormatter(), "logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                Log.Information("Starting up...");

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Api starting up failed.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private class CustomElasticsearchJsonFormatter : ElasticsearchJsonFormatter
        {
            protected override void WriteTimestamp(DateTimeOffset timestamp, ref string delim, TextWriter output)
            {
                // default field name @timestamp clashes with Fluentd
                WriteJsonProperty("source-timestamp", timestamp, ref delim, output);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{env.EnvironmentName.ToLower()}.json", true);
                    config.AddEnvironmentVariables();

                })
                .UseSerilog();
    }
}
