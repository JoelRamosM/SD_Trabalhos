using System;
using System.Collections.Generic;
using DS.Calculadora.Stub.Commom;
using DS.Calculadora.Stub.Remote;

namespace DS.Calculadora.Stub.OnStartServers
{
    public static class OnStartCalculadoraServer
    {
        public static void RegistraNoServerDir(string urlServerDir, string urlCalculadoraServer, string operacoes)
        {
            var call = new CallServer(urlServerDir);
            call.Action(Actions.Online)
                .Params(new Tuple<string, string>("url", urlCalculadoraServer),
                    new Tuple<string, string>("actions", operacoes))
                 .ExecuteGet();
        }
    }
}
