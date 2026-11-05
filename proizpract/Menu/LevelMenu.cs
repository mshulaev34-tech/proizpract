using proizpract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proizpract.menu
{
    internal class LevelMenu
    {
        public List<Common.ITask> Tasks = new List<Common.ITask>();
        private string LevelName = "NULL";
        public LevelMenu(string Name)
        {
            LevelName = Name;
            MenuRunner._instance.ArrayLevels.Add(this);
        }
        public void Show()
        {
            while (true)
            {
                Console.WriteLine("SELECT INDEX LEVEL");
                for (int i = 0; i < Tasks.Count; i++)
                {
                    ITask task = Tasks[i];
                    Console.WriteLine($"{i} : {task.Title}");
                }
                int TaskIndex = WriteIndexTask();
                //print TAsk info
                Console.WriteLine($"{TaskIndex} : {Tasks[TaskIndex].Title}");
                Console.WriteLine($" Description: \n{Tasks[TaskIndex].Description}");
                Console.WriteLine(" Requirements:");
                foreach (string requir in Tasks[TaskIndex].Requirements)
                {
                    Console.WriteLine($"\t{requir}");
                }
                Console.WriteLine("RUN EXUCUTION...\n");
                Tasks[TaskIndex].Run();
            }

        }
        private int WriteIndexTask()
        {
            string read = Console.ReadLine();
            int NumTask = -1;
            if (read == "q")
            {
                Console.WriteLine("Exit main menu...\n");
                MenuRunner._instance.Run();
            }
            try
            {
                NumTask = Convert.ToInt32(read);
                if (NumTask >= 0 && NumTask < Tasks.Count)
                {
                    return NumTask;
                }
            }
            catch { }

            Console.WriteLine("None Valid Task Index  , Write Correct  Task index Again");
            return WriteIndexTask();

        }
        public string GetName()
        {
            return LevelName;
        }

    }
}
