using proizpract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace proizpract.Menu
{
    /// <summary>
    /// Main menu and runner all menu
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

            Console.WriteLine("=== Практикум по C# ===");
            for (int i = 0; i < ArrayLevels.Count; i++)
            {
                Console.WriteLine($"{i} : {ArrayLevels[i].GetName()}");
            }
            Console.WriteLine("Выберите Меню (q- для выхода):");
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
                Console.WriteLine("Закрытие программы...");
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

            Console.WriteLine("Не коректный индекс , введите индекс повторно:");
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
        }
        /// <summary>
        /// Add task in level by name
        /// </summary>
        /// <param name="name">level name to which conncect task</param>
        /// <param name="task">class who exuteting</param>
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
        /// <summary>
        /// get all class with TaskInfoAttribure and link with level menu
        /// </summary>
        private void InitTask()
        {
            if (IsInit) return;
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var allTypes = assembly.GetTypes();
            var TaskTypes = allTypes.Where(t => t.GetCustomAttribute<Common.TaskInfoAttribute>() != null)
                                    .OrderBy(t =>
                                    {
                                        var attr = t.GetCustomAttribute<Common.TaskInfoAttribute>();
                                        return attr?.Order ?? 0;
                                    })
                                    .ToList();

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
