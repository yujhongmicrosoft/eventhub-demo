using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System.Configuration;

namespace TrafficSensorApp
{
    class Program
    {
        static string eventHubName = "trafficeventhub";
        static string connection = ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"];
        static int Main(string[] args)
        {
            // Test if input arguments were supplied:
            if (args.Length < 3)
            {
                System.Console.WriteLine("Please enter Arguments: Location(string), AvgSpeed(int), NumofCars(int)");
                return 1;
            }

            int loc;
            int avgspeed;
            int numcars;
            bool loctest = int.TryParse(args[0], out loc);
            bool speedtest = int.TryParse(args[1], out avgspeed);
            bool numtest = int.TryParse(args[2], out numcars);
            if (loctest || !speedtest || !numtest)
            {
                System.Console.WriteLine("Please enter correct type for arguments");
                return 1;
            }
            var message = new TrafficMessage();
            message.location = args[0];
            message.averageSpeed = avgspeed;
            message.numberofCars = numcars;
            var ehMessage = JsonConvert.SerializeObject(message);
            var eventHubClient = EventHubClient.CreateFromConnectionString(connection, eventHubName);
            while (true)
            {
                try
                {
                    eventHubClient.Send(new EventData(Encoding.UTF8.GetBytes(ehMessage)));
                    Console.WriteLine("Successfully sent message");

                }
                catch (Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0} > Exception: {1}", DateTime.Now, exception.Message);
                    Console.ResetColor();
                }
                Thread.Sleep(1000);
                System.Diagnostics.Process.Start("TrafficSensorApp.exe");
                Environment.Exit(0);
                //Thread.Sleep(3000);
            }
         
        }
    }
}
