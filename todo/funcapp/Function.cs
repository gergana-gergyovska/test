using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using funcapp.Database;

namespace funcapp
{
    public static class Function
    {
        [FunctionName("Function")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log, ExecutionContext context)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            log.LogInformation("222");

            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            CosmosDBSettings dBSettings = new CosmosDBSettings();
            config.Bind("CosmosDb", dBSettings);

            var conn = new DocumentClientSingleton(dBSettings).Client;
        }
    }
}
