using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using DS.Calculadora.Stub.OnStartServers;
using DS.Calculadora.Stub.OnStopServers;

namespace DS.Calculadora.WebApi
{
    class Program
    {
        public static string url;
        public static string urlServerDir;

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomaind_ProcessExit;
            Console.WriteLine(@"******* SERVIDOR CALCULADORA *********");
            IPAddress[] localIps = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (var ipAddress in localIps)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                    url = ipAddress.ToString();
            }

            var port = default(string);
            urlServerDir = default(string);
            var operacoes = default(string);
            Console.WriteLine(@"Informe porta do servidor:");
            port = Console.ReadLine();
            url = PreparaUrl(url) + ":" + port;

            Console.WriteLine(@"Informe url Diretorio Servidores: ");
            urlServerDir = Console.ReadLine();
            urlServerDir = PreparaUrl(urlServerDir);

            Console.WriteLine(@"Informe as Operações possiveis separadas por | e sem acentos:");
            Console.WriteLine(@"Ex: Soma|Subtracao|Multiplicacao");
            operacoes = Console.ReadLine();


            var config = new HttpSelfHostConfiguration(url);
            config.Routes.MapHttpRoute("API", "api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });
            
            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                OnStartCalculadoraServer.RegistraNoServerDir(urlServerDir, url, operacoes);
                Console.WriteLine(@"Online:");
                Console.WriteLine(url);
                Console.WriteLine(@"Press any key to stop");
                Console.ReadKey();
                server.CloseAsync();
            }
            Console.ReadKey();
            OnStopCalculadoraServer.RemoveServerDoServerDir(urlServerDir, url);

        }

        private static string PreparaUrl(string url)
        {
            return !url.StartsWith("http://") ? "http://" + url : url;
        }

        static void CurrentDomaind_ProcessExit(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(urlServerDir))
                OnStopCalculadoraServer.RemoveServerDoServerDir(urlServerDir, url);
        }
    }
}
