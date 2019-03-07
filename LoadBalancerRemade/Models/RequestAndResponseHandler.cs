using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
namespace LoadBalancerRemade.Models
{
    public class RequestAndResponseHandler
    {
        private static readonly HttpClient client = new HttpClient();
        static Queue<string> Servers = new Queue<string>(new[] { "http://localhost:5005/api/primenum/", "http://localhost:5006/api/primenum/" });
        public async Task<long> sendRequestToService(long num1, long num2)
        {


            // Get the next one up
            String currentServer = Servers.Dequeue();

            // Adds the server to the end of the queue
            Servers.Enqueue(currentServer);

            Console.WriteLine("HALLO I AM A UNICORN");
            // Gives back a response from the current server requested to
            var response = await client.GetStringAsync(currentServer + num1 + "/" + num2);
            // Console for debug
            String time = DateTime.Now.ToString("h:mm:ss tt");

            Debug.WriteLine(currentServer + " " + time);


            return Convert.ToInt64(response);

        }
    }

}
