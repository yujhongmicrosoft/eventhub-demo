using McMaster.Extensions.CommandLineUtils;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace TrafficManagerCli
{
    class Program
    {
        static string eventHubName = "trafficeventhub";
        //static string connection = ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"];

        static int Main(string[] args)
        {
            var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var connection = configuration.GetSection("ServiceBus").Value;
            var app = new CommandLineApplication();

            app.HelpOption();

            var optionLocation = app.Option("-l|--location <LOCATION>", "The location", CommandOptionType.SingleValue).IsRequired();
            var optionSpeed = app.Option("-s|--speed <SPEED>", "Average speed", CommandOptionType.SingleValue).IsRequired()
                .Accepts(v => v.RegularExpression(@"\d+", "Speed must be an integer."));
            var optionNum = app.Option("-n|--number <NUMBER>", "Number of cars", CommandOptionType.SingleValue).IsRequired()
                .Accepts(v => v.RegularExpression(@"\d+", "Number of cars must be an integer."));

            app.OnExecute(async () =>
            {
                var location = optionLocation.Value();
                var speed = optionSpeed.Value();
                var num = optionNum.Value();

                Console.WriteLine($"Hello {location}, speed: {speed}, number of cars: {num}!");

                var messageSpeed = new TrafficMessage
                {
                    location = location,
                    sensorType = "averageSpeed",
                    sensorValue = int.Parse(speed),
                    sensorId = location[0] + "-123"
                };

                var messageCars = new TrafficMessage
                {
                    location = location,
                    sensorType = "numberofCars",
                    sensorValue = int.Parse(num),
                    sensorId = messageSpeed.sensorId
                };

                var ehMessageSpeed = JsonConvert.SerializeObject(messageSpeed);
                var ehMessageCars = JsonConvert.SerializeObject(messageCars);
                var connectionStringBuilder = new EventHubsConnectionStringBuilder(connection)
                {
                    EntityPath = eventHubName
                };
                var eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());

                while (true)
                {
                    Console.WriteLine(messageSpeed.sensorId);
                    await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(ehMessageSpeed)));
                    await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(ehMessageCars)));
                    Console.WriteLine("Successfully sent message");

                    Thread.Sleep(1000);
                }
            });

            try
            {
                return app.Execute(args);
            }
            catch (CommandParsingException ex)
            {
                Console.WriteLine(ex.Message);
                app.ShowHelp();
                return 0;
            }

        }
    }
}
