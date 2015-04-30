using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using DS.Calculadora.Stub.Commom;
using DS.Calculadora.Stub.Commom.Exceptions;
using DS.Calculadora.Stub.Model;
using DS.Calculadora.Stub.Remote;

namespace DS.Calculadora.Stub.BO
{
    public class CalculadoraCliente : CalculadoraClienteBase, ICalculadora
    {
        public CalculadoraCliente(string urlServer)
            : base(urlServer) { }

        public double Soma(long x, long y)
        {

            var urlOper = GetServer("Soma");
            ValidaUrl(urlOper);

            var callCalc = new CallServer(urlOper);
            var result = callCalc.Action(Actions.Soma)
                            .Params(new Tuple<string, string>("x", x.ToString()),
                                    new Tuple<string, string>("y", y.ToString()))
                            .ExecuteGet();
            return GetResult(result);
        }

        private static void ValidaUrl(string urlOper)
        {
            if (string.IsNullOrEmpty(urlOper))
                throw new Exception("Calculadora não encontrada");
        }

        private string GetServer(string oper)
        {
            var result = this.CallServer.Action(Actions.GetServer)
                .Params(new Tuple<string, string>("operacao", oper)).ExecuteGet();
            if (result.Status == TipoResponse.Proxy)
                return result.Url;
            else
            {
                throw new ServidorNaoEncontradoException(result.Message);
            }

        }

        public double Subtracao(long x, long y)
        {
            var urlOper = GetServer("Subtracao");
            ValidaUrl(urlOper);
            var callCalc = new CallServer(urlOper);
            var result = callCalc.Action(Actions.Subtracao)
                            .Params(new Tuple<string, string>("x", x.ToString()),
                                    new Tuple<string, string>("y", y.ToString()))
                            .ExecuteGet();
            return GetResult(result);
        }

        public double Multiplicacao(long x, long y)
        {
            var urlOper = GetServer("Multiplicacao");
            ValidaUrl(urlOper);
            var callCalc = new CallServer(urlOper);
            var result = callCalc.Action(Actions.Multiplicacao)
                               .Params(new Tuple<string, string>("x", x.ToString()),
                                       new Tuple<string, string>("y", y.ToString()))
                               .ExecuteGet();
            return GetResult(result);
        }

        public double Divisao(long x, long y)
        {
            var urlOper = GetServer("Divisao");
            ValidaUrl(urlOper);
            var callCalc = new CallServer(urlOper);
            var result = callCalc.Action(Actions.Divisao)
                               .Params(new Tuple<string, string>("x", x.ToString()),
                                       new Tuple<string, string>("y", y.ToString()))
                               .ExecuteGet();
            return GetResult(result);
        }
        private static double GetResult(CalculadoraResponse result)
        {
            if (result.Status == TipoResponse.Result)
                return result.Result;

            throw new Exception(result.Message);
        }
    }
}
