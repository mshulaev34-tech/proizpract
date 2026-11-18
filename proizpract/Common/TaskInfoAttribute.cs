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
        public int Order { get; }
        public TaskInfoAttribute(string levelName, int order)
        {
            LevelName = levelName;
            Order = order;
        }
    }
}
