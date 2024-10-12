using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculadoraCientifica
{
    internal class VerificadorOperaciones
    {
        public double ExtraerNumero(string parte)
        {
            if (parte.StartsWith("√"))
            {
                string interior = parte.Substring(1).Trim(); // Eliminar "√"
                double valorInterior;
                if (double.TryParse(interior, out valorInterior))
                {
                    return Math.Sqrt(valorInterior);
                }
                else
                {
                    throw new FormatException("Error: Formato de número no válido para raíz cuadrada.");
                }
            }
            else if (parte.StartsWith("sen("))
            {
                    string interior = parte.Substring(4, parte.Length - 5).Trim(); // Eliminar "sen(" y ")"
                    double valorInterior = ParseAndEvaluate(interior);
                    return Math.Sin(valorInterior * Math.PI / 180); // Convertir a radianes
                
            }
               
            else if (parte.StartsWith("cos("))
            {
                string interior = parte.Substring(4, parte.Length - 5).Trim(); // Eliminar "cos(" y ")"
                double valorInterior = ParseAndEvaluate(interior);
                return Math.Cos(valorInterior * Math.PI / 180); // Convertir a radianes
            }
            else if (parte.StartsWith("tan("))
            {
                string interior = parte.Substring(4, parte.Length - 5).Trim(); // Eliminar "tan(" y ")"
                double valorInterior = ParseAndEvaluate(interior);
                return Math.Tan(valorInterior * Math.PI / 180); // Convertir a radianes
            }
            else if (parte.StartsWith("log("))
            {
                string interior = parte.Substring(4, parte.Length - 5).Trim(); // Eliminar "log(" y ")"
                double valorInterior = ParseAndEvaluate(interior);
                return Math.Log10(valorInterior);
            }
            else
            {
                // Evaluar directamente si no es una función especial
                double valor;
                if (double.TryParse(parte, out valor))
                {
                    return valor;
                }
                else
                {
                    throw new FormatException("Error: Formato de número no válido.");
                }
            }
        }

        private double ParseAndEvaluate(string expresion)
        {
            // Aquí podrías incluir una lógica para evaluar expresiones más complejas
            return double.Parse(expresion); // Simplificado para este ejemplo
        }
    }

}