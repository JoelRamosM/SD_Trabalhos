using System.Collections.Generic;
using DS.Calculadora.Stub.Remote;

namespace DS.Calculadora.Stub.Commom
{
    public abstract class CalculadoraClienteBase
    {
        protected readonly string urlServer;
        protected CallServer CallServer;

        protected CalculadoraClienteBase(string urlServer)
        {
            this.urlServer = urlServer;
            this.CallServer = new CallServer(urlServer);
        }
    }
}
