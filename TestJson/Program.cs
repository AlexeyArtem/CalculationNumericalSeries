using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace TestJson
{
    class Program
    {
        static void Main(string[] args)
        {
            Project project = new Project("Первый проект");
            project.Functions.Add("1/n", "Степенная функция ряда");
            project.Functions.Add("1/n^2", "Степенная сходящаяся функция ряда");

            Project project1 = new Project("Второй проект");
            project1.Functions.Add("1/n", "Степенная функция ряда1");
            project1.Functions.Add("1/n^2", "Степенная сходящаяся функция ряда1");

            Project project2 = new Project("Второй проект");
            project2.Functions.Add("1/n", "Степенная функция ряда2");
            project2.Functions.Add("1/n^2", "Степенная сходящаяся функция ряда2");

            ObservableCollection<Project> projects = new ObservableCollection<Project>() { project, project1, project2 };

            string json = JsonConvert.SerializeObject(projects);
            ObservableCollection<Project> prs = JsonConvert.DeserializeObject<ObservableCollection<Project>>(json);


            Console.WriteLine("После сериализации:");
            Console.WriteLine(json);

            Console.WriteLine("После десериализации:");
            for (int i = 0; i < prs.Count; i++) 
            {
                Console.WriteLine("Название проекта: " + prs[i].Name);
                foreach (string key in prs[i].Functions.Keys)
                    Console.WriteLine("Функция: {0}, название: {1}", key, prs[i].Functions[key]);
            }

            Console.ReadLine();
        }
    }
}
