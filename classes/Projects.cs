using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NSwag.Collections;

namespace CalculationNumericalSeries
{
    static class Projects
    {
        private static string nameFile = "projects.json";
        private static ObservableCollection<Project> listProjects;
        private static Project currentProject;
        private static ObservableDictionary<string, string> currentFunctions;

        static Projects() 
        {
            using (FileStream file = new FileStream(nameFile, FileMode.OpenOrCreate))
            {
                byte[] array = new byte[file.Length];
                file.Read(array, 0, array.Length);
                string jsonText = Encoding.Default.GetString(array);

                List<Project> list = JsonConvert.DeserializeObject<List<Project>>(jsonText);
                listProjects = JsonConvert.DeserializeObject<ObservableCollection<Project>>(jsonText);
                if (!(listProjects is null)) 
                {
                    ListProjects = new ReadOnlyObservableCollection<Project>(listProjects);
                    CurrentProject = listProjects[0];
                }
                else
                {
                    listProjects = new ObservableCollection<Project>() { new Project("Проект по умолчанию") };
                    CurrentProject = listProjects[0];
                    ListProjects = new ReadOnlyObservableCollection<Project>(listProjects);
                }
            }

        }

        /// <summary>
        /// Возвращает или задает текущий выбранный проект
        /// </summary>
        public static Project CurrentProject 
        {
            get 
            {
                return currentProject;
            }
            set 
            {
                if (listProjects.Contains(value)) 
                {
                    currentProject = value;
                    currentFunctions = currentProject.Functions;
                }
            }
        }

        public static ObservableDictionary<string, string> CurrentFunctions 
        {
            get 
            {
                return currentFunctions;
            }
            private set 
            {
                currentFunctions = value;
            }
        }

        public static ReadOnlyObservableCollection<Project> ListProjects { get; }

        private static void Save() 
        {
            string json = JsonConvert.SerializeObject(listProjects);
            File.WriteAllText(nameFile, json, Encoding.Default);
        }

        public static void AddProject(string name) 
        {
            listProjects.Add(new Project(name));
            Save();
        }

        public static void RemoveProject(Project project) 
        {
            if (listProjects.Contains(project) && listProjects.Count < 2) throw new Exception("Нельзя удалить последний проект");

            listProjects.Remove(project);
            if (CurrentProject == project)
                CurrentProject = listProjects[0];
            Save();
        }

        public static void AddFunction(Project project, string function, string name) 
        {
            int index = listProjects.IndexOf(project);
            if (index != -1) 
            {
                listProjects[index].Functions.Add(function, name);
                Save();
            }
        }

        public static void RemoveFunction(string function) 
        {
            for (int i = 0; i < listProjects.Count; i++)
            {
                if (listProjects[i].Functions.Remove(function)) 
                {
                    Save();
                    break;
                }
            }
        }

        public static void ChangeFunction(string oldFunction, string newFunction, string newName) 
        {
            for (int i = 0; i < listProjects.Count; i++)
            {
                if (listProjects[i].Functions.Remove(oldFunction))
                {
                    listProjects[i].Functions.Add(newFunction, newName);
                    Save();
                    break;
                }
            }
        }
    }
}
