using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using static System.Windows.Forms.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Timers;

namespace WinformsMultithreading
{
    public partial class MainForm : Form
    {
        private readonly MainMultithreadClass mainMultithreadClass;

        private readonly String RESULT_LABEL = "Среднее значение: ";

        private List<ThreadClass> selectedThreads;

        private bool exit;

        private static System.Timers.Timer timer;

        public MainForm()
        {
            InitializeComponent();

            mainMultithreadClass = new MainMultithreadClass();
            exit = false;
            selectedThreads = new List<ThreadClass>();

            ApplySettings();

            //new Thread(() => { 
            //    while (!exit)
            //    {
            //        Application.DoEvents();
            //    }
            
            //}).Start();

            Show();
            //UpdateGUI();

            timer = new System.Timers.Timer(1000);
            timer.Elapsed += UpdateGUI;
            timer.Enabled = true;
        }

        private void ApplySettings()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.Closing += (s, e) =>
            {
                exit = true;
                for (int i = 0; i < mainMultithreadClass.ThreadClasses.Count; i++)
                    mainMultithreadClass.ThreadClasses[i].Stop();
            };

            priorityComboBox.Items.Clear();
            priorityComboBox.DataSource = Enum.GetValues(typeof(ThreadPriority));
            priorityComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            priorityComboBox.SelectedIndex = 2;
        }

        private void UpdateGUI(Object source, ElapsedEventArgs e)
        {
            //while (!exit)
            //{
                //Application.DoEvents();

                List<double> results = new List<double>();
                for (int i = 0; i < table.Items.Count; i++)
                {
                    ThreadClass threadClass = ((ThreadListViewItem)table.Items[i]).ThreadClass;
                    double currResult = threadClass.Result;
                    results.Add(currResult);
                    UpdateRowByThreadClass(i, threadClass);
                }
                if (results.Count != 0)
                    resultLabel.Text = RESULT_LABEL + results.Average().ToString();
                else
                    resultLabel.Text = RESULT_LABEL + "0";

            //    Thread.Sleep(10);
            //}
        }

        private void UpdateRowByThreadClass(int rowNum, ThreadClass threadClass)
        {
            table.Items[rowNum].SubItems[0].Text = threadClass.ToString();
            table.Items[rowNum].SubItems[1].Text = threadClass.Result.ToString();
            table.Items[rowNum].SubItems[2].Text = threadClass.SleepTime.ToString();
            table.Items[rowNum].SubItems[3].Text = threadClass.GetLoad().ToString();
            table.Items[rowNum].SubItems[4].Text = threadClass.GetPriority().ToString();
            table.Items[rowNum].SubItems[5].Text = threadClass.State.ToString();
        }

        private void table_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedListViewItemCollection items = table.SelectedItems;
            if (items.Count == 0)
                return;
            selectedThreads = new List<ThreadClass>();

            for (int i = 0; i < items.Count; i++)
            {
                selectedThreads.Add(((ThreadListViewItem)items[i]).ThreadClass);
            }

            nameLabel.Text = String.Join(", ", selectedThreads);
        }

        private void createUpdateButton_Click(object sender, EventArgs e)
        {
            if (selectedThreads.Count == 0)
                CreateThread();
            else
                selectedThreads.ForEach(t => UpdateThread(t));
        }

        private void CreateThread()
        {
            ThreadClass threadClass;
            if (!ValidateFields())
            {
                threadClass = mainMultithreadClass.CreateThread();
            }
            else
            {
                ThreadPriority priority = ThreadPriority.Normal;
                Enum.TryParse(priorityComboBox.Text, out priority);
                threadClass = mainMultithreadClass.CreateThread(int.Parse(sleeptimeTextBox.Text), priority);
            }

            ThreadListViewItem listItem = new ThreadListViewItem(threadClass.ToString(), threadClass);
            listItem.SubItems.Add("0");
            listItem.SubItems.Add(threadClass.SleepTime.ToString());
            listItem.SubItems.Add(threadClass.GetLoad().ToString());
            listItem.SubItems.Add(threadClass.GetPriority().ToString());
            listItem.SubItems.Add(threadClass.State.ToString());
            table.Items.Add(listItem);

            threadClass.Start();
        }

        private void UpdateThread(ThreadClass threadClass)
        {
            if (!ValidateFields())
                return;
            ThreadPriority priority = ThreadPriority.Normal;
            Enum.TryParse(priorityComboBox.Text, out priority);
            threadClass.UpdateThread(int.Parse(sleeptimeTextBox.Text), priority);
        }

        private bool ValidateFields()
        {
            try
            {
                int value = int.Parse(sleeptimeTextBox.Text);
                if (value < 0)
                    return false;
                return true;
            }
            catch {
                return false;
            }

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            mainMultithreadClass.deleteThreads(selectedThreads);

            foreach (ThreadListViewItem threadItem in table.SelectedItems)
            {
                table.Items.Remove(threadItem);
            }

            ClearChoice();
        }

        private void resumeButton_Click(object sender, EventArgs e)
        {
            selectedThreads.ForEach(t => t.Resume());
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            selectedThreads.ForEach(t => t.Pause());
        }

        //private void stopButton_Click(object sender, EventArgs e)
        //{
        //    selectedThreads.ForEach(t => t.Stop());
        //}

        private void clearChoiceButton_Click(object sender, EventArgs e)
        {
            ClearChoice();
        }

        private void ClearChoice()
        {
            table.SelectedItems.Clear();
            selectedThreads.Clear();
            nameLabel.Text = String.Join(", ", selectedThreads);
        }

        private void ShowChart(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                ChartForm chartForm = new ChartForm(mainMultithreadClass);

                //chartForm.Show();
                //chartForm.ShowChart();
            }).Start();
        }
    }
}
