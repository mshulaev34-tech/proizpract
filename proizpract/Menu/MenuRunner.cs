using proizpract.Common;
using proizpract.Tasks.level1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace proizpract.menu
{
    /// <summary>
    /// 
    /// </summary>
    internal class MenuRunner
    {
        private bool IsInit = false;
        static public MenuRunner _instance;
        public MenuRunner()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }
        public List<LevelMenu> ArrayLevels = new List<LevelMenu>();
        /// <summary>
        /// Running Menu 
        /// </summary>
        public void Run()
        {
            LevelInits();
            InitTask();
            IsInit = true;

            for (int i = 0; i < ArrayLevels.Count; i++)
            {
                Console.WriteLine($"{i} : {ArrayLevels[i].GetName()}");
            }
            int index = WriteIndexMenu();
            ArrayLevels[index].Show();
        }
        /// <summary>
        /// iteration getting correctin index from user
        /// </summary>
        /// <returns>INT : return validate level Menu index </returns>
        private int WriteIndexMenu()
        {
            string read = Console.ReadLine();
            int NumTask = -1;
            if (read == "q")
            {
                Console.WriteLine("Close Programm");
                Environment.Exit(0);
            }
            try
            {
                NumTask = Convert.ToInt32(read);
                if (NumTask >= 0 && NumTask < ArrayLevels.Count)
                {
                    return NumTask;
                }
            }
            catch { }

            Console.WriteLine("None Valid Index , Write Correct index Again");
            return WriteIndexMenu();
        }
        /// <summary>
        /// Create new Level
        /// </summary>
        private void LevelInits()
        {
            if (IsInit) return;
            new LevelMenu("Level-1");
            new LevelMenu("Level-2");
            //new LevelMenu("Level-3");
        }
        public void AddOnLevelByName(string name, ITask task)
        {
            foreach (LevelMenu level in ArrayLevels)
            {
                if (level.GetName() == name)
                {
                    level.Tasks.Add(task);
                    break;
                }
            }
        }
        private void InitTask()
        {
            if (IsInit) return;
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var allTypes = assembly.GetTypes();
            var TaskTypes = allTypes.Where(Type => Type.GetCustomAttribute<Common.TaskInfoAttribute>() != null);

            foreach (var type in TaskTypes)
            {
                var attribute = type.GetCustomAttribute<Common.TaskInfoAttribute>();
                string targetLevelName = attribute.LevelName;

                ITask newTaskInstance = (ITask)Activator.CreateInstance(type);

                AddOnLevelByName(targetLevelName, newTaskInstance);
            }
        }
    }
}
