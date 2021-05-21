using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TestJson
{
    class Project
    {
        public string Name { get; }
        public Dictionary<string, string> Functions { get; }

        public Project(string name) 
        {
            Name = name;
            Functions = new Dictionary<string, string>();
        }
    }
}
