using System;
using DS.Calculadora.Stub.Commom;
using DS.Calculadora.Stub.Remote;

namespace DS.Calculadora.Stub.OnStopServers
{
    public static class OnStopCalculadoraServer
    {
        public static void RemoveServerDoServerDir(string urlServerDir, string url)
        {
            var call = new CallServer(urlServerDir);
            call.Action(Actions.Offline)
                .Params(new Tuple<string, string>("url", url))
                 .ExecuteGet();
        }
    }
}
