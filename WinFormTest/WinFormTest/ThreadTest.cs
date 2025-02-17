using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormTest
{
    public partial class ThreadTest : Form
    {
        public ThreadTest()
        {
            InitializeComponent();
        }

        private ManualResetEvent a = null;
        private CancellationTokenSource c = null;
        private void button1_Click(object sender, EventArgs e)
        {
            a = new ManualResetEvent(true);
            c = new CancellationTokenSource();
            var task = new Task(() =>
            {
                try
                {
                    for (int i = 0; i < 100; i++)
                    {
                        if (c.IsCancellationRequested)
                        {
                            throw new InvalidOperationException();
                        }
                        a.WaitOne();
                        Invoke(new Action(() =>
                        {
                            textBox1.Text = i.ToString();
                        }));

                        Thread.Sleep(500);
                    }
                }
                catch (InvalidOperationException)
                {
                    Invoke(new Action(() => textBox1.Text = "线程已经取消"));
                }

            }, c.Token);
            task.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            a.Reset();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            a.Set();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //a.Set();
            c.Cancel();
        }
    }
}
