using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculadoraCientifica
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Display.TextChanged += Display_TextChanged;
        }
        private void Display_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string cadena = Display.Text;
                if (!string.IsNullOrEmpty(cadena) && !cadena.EndsWith("+") && !cadena.EndsWith("-") 
                    && !cadena.EndsWith("x") && !cadena.EndsWith("/") && !cadena.EndsWith("%"))
                {
                    // Llamamos a la lógica de cálculo
                    double result = CalcularResultado(cadena);
                    Display2.Text = result.ToString();
                }
            }
            catch (Exception)
            {
                // Si hay un error en la expresión, no hacemos nada
                Display2.Clear();
            }
        }
        private void CalcularResultadoEnTiempoReal()
        {
            try
            {
                // Reemplazar el símbolo de potencia por la función Math.Pow
                string cadena = Display.Text;

                // Evaluar el exponente de la forma "x^y", "x^2" o "x^3"
                while (cadena.Contains("^"))
                {
                    int index = cadena.IndexOf("^");
                    string baseParte = ObtenerParteIzquierda(cadena, index);
                    string exponenteParte = ObtenerParteDerecha(cadena, index + 1);

                    double baseNum = double.Parse(baseParte);
                    double exponenteNum = double.Parse(exponenteParte);

                    double resultadoExponente = Math.Pow(baseNum, exponenteNum);

                    // Reemplazar la expresión con el resultado
                    cadena = cadena.Replace(baseParte + "^" + exponenteParte, resultadoExponente.ToString());
                }

                // Actualizar el segundo display con el resultado en tiempo real
                Display2.Text = cadena;
            }
            catch (Exception ex)
            {
                Display2.Text = "Error";
            }
        }

        private string ObtenerParteIzquierda(string cadena, int index)
        {
            // Aquí extraemos la base antes del símbolo de potencia (^)
            int startIndex = index - 1;
            while (startIndex >= 0 && char.IsDigit(cadena[startIndex]))
            {
                startIndex--;
            }
            return cadena.Substring(startIndex + 1, index - startIndex - 1);
        }

        private string ObtenerParteDerecha(string cadena, int index)
        {
            // Aquí extraemos el exponente después del símbolo de potencia (^)
            int endIndex = index;
            while (endIndex < cadena.Length && char.IsDigit(cadena[endIndex]))
            {
                endIndex++;
            }
            return cadena.Substring(index, endIndex - index);
        }
        private double CalcularResultado(string cadena)
        {
            // Aquí puedes reutilizar tu lógica de cálculo, tal como en el botón igual.
            Operaciones oper = new Operaciones();
            VerificadorOperaciones verificador = new VerificadorOperaciones();
            double result = 0;

            // Reemplazar π por su valor numérico
            cadena = cadena.Replace("π", Math.PI.ToString());

            string[] operadores = { "+", "-", "x", "/", "^", "%" };

            // Si la cadena contiene √ y un índice, lo manejamos como una operación especial
            if (cadena.Contains("√"))
            {
                int indiceRaiz = cadena.IndexOf("√");
                string parte1 = cadena.Substring(0, indiceRaiz).Trim();
                string parte2 = cadena.Substring(indiceRaiz + 1).Trim();

                double num1 = 0;
                double num2 = 0;

                if (parte1.Length > 0 && double.TryParse(parte1, out num1))
                    num1 = num1;
                else
                    num1 = 2;

                if (double.TryParse(parte2, out num2))
                    result = Math.Pow(num2, 1 / num1);
            }
            else
            {
                int operadorIndex = -1;
                foreach (var op in operadores)
                {
                    operadorIndex = cadena.LastIndexOf(op);
                    if (operadorIndex != -1)
                        break;
                }

                if (operadorIndex == -1)
                {
                    result = verificador.ExtraerNumero(cadena);
                }
                else
                {
                    string operador = cadena[operadorIndex].ToString();
                    string parte1 = cadena.Substring(0, operadorIndex).Trim();
                    string parte2 = cadena.Substring(operadorIndex + 1).Trim();

                    double num1 = verificador.ExtraerNumero(parte1);
                    double num2 = verificador.ExtraerNumero(parte2);

                    switch (operador)
                    {
                        case "+":
                            result = oper.oper_suma(num1, num2);
                            break;
                        case "-":
                            result = oper.oper_resta(num1, num2);
                            break;
                        case "x":
                            result = oper.oper_multiplicacion(num1, num2);
                            break;
                        case "/":
                            result = oper.oper_division(num1, num2);
                            break;
                        case "%":
                            result = num1 * (num2 / 100);
                            break;
                    }
                }
            }

            return result;
        }
        private void button0_Click(object sender, EventArgs e)
        {
            Display.Text += "0";  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Display.Text += "1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Display.Text += "2";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Display.Text += "3";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Display.Text += "4";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Display.Text += "5";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Display.Text += "6";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Display.Text += "7";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Display.Text += "8";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Display.Text += "9";
        }

        private void buttonPunto_Click(object sender, EventArgs e)
        {
            
                Display.Text += ".";
        }

        private void buttonSuma_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Display.Text) || Display.Text.EndsWith("+"))
            {
                return;
            }
            AgregarOperador("+");
        }

        private void buttonResta_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Display.Text))
            {
                Display.Text = "-";
            }
            AgregarOperador("-");
        }

        private void buttonMultiplicacion_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Display.Text) || Display.Text.EndsWith("x"))
            {
                return;
            }
            AgregarOperador("x");
        }

        private void buttonDivision_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Display.Text) || Display.Text.EndsWith("/"))
            {
                return;
            }
            AgregarOperador("/");
        }

        private void buttonPorcentaje_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Display.Text) || Display.Text.EndsWith("%"))
            {
                return;
            }
            AgregarOperador("%");
        }

        private void buttonRaizCuadrada_Click(object sender, EventArgs e)
        {

                Display.Text += "√";
            

        }
        private void AgregarOperador(string operador)
        {
            if (Display.Text.Length > 0 && "+-x/%".Contains(Display.Text.Last()))
            {
                Display.Text = Display.Text.Substring(0, Display.Text.Length - 1) + operador;
            }
            else if (Display.Text.Length > 0)
            {
                Display.Text += operador;
            }
        }
        private void buttonBorrar_Click(object sender, EventArgs e)
        {
            String contenido = Display.Text;
            if (!string.IsNullOrEmpty(contenido)) 
            {
                contenido = contenido.Substring(0, contenido.Length - 1);
                Display.Text = contenido;
            }
        }
        //Operaciones oper = new Operaciones();
        //VerificadorOperaciones verificador = new VerificadorOperaciones();
        private void buttonIgual_Click(object sender, EventArgs e)
        {
            Operaciones oper = new Operaciones();
            VerificadorOperaciones verificador = new VerificadorOperaciones();
            double result = 0;
            string cadena = Display.Text;

            try
            {
                // Reemplazar π por su valor numérico
                cadena = cadena.Replace("π", Math.PI.ToString());

                // Verifica si hay una operación incompleta
                if (cadena.Contains("(") && !cadena.Contains(")"))
                {
                    MessageBox.Show("Error: Operación incompleta. Asegúrate de cerrar todas las operaciones.");
                    Display.Clear();
                    return;
                }

                // Definir una expresión que manejará todos los operadores
                string[] operadores = { "+", "-", "x", "/", "^", "%" };

                // Si la cadena contiene √ y un índice, lo manejamos como una operación especial
                if (cadena.Contains("√"))
                {
                    // Extraer el índice de la raíz y el número
                    int indiceRaiz = cadena.IndexOf("√");
                    string parte1 = cadena.Substring(0, indiceRaiz).Trim();
                    string parte2 = cadena.Substring(indiceRaiz + 1).Trim();

                    double num1 = 0; // Índice de la raíz
                    double num2 = 0; // Número del cual calcular la raíz

                    if (parte1.Length > 0)
                    {
                        if (double.TryParse(parte1, out num1))
                        {
                            // Si parte1 tiene un índice, tomarlo; si no, asumir raíz cuadrada (2)
                            num1 = num1;
                        }
                        else
                        {
                            MessageBox.Show("Error: Índice de raíz no válido.");
                            Display.Clear();
                            return;
                        }
                    }
                    else
                    {
                        num1 = 2; // Por defecto, si no se especifica índice, asumimos raíz cuadrada
                    }

                    if (double.TryParse(parte2, out num2))
                    {
                        result = Math.Pow(num2, 1 / num1); // Calcula la raíz
                    }
                    else
                    {
                        MessageBox.Show("Error: Número para calcular raíz no válido.");
                        Display.Clear();
                        return;
                    }
                }
                else
                {
                    // Ordenar los operadores según la prioridad
                    int operadorIndex = -1;
                    foreach (var op in operadores)
                    {
                        operadorIndex = cadena.LastIndexOf(op);
                        if (operadorIndex != -1)
                            break;
                    }

                    if (operadorIndex == -1)
                    {
                        // Si no hay operadores, solo maneja el caso especial
                        try
                        {
                            result = verificador.ExtraerNumero(cadena);
                        }
                        catch (FormatException ex)
                        {
                            MessageBox.Show(ex.Message);
                            Display.Clear();
                            return;
                        }
                    }
                    else
                    {
                        string operador = cadena[operadorIndex].ToString();
                        string parte1 = cadena.Substring(0, operadorIndex).Trim();
                        string parte2 = cadena.Substring(operadorIndex + 1).Trim();

                        double num1;
                        double num2;

                        try
                        {
                            num1 = verificador.ExtraerNumero(parte1);
                            num2 = verificador.ExtraerNumero(parte2);
                        }
                        catch (FormatException ex)
                        {
                            MessageBox.Show(ex.Message);
                            Display.Clear();
                            return;
                        }
                        switch (operador)
                        {
                            case "+":
                                result = oper.oper_suma(num1,num2);
                                break;
                            case "-":
                                result = oper.oper_resta(num1, num2);
                                break;
                            case "x":
                                result = oper.oper_multiplicacion(num1, num2);
                                break;
                            case "/":
                                if (num2 != 0)
                                {
                                    result = oper.oper_division(num1, num2);
                                }
                                else
                                {
                                    MessageBox.Show("No es posible realizar esta operación. División por cero.");
                                    Display.Clear();
                                    return;
                                }
                                break;
                            case "%":
                                result = num1 * (num2 / 100);
                                break;
                            case "^":
                                result = Math.Pow(num1, num2); 
                                break;
                        }
                    }
                }

                Display2.Text = result.ToString();
                Display.Text = Display2.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Display.Clear();
            }
        }

        private void buttonSeno_Click(object sender, EventArgs e)
        {
            Display.Text += "sen(";
        }

        private void buttonC_Click(object sender, EventArgs e)
        {
            Display.Clear();
            Display2.Clear();

        }

        private void buttonCE_Click(object sender, EventArgs e)
        {
            // Si el Display no está vacío
            if (Display.Text.Length > 0)
            {
                // Eliminar la última entrada (último carácter)
                Display.Text = Display.Text.Substring(0, Display.Text.Length - 1);

                // Recalcular el resultado en tiempo real para actualizar el Display2
                CalcularResultadoEnTiempoReal();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void buttonParentesisC_Click(object sender, EventArgs e)
        {
            Display.Text += ")";
        }

        private void buttonCoseno_Click(object sender, EventArgs e)
        {
            Display.Text += "cos(";
        }

        private void buttonTangente_Click(object sender, EventArgs e)
        {
            Display.Text += "tan(";
        }

        private void buttonXelavadoY_Click(object sender, EventArgs e)
        {
            Display.Text += "^";
            CalcularResultadoEnTiempoReal();
        }

        private void buttonXalcubo_Click(object sender, EventArgs e)
        {
            Display.Text += "^3";
            CalcularResultadoEnTiempoReal();
        }

        private void buttonXalcuadrado_Click(object sender, EventArgs e)
        {
            Display.Text += "^2";
            CalcularResultadoEnTiempoReal();
        }

        private void buttonPI_Click(object sender, EventArgs e)
        {
            Display.Text += "π";
        }

        private void buttonRaizCubicaX_Click(object sender, EventArgs e)
        {
            Display.Text += "3√";
        }

        private void buttonRaizYdeX_Click(object sender, EventArgs e)
        {
            Display.Text += "√";
        }

        private void button10elevadoX_Click(object sender, EventArgs e)
        {
            Display.Text += "10^";
        }

        private void buttonEXP_Click(object sender, EventArgs e)
        {
            Display.Text += "^";
        }

        private void buttonLogaritmo_Click(object sender, EventArgs e)
        {
            Display.Text += "log(";
        }

        private void buttonMasMenos_Click(object sender, EventArgs e)
        {
            string cadena = Display.Text;

            if (string.IsNullOrWhiteSpace(cadena))
            {
                return;
            }

            if (cadena.StartsWith("-"))
            {
                Display.Text = cadena.Substring(1);
            }
            else
            {
                Display.Text = "-" + cadena;
            }
        }

    }
}
