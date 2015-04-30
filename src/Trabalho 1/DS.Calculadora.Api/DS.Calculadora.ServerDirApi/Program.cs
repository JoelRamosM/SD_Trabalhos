using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Web.Http;
using System.Web.Http.SelfHost;
using DS.Calculadora.ServerDirApi.DAO;
using DS.Calculadora.ServerDirApi.Models;

namespace DS.Calculadora.ServerDirApi
{
    class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine(@"********* SERVIDOR DIRETORIO ***********");
            Servers.Servidores = new List<CalculadoraPath>();
            var url = default(string);
            IPAddress[] localIps = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (var ipAddress in localIps)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                    url = ipAddress.ToString();
            }
            var port = default(string);
            Console.WriteLine(@"Informe porta do servidor:");
            port = Console.ReadLine();
            url = PreparaUrl(url);
            url = string.Format(url + ":{0}", port);
            var config = new HttpSelfHostConfiguration(url);
            config.Routes.MapHttpRoute("API", "api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });
            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                Console.WriteLine(@"Online:");
                Console.WriteLine(url);
                Console.WriteLine(@"Press any key to stop");
                Console.ReadKey();
                server.CloseAsync();
            }



        }
        private static string PreparaUrl(string urlServerDir)
        {
            return !urlServerDir.StartsWith("http://") ? urlServerDir = "http://" + urlServerDir : urlServerDir;
        }
    }
}
