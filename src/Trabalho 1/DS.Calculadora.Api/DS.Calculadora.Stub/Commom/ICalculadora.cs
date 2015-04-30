using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Calculadora.Stub.Commom
{
    public interface ICalculadora
    {
        double Soma(long x, long y);
        double Subtracao(long x, long y);
        double Multiplicacao(long x, long y);
        double Divisao(long x, long y);

    }
}
