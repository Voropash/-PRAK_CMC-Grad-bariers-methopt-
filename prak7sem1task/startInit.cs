using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace prak7sem1task
{

    /*
    Bariers+grad with const step

    check me
    http://www.wolframalpha.com/input/?i=minimize+x%5E2%2By%5E2+on+y%3E%3Dx-6%2C+y%3C%3D2x-4%2C+y%3C%3D3
    
    description of algorithm
    https://yadi.sk/i/oIw75CDCjFERz
    */

    class ProgramData
    {
        // dif.constants
        public static uint numOfVariables = 6; 
        public static double stepSize = 0.00004; // for grad.method
        public static double rParam = 1.0; // see in algorithm link
        public static double gradStepParam = 0.000000001; // dx 
        public static double bParam = 0.95; // see in algorithm link
        public static double desParam = 0.01; // norma of gradient for grad.method 
        public static double maxNumOfIterations = 30000;    // catch error
        public static double maxNumOfIterations2 = 300;     // catch error

        // draw textboxes
        public static void startInit(Form1 form)
        {
            try
            {
                for (int i = 1; i < numOfVariables + 1; i++)
                {
                    var tb = new System.Windows.Forms.TextBox();
                    tb.Location = new System.Drawing.Point(55, 50 + 30 * i);
                    tb.Name = "tb" + System.Convert.ToString(i);
                    tb.Size = new System.Drawing.Size(130, 20);
                    tb.TabIndex = 0;
                    form.variables.Add(tb);
                    form.Controls.Add(tb);
                }
            }
            catch
            {
                // error
            }
        }

        // barier weight for func
        public static double Bariers(List<double> values)
        {
            double result = 0.0, eq = 0, nEq = 0, bound;
            var x1 = values.ElementAt(0);
            var x2 = values.ElementAt(1);
            var x3 = values.ElementAt(2);
            var x4 = values.ElementAt(3);
            var x5 = values.ElementAt(4);
            var x6 = values.ElementAt(5);

            // bound >= 0

            bound = 363 - ( x1*x1 + x2*x2 + x3*x3 + x4*x4 + x5*x5 + x6* x6 );
            nEq += 1 / bound;
            
            result = rParam * (eq + nEq);
            return result;

        }

        // count norm of barier gradient
        public static double normGradBariers(List<double> values)
        {
            double result = 0;
            var gradTmp = GradBariers(values);
            foreach (var i in gradTmp)
            {
                result += i * i;
            }
            return Math.Pow(result, 0.5);
        }

        // count barier gradient
        public static List<double> GradBariers(List<double> values)
        {
            List<double> result = new List<double>();

            for (int i = 0; i < numOfVariables; i++)
            {
                List<double> valuesTmp = new List<double>();
                valuesTmp.AddRange(values);
                valuesTmp.Insert(i, (valuesTmp.ElementAt(i) + gradStepParam));
                valuesTmp.RemoveAt(i + 1);
                result.Add((Bariers(valuesTmp) - Bariers(values) + function(valuesTmp) - function(values)) / gradStepParam);
            }

            return result;
        }


        // our function
        public static double function(List<double> values)
        {
            double result = 0;
            var x1 = values.ElementAt(0);
            var x2 = values.ElementAt(1);
            var x3 = values.ElementAt(2);
            var x4 = values.ElementAt(3);
            var x5 = values.ElementAt(4);
            var x6 = values.ElementAt(5);


            // x^2 + y^2 + ...
            /*
            foreach ( var i in values )
            {
                result += i * i;
            }
            */
            result = 150 *(Math.Pow(x2 - 2 * x1, 4) 
                + Math.Pow(x3 - 3 * x1, 4) 
                + Math.Pow(x4 - 4 * x1, 4) 
                + Math.Pow(x5 - 5 * x1, 4)
                + Math.Pow(x6 - 6 * x1, 4) 
                + Math.Pow(x1 - 2, 4)
                );
            return result;
        }


        // count norm of aim gradient
        public static double normGradFunction(List<double> values)
        {
            double result = 0;
            var gradTmp = GradFunction(values);
            for (int i = 0; i < numOfVariables; i++)
            {
                result += Math.Pow(gradTmp.ElementAt(i),2);
            }
            return Math.Pow(result, 0.5);
        }

        // count aim gradient
        public static List <double> GradFunction(List<double> values)
        {
            List<double> result = new List<double>();

            for (int i = 0; i < numOfVariables; i++)
            {
                List<double> valuesTmp = new List<double>();
                valuesTmp.AddRange(values);
                valuesTmp.Insert(i, (valuesTmp.ElementAt(i) + gradStepParam));
                valuesTmp.RemoveAt(i + 1);
                result.Add((function(valuesTmp)-function(values))/gradStepParam);
            }

            return result;
        }

    }
}
 