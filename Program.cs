using System;
using Microsoft.Extensions.Configuration;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            
            var groupId = args[0] ?? "1";
            Console.WriteLine($"Consumer of groupId {groupId} was started");

            var consumer = new Consumers.Consumer(configuration, groupId);
            while (true)
            {
                consumer.Consume("Events");
            }
        }
    }
}
