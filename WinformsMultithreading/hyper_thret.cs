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

namespace WinformsMultithreading
{
    public partial class hyper_thret : Form
    {

        MainMultithreadClass multithreadTest;

        private bool exit;

        public hyper_thret()
        {
            InitializeComponent();

            this.multithreadTest = new MainMultithreadClass();

            this.Closing += (s, e) =>
            {
                exit = true;
                for (int i = 0; i < multithreadTest.ThreadClasses.Count; i++)
                    multithreadTest.ThreadClasses[i].Stop();
            };

            new Thread(updateGUI).Start();

            //new MainForm().Show();

        }

        private void updateGUI()
        {
            while (true)
            {
                String consoleString = "";

                for (int i = 0; i < multithreadTest.ThreadClasses.Count; i++)
                {
                    consoleString += $"{multithreadTest.ThreadClasses[i].ToString()} {multithreadTest.ThreadClasses[i].Result} {multithreadTest.ThreadClasses[i].GetLoad()}\n";
                    ConsoleWrite(consoleString);
                }


                //multithreadTest.ThreadClasses.ForEach(t => consoleString += $"{t.currTread.Name} {t.Result}");
                if (exit)
                    break;
                Thread.Sleep(50);
            }

        }

        public void ConsoleWrite(String str)
        {
            //lock (consoleLabel)
            //{
                consoleLabel.Text = str;
                //consoleLabel.Text += "\n" + str;
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ConsoleWrite(button1.Text);
            //multithreadTest.buttonClick();
            multithreadTest.CreateAndStartThread();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            //multithreadTest.threadClass.Stop();
            //multithreadTest.ThreadClasses.ForEach(t => t.Stop());
            for (int i = 0; i < multithreadTest.ThreadClasses.Count; i++)
                multithreadTest.ThreadClasses[i].Stop();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            //multithreadTest.threadClass.Pause();
            //multithreadTest.ThreadClasses.ForEach(t => t.Pause());
            for (int i = 0; i < multithreadTest.ThreadClasses.Count; i++)
                multithreadTest.ThreadClasses[i].Pause();
        }

        private void resumeButton_Click(object sender, EventArgs e)
        {
            //multithreadTest.threadClass.Resume();
            //multithreadTest.ThreadClasses.ForEach(t => t.Resume());
            for (int i = 0; i < multithreadTest.ThreadClasses.Count; i++)
                multithreadTest.ThreadClasses[i].Resume();
        }

        private void lowestPriorityButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < multithreadTest.ThreadClasses.Count; i++)
                multithreadTest.ThreadClasses[i].SetPriority(ThreadPriority.Lowest);
        }
    }
}
