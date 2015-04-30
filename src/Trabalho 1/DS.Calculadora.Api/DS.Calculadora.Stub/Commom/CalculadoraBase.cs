namespace DS.Calculadora.Stub.Commom
{
    public class CalculadoraBase : ICalculadora
    {
        public virtual double Soma(long x, long y)
        {

            return x + y;
        }

        public virtual double Subtracao(long x, long y)
        {
            return x - y;
        }

        public virtual double Multiplicacao(long x, long y)
        {
            return x * y;
        }

        public virtual double Divisao(long x, long y)
        {

            return x / y;
        }

    }
}
