using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformsMultithreading
{
    public partial class ChartForm : Form
    {
        private List<ThreadClass> threads;
        private bool exit;
        private float maxValue;

        public ChartForm(MainMultithreadClass mainClass)
        {
            InitializeComponent();
            exit = false;
            maxValue = 0;
            this.threads = mainClass.ThreadClasses;
            this.FormClosing += (s, e) =>
            {
                exit = true;
            };
            ShowChart();
        }


        public void ShowChart()
        {
            Show();
            threadChart.Show();
            this.Height = threadChart.Height + 50;
            this.Width = threadChart.Width + 50;
            while (!exit)
            {
                threadChart.Series.Clear();
                for (int i = 0; i < threads.Count; i++)
                {
                    threadChart.Series.Add(threads[i].ToString());
                    threadChart.Series[threads[i].ToString()].Points.Add(threads[i].GetLoad());
                    if (threads[i].GetLoad() > maxValue)
                    {
                        maxValue = threads[i].GetLoad();
                        threadChart.ResetAutoValues();
                    }
                }
                Application.DoEvents();
                //Thread.Sleep(50);
            }
        }
    }
}
