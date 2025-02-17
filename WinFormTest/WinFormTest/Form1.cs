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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            Task task = new Task(() =>
            {
                int i = 0;
                while (++i < 100)
                {
                    Thread.Sleep(10);//模拟耗时操作
                    MethodInvoker mi = new MethodInvoker(() =>
                    {
                        progressBar1.Value = i;
                        this.label1.Text = i.ToString();
                    });
                    this.BeginInvoke(mi);
                }
            });
            task.Start();
            _ = task.ContinueWith(t =>
            {
                progressBar1.Visible = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Task task1 = new Task(() =>
            {
                M1();
                MethodInvoker mi = new MethodInvoker(() =>
                {

                    this.textBox1.Text = "1";
                });
                this.BeginInvoke(mi);

                M2();
                mi = new MethodInvoker(() =>
                {

                    this.textBox1.Text = "2";
                });
                this.BeginInvoke(mi);
            });
            task1.Start();

            this.textBox1.Text = "主线程开始运行！";
        }

        private void M1()
        {
            Thread.Sleep(2000);
        }
        private void M2()
        {
            Thread.Sleep(1000);

        }
    }
}
