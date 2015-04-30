using System;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using DS.Calculadora.Stub.BO;
using DS.Calculadora.Stub.Model;
using Newtonsoft.Json;

namespace DS.Calculadora.WebApi.Controllers
{
    public class CalculadoraController : ApiController
    {
        private readonly CalculadoraServer calculadora;

        public CalculadoraController()
        {
            this.calculadora = new CalculadoraServer();
        }

        [HttpGet]
        public bool Alive()
        {
            return true;
        }

        [HttpGet]
        public JsonResult<CalculadoraResponse> Soma(long x, long y)
        {
            var valor = calculadora.Soma(x, y);
            var result = PreparaResult(valor);
            return MakeResult(result);
        }
        private JsonResult<CalculadoraResponse> MakeResult(CalculadoraResponse calculadoraResponse)
        {
            return new JsonResult<CalculadoraResponse>(
                calculadoraResponse,
                new JsonSerializerSettings(),
                Encoding.UTF8, this);
        }
        [HttpGet]
        public JsonResult<CalculadoraResponse> Subtracao(long x, long y)
        {
            var valor = calculadora.Subtracao(x, y);
            return MakeResult(PreparaResult(valor));
        }
        [HttpGet]
        public JsonResult<CalculadoraResponse> Multiplicacao(long x, long y)
        {
            var valor = calculadora.Multiplicacao(x, y);
            return MakeResult(PreparaResult(valor));
        }
        [HttpGet]
        public JsonResult<CalculadoraResponse> Divisao(long x, long y)
        {
            try
            {
                var valor = calculadora.Divisao(x, y);
                return MakeResult(PreparaResult(valor));
            }
            catch (Exception e)
            {
                return MakeResult(PreparaExceptionResult(e));
            }
        }

        private CalculadoraResponse PreparaExceptionResult(Exception exception)
        {
            return new CalculadoraResponse
            {
                Status = TipoResponse.Erro,
                Message = exception.Message
            };
        }

        private static CalculadoraResponse PreparaResult(double valor)
        {
            var result = new CalculadoraResponse
            {
                Status = TipoResponse.Result,
                Result = valor
            };
            return result;
        }
    }
}
