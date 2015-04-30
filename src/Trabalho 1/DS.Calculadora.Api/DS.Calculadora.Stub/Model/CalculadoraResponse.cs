namespace DS.Calculadora.Stub.Model
{
    public class CalculadoraResponse
    {
        public TipoResponse Status { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public double Result { get; set; }
    }

    public enum TipoResponse
    {
        Result,
        Erro,
        Proxy,
        ServerDirectory
    }
}
