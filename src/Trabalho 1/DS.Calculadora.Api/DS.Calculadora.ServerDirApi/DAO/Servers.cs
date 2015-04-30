using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using DS.Calculadora.ServerDirApi.DAO.Commom;
using DS.Calculadora.ServerDirApi.Models;
using DS.Calculadora.Stub.Remote;

namespace DS.Calculadora.ServerDirApi.DAO
{
    public class Servers : IServers
    {
        public static List<CalculadoraPath> Servidores;
        public CalculadoraPath Add(CalculadoraPath path)
        {
            Servidores.Add(path);
            return path;
        }

        public void Remove(string url)
        {
            Servidores.Remove(Servidores.Find(c => c.Url == url));
        }

        public CalculadoraPath GetServer(string operacao)
        {
            var servers = Servidores.Where(c => c.Operacoes.ToLower().Contains(operacao.ToLower())).ToList();
            foreach (var server in servers)
            {
                try
                {
                    new CallServer(server.Url).ServerAlive();
                    return server;
                }
                catch (WebException) { Servidores.Remove(server); }
            }
            return null;
        }
    }
}
