using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prak7sem1task
{
    public partial class Form1 : Form
    {

        public static List<double> values = new List<double>();
        public static double des;

        public Form1()
        {
            this.SuspendLayout();
            ProgramData.startInit(this); // run initialize
            this.ResumeLayout(false);
            this.PerformLayout();
            InitializeComponent();
        }

        
        public uint performProgram()
        {
            double normGradient;
            uint iterOut = 0;
            // see in algorithm link
            while ((iterOut++< ProgramData.maxNumOfIterations2) && (ProgramData.rParam * ProgramData.Bariers(values) > des))
            {
                ProgramData.rParam *= ProgramData.bParam;
                var iter = 0;
                while ((++iter < ProgramData.maxNumOfIterations)&&((normGradient = /* ProgramData.normGradFunction(values) + */ ProgramData.normGradBariers(values)) > ProgramData.desParam))
                {
                    var gradient = ProgramData.GradBariers(values);
                    for (var i = 0; i < variables.Count; i++)
                    {
                        values.Insert(i, values.ElementAt(i) - gradient.ElementAt(i) * ProgramData.stepSize);
                        values.RemoveAt(i + 1);
                    }
                    //Debug.Print(" " + normGradient + "-" + Convert.ToString(values.ElementAt(0)) + "-" + Convert.ToString(values.ElementAt(1)) + "; ");
                }
                //Debug.Print(" " + iterOut);
                //Debug.Print(" "  + "-" + Convert.ToString(values.ElementAt(0)) + "-" + Convert.ToString(values.ElementAt(1)) + "; ");
            }
            return iterOut;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // renull variables
            ProgramData.rParam = 1.0;
            // read start point
            for (var i = 0; i < ProgramData.numOfVariables; i++)
            {
                values.Add(Convert.ToDouble(variables.ElementAt(i).Text));
            }
            // read epsilon
            des = Convert.ToDouble(textBox1.Text);
            ProgramData.desParam = Math.Max(ProgramData.desParam, des);
            // read step
            ProgramData.stepSize = Convert.ToDouble(textBox4.Text);
            // run program
            var countOfSteps = performProgram();
            // printing result
            textBox2.Text = Convert.ToString(Math.Round(ProgramData.function(values),3));
            textBox3.Text = Convert.ToString(countOfSteps);
            for (var i = 0; i < ProgramData.numOfVariables; i++)
            {
                variables.ElementAt(i).Text = Convert.ToString(Math.Round(values.ElementAt(i),3));
            }
        }
    }
}
