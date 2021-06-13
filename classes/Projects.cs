using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using NSwag.Collections;

namespace CalculationNumericalSeries
{
    static class Projects
    {
        private static readonly string nameFile = "projects.json";
        private static ObservableCollection<Project> listProjects;
        private static Project currentProject;

        static Projects() 
        {
            using (FileStream file = new FileStream(nameFile, FileMode.OpenOrCreate))
            {
                byte[] array = new byte[file.Length];
                file.Read(array, 0, array.Length);
                string jsonText = Encoding.Default.GetString(array);

                List<Project> list = JsonConvert.DeserializeObject<List<Project>>(jsonText);
                listProjects = JsonConvert.DeserializeObject<ObservableCollection<Project>>(jsonText);
            }

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

            foreach (Project p in listProjects)
                p.ChangingFunctions += FunctionsChangeHandler;

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
                if (listProjects.Contains(value)) currentProject = value;
            }
        }

        public static ReadOnlyObservableCollection<Project> ListProjects { get; }

        private static void FunctionsChangeHandler(object sender, EventArgs args) 
        {
            Save();
        }

        private static void Save() 
        {
            string json = JsonConvert.SerializeObject(listProjects);
            File.WriteAllText(nameFile, json, Encoding.Default);
        }

        public static void Add(string name) 
        {
            listProjects.Add(new Project(name));
            Save();
        }

        public static void Remove(Project project) 
        {
            if (listProjects.Contains(project) && listProjects.Count < 2) throw new Exception("Нельзя удалить последний проект");

            listProjects.Remove(project);
            if (CurrentProject == project) CurrentProject = listProjects[0];
            Save();
        }
    }
}
