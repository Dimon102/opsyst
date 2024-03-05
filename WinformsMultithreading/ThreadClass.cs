using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinformsMultithreading
{
    public class ThreadClass
    {
        public double Result { get; set; }
        public ThreadState State { get; set; }
        public int SleepTime { get; set; }

        private long duration;
        private DateTime resumeTime;
        private int general_counter;




        private Thread currTread;

        AutoResetEvent waitHandler = new AutoResetEvent(true);

        Random rand = new Random();

        public ThreadClass(String name)
        {
            currTread = new Thread(calculate);

            currTread.Name = name;
            SleepTime = 20;
        }

        public ThreadClass(String name, int sleepTime, ThreadPriority priority) : this(name)
        {
            SleepTime = sleepTime;
            currTread.Priority = priority;
        }

        public void UpdateThread(int sleepTime, ThreadPriority priority)
        {
            SleepTime = sleepTime;
            currTread.Priority = priority;
        }

        private void calculate()
        {
            int a = 1;

            double x;
            double y;

            int success_counter = 0;
            while (true)
            {
                general_counter++;

                x = rand.NextDouble() * a;
                y = rand.NextDouble() * a;
                if (Math.Pow(x, 2) + Math.Pow(y, 2) <= a)
                    success_counter++;

                Result = 4 * (float)success_counter / general_counter;

                if (State.Equals(ThreadState.Stopped))
                    break;
                if (State.Equals(ThreadState.Paused))
                    waitHandler.WaitOne();
                Thread.Sleep(SleepTime);
            }
        }

        public void Start()
        {
            currTread.Start();
            resumeTime = DateTime.Now;
            State = ThreadState.Running;
        }

        public void Pause()
        {
            if (State.Equals(ThreadState.Paused))
                return;
            duration += (long)DateTime.Now.Subtract(resumeTime).TotalSeconds;
            State = ThreadState.Paused;
        }

        public void Stop()
        {
            if (State.Equals(ThreadState.Stopped))
                return;
            duration += (long)DateTime.Now.Subtract(resumeTime).TotalSeconds;
            State = ThreadState.Stopped;
        }

        public void Resume()
        {
            State = ThreadState.Running;
            resumeTime = DateTime.Now;
            waitHandler.Set();
        }

        public void SetPriority(ThreadPriority priority)
        {
            currTread.Priority = priority;
        }

        public ThreadPriority GetPriority()
        {
            return currTread.Priority;
        }

        public float GetLoad()
        {
            long delta = GetDuration();
            if (delta == 0)
                delta = 1;
            return (float) general_counter / delta;
        }

        public long GetDuration()
        {
            if (State.Equals(ThreadState.Running))
                return duration + (long)DateTime.Now.Subtract(resumeTime).TotalSeconds;
            return duration;
        }

        public override String ToString()
        {
            return currTread.Name;
        }
    }

    public enum ThreadState
    {
        Running,
        Paused,
        Stopped
    }
}
