using System;
using DS.Calculadora.ServerDirApi.Models;

namespace DS.Calculadora.ServerDirApi.DAO.Commom
{
    public interface IServers
    {
        CalculadoraPath Add(CalculadoraPath path);
        void Remove(string url);

        CalculadoraPath GetServer(string operacao);
    }
}
