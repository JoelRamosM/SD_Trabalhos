using System;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using DS.Calculadora.ServerDirApi.DAO;
using DS.Calculadora.ServerDirApi.DAO.Commom;
using DS.Calculadora.ServerDirApi.Models;
using DS.Calculadora.Stub.Commom.Exceptions;
using DS.Calculadora.Stub.Model;
using Newtonsoft.Json;

namespace DS.Calculadora.ServerDirApi.Controllers
{
    public class ServerController : ApiController
    {
        private readonly IServers todosServers;
        public ServerController()
        {
            todosServers = new Servers();
        }
        [HttpGet]
        public JsonResult<CalculadoraResponse> Inclui(string url, string actions)
        {
            var server = new CalculadoraPath() { Url = url, Operacoes = actions };
            todosServers.Add(server);
            var calculadoraResponse = new CalculadoraResponse() { Status = TipoResponse.Result, Message = "OK" };
            return MakeResult(calculadoraResponse);
        }

        private JsonResult<CalculadoraResponse> MakeResult(CalculadoraResponse calculadoraResponse)
        {
            return new JsonResult<CalculadoraResponse>(
                calculadoraResponse,
                new JsonSerializerSettings(),
                Encoding.UTF8, this);
        }

        [HttpGet]
        public JsonResult<CalculadoraResponse> Remove(string url)
        {
            todosServers.Remove(url);
            var calculadoraResponse = new CalculadoraResponse() { Status = TipoResponse.Result, Message = "OK" };
            return MakeResult(calculadoraResponse);
        }
        [HttpGet]
        public JsonResult<CalculadoraResponse> GetServer(string operacao)
        {
            var calculadoraResponse = default(CalculadoraResponse);
            try

            {
                var server = todosServers.GetServer(operacao);
                ValidaServer(server);
                calculadoraResponse = new CalculadoraResponse() { Status = TipoResponse.Proxy, Message = "OK", Url = server != null ? server.Url : "" };
            }
            catch (NaoExisteUrlRegistradaException e)
            {
                calculadoraResponse = new CalculadoraResponse() { Status = TipoResponse.Erro, Message = e.Message };
            }
            
            return MakeResult(calculadoraResponse);
        }

        private void ValidaServer(CalculadoraPath server)
        {
            if (server == null)
                throw new NaoExisteUrlRegistradaException("Não existe url registrada para operação.");
        }
    }
}
