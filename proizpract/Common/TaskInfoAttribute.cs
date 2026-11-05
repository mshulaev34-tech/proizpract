using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proizpract.Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TaskInfoAttribute : Attribute
    {
        public string LevelName { get; }
        public TaskInfoAttribute(string levelName)
        {
            LevelName = levelName;
        }
    }
}
