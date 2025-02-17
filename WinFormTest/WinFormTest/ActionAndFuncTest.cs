using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormTest
{
    public partial class ActionAndFuncTest : Form
    {
        public ActionAndFuncTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Action<string> action = new Action<string>(Display);
            action("Hello!!!");
            Console.Read();
        }

        public static void Display(string message)
        {
            Console.WriteLine(message);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Func<int, double> func = new Func<int, double>(CalculateHra);
            Console.WriteLine(func(50000));
            Console.Read();
        }

        static double CalculateHra(int basic)
        {
            return (double)(basic * .4);
        }
    }
}
