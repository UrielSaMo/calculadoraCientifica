using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculadoraCientifica
{
    internal class Operaciones
    {
        public double oper_suma(double n1, double n2)
        {
            double res = 0;
            res = n1 + n2;
            return res;
        }
        public double oper_resta(double n1, double n2)
        {
            double res = 0;
            res = n1 - n2;
            return res;
        }
        public double oper_multiplicacion(double n1, double n2)
        {
            double res = 0;
            res = n1 * n2;
            return res;
        }
        public double oper_division(double n1, double n2)
        {
            double res = 0;
            res = n1 / n2;
            return res;
        }
        public double oper_porcentaje(double n1, double n2)
        {
            double res = 0;
            res = (n1 * n2) / 100;
            return res;
        }
        public double oper_raizCuadrada(double n1)
        {
            double res = 0;
            res = Math.Sqrt(n1);
            return res;
        }
        public double oper_seno(double n1)
        {
            double res = 0;
            res = Math.Sin(n1);
            return res;
        }
        public double oper_coseno(double n1)
        {
            double res = 0;
            res = Math.Cos(n1);
            return res;
        }
        public double oper_tangente(double n1)
        {
            double res = 0;
            res = Math.Tan(n1);
            return res;
        }
        public double oper_exponente(double baseNum, double exponente)
        {
            return Math.Pow(baseNum, exponente);
        }
    }
}
