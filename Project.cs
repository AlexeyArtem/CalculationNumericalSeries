using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using NSwag.Collections;

namespace CalculationNumericalSeries
{
    public class Project
    {
        public Project(string name)
        {
            Name = name;
            Functions = new ObservableDictionary<string, string>();
        }

        public string Name { get; }
        public ObservableDictionary<string, string> Functions { get; }

        public void AddFunction(string function, string name) 
        {
            Functions.Add(function, name);
        }

        //public void RemoveFunction(int index) 
        //{
        //    KeyValuePair<string, string> pair = Functions.ElementAtOrDefault(index);
        //    Functions.Remove(pair.Key);
        //}
    }
}
