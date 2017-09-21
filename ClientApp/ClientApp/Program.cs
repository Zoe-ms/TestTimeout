using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTimeout;

namespace ClientApp
{
    class Program
    {
        private static Uri DummyRPRootUri = new Uri(@"http://dummyrp.cloudapp.net/");

        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();

            using (var client = CreateClient())
            {
                Console.WriteLine("GET 200...");
                // GET 200
                watch.Start();
                client.Service.OK();
                watch.Stop();
                Console.WriteLine("GET 200 elapsed time: " + watch.ElapsedMilliseconds + "ms");

                Console.WriteLine();

                // GET 400
                Console.WriteLine("GET 400...");
                try
                {
                    watch.Start();
                    client.Service.BadRequest();
                }
                catch (Exception ex)
                {
                    watch.Stop();
                    Console.WriteLine("GET 400 returns exception: " + ex.ToString());
                    Console.WriteLine("GET 400 elapsed time: " + watch.ElapsedMilliseconds + "ms");
                }

                Console.WriteLine();

                // GET 500
                Console.WriteLine("GET 500...");
                try
                {
                    watch.Start();
                    client.Service.InternalError();
                }
                catch (Exception ex)
                {
                    watch.Stop();
                    Console.WriteLine("GET 500 returns exception: " + ex.ToString());
                    Console.WriteLine("GET 500 elapsed time: " + watch.ElapsedMilliseconds + "ms");
                }
            }

            Console.ReadLine();
        }

        private static DummyBackendServiceClient CreateClient()
        {
            return new DummyBackendServiceClient(DummyRPRootUri, AnonymousCredential.GetInstance());
        }
    }

    public sealed class AnonymousCredential : ServiceClientCredentials
    {
        private static AnonymousCredential _instance;
        public static AnonymousCredential GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AnonymousCredential();
            }
            return _instance;
        }
        private AnonymousCredential() { }
    }
}
