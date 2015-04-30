using System;

namespace DS.Calculadora.Stub.Commom.Exceptions
{
    public class ServidorNaoEncontradoException : Exception
    {
        public ServidorNaoEncontradoException(string msg = "Servidor não encontrado.")
            : base(msg)
        {

        }
    }
}
