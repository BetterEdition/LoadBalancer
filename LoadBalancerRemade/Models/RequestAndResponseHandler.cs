using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using System.IO;

namespace LoadBalancerRemade.Models
{
    public class RequestAndResponseHandler
    {
        private static readonly HttpClient client = new HttpClient();

        static Queue<string> Servers = new Queue<string>(new[] { "http://localhost:5005/api/primenum/",
            "http://localhost:5006/api/primenum/" });
            
        string file = "C:\\Users\\Jespe\\Desktop\\monitor.txt";

        public async Task<long> GetNumberOfPrimes(long num1, long num2)
        {
            // Time for monitoring
            String timeRequest = DateTime.Now.ToString("h:mm:ss tt");
            // Get the next one up
            String currentServer = Servers.Dequeue();

            // For monitoring
            File.AppendAllText(file, "Request " + "currentServer: " + currentServer + " " + timeRequest + Environment.NewLine);
            
            // Adds the server to the end of the queue
            Servers.Enqueue(currentServer);

            // Gives back a response from the current server requested to
            var response = await client.GetStringAsync(currentServer + num1 + "/" + num2);

            String timeResponse = DateTime.Now.ToString("h:mm:ss tt");
            // For monitoring
            File.AppendAllText(file, "Response " + "currentServer: " + currentServer + " " + timeResponse + Environment.NewLine);

            return Convert.ToInt64(response);

        }
       
        public async Task<bool> GetIsPrime(long num)
        {
            String currentServer = Servers.Dequeue();
            Servers.Enqueue(currentServer);

            var response = await client.GetStringAsync(currentServer + num);
         

            if (response == "true")
            
                return true;
            
            else
            
                return false;
            
        }


    }

}
