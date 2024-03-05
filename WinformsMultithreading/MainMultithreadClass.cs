using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Security.Policy;

namespace WinformsMultithreading
{
    public class MainMultithreadClass
    {
        public List<ThreadClass> ThreadClasses { get; set; }

        Random rand = new Random();

        public MainMultithreadClass()
        {
            ThreadClasses = new List<ThreadClass>();
        }

        public void CreateAndStartThread()
        {
            //if (threadClass == null)
            ThreadClass threadClass = CreateThread();
            startThread(threadClass);
        }

        public ThreadClass CreateThread()
        {
            ThreadClass threadClass = new ThreadClass(GenerateName());
            ThreadClasses.Add(threadClass);
            return threadClass;
        }

        public ThreadClass CreateThread(int sleepTime, ThreadPriority priority)
        {
            ThreadClass threadClass = new ThreadClass(GenerateName(), sleepTime, priority);
            ThreadClasses.Add(threadClass);
            return threadClass;
        }
        public string RandomizeName()
        {
            string name = $"Thread {rand.Next(100)}";
            return name;
        }

        public string GenerateName()
        {
            while (true)
            {
                string name = RandomizeName();
                if (ThreadClasses.Find(t => t.ToString() == name) == default(ThreadClass))
                    return name;
            }
        }

        public void startThread(ThreadClass threadClass)
        {
            threadClass.Start();
        }

        public void deleteThreads(List<ThreadClass> threadClasses)
        {
            threadClasses.ForEach(t => deleteThread(t));
        }

        public void deleteThread(ThreadClass threadClass)
        {
            threadClass.Stop();
            ThreadClasses.Remove(threadClass);
        }
    }
}
