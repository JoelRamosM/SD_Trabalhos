using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using DS.Calculadora.Stub.Commom;
using DS.Calculadora.Stub.Model;

namespace DS.Calculadora.Stub.Remote
{
    public class CallServer
    {
        private readonly string serverUrl;
        private string action;
        private Action onError;
        private Action onSuccess;

        private NameValueCollection queryString;
        public CallServer(string serverUrl)
        {
            if (!serverUrl.StartsWith("http://"))
                serverUrl = "http://" + serverUrl;
            this.serverUrl = serverUrl;
        }

        public CallServer Action(string action)
        {
            this.action = action;
            return this;
        }

        public CallServer 
            Params(params Tuple<string, string>[] args)
        {
            queryString = new NameValueCollection();
            foreach (var arg in args)
            {
                queryString.Add(arg.Item1, arg.Item2);
            }
            return this;
        }

        public CallServer OnError(Action callback)
        {
            onError = callback;
            return this;
        }
        public CalculadoraResponse ExecuteGet()
        {
            var webClient = new WebClient();
            if (queryString != null && queryString.AllKeys.Any())
                webClient.QueryString = queryString;
            webClient.Encoding = Encoding.UTF8;
            try
            {
                var jsonResponse = webClient.DownloadString(string.Format("{0}/{1}", serverUrl, action));
                var calculadoraResponse = JsonHelper.JsonDeserialize<CalculadoraResponse>(jsonResponse);
                ExecCallbacks(calculadoraResponse);
                return calculadoraResponse;
            }
            catch (WebException e)
            {
                throw new Exception("Não foi possivel conectar-se com o servidor.");
            }

        }

        public CallServer ServerAlive()
        {
            var webClient = new WebClient();
            try
            {
                webClient.DownloadData(string.Format("{0}/{1}", serverUrl, Actions.ServerAlive));
            }
            catch (WebException)
            {
                throw;
            }
            return this;
        }

        private void ExecCallbacks(CalculadoraResponse calculadoraResponse)
        {
            if (calculadoraResponse.Status == TipoResponse.Erro)
            {
                if (onError != null)
                    onError.Invoke();
            }
            else if (calculadoraResponse.Status == TipoResponse.Result)
            {
                if (onSuccess != null)
                    onSuccess.Invoke();
            }
        }
    }
}
