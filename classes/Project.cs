using System;
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

        public event EventHandler ChangingFunctions;

        private void OnChangingFunctions() 
        {
            ChangingFunctions?.Invoke(this, new EventArgs());
        }

        public void AddFunction(string function, string name) 
        {
            Functions.Add(function, name);
            OnChangingFunctions();
        }

        public void ChangeFunction(string oldFunction, string newFunction, string newName) 
        {
            if (Functions.Contains(newFunction)) throw new ArgumentException("Функция уже существует в проекте");
            
            if (Functions.Remove(oldFunction)) 
            {
                Functions.Add(newFunction, newName);
                OnChangingFunctions();
            }
        }

        public void RemoveFunction(string function) 
        {
            if (Functions.Remove(function))
                OnChangingFunctions();
        }
    }
}
