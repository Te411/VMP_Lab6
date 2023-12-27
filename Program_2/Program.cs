//Представим, что у вас есть приложение для управления задачами. 
//В нем есть класс Task, который содержит информацию о задаче, такую как название, описание и статус (выполнена или нет). 

//Класс TaskManager отвечает за создание, обновление и удаление задач. Вам нужно создать делегат, 
//который позволяет другим классам подписываться на событие изменения статуса задачи. 
//Задача будет считаться выполненной, если ее статус изменится на "выполнена".

//Напишите метод с именем "CompleteTask" в классе TaskManager. 
//Этот метод должен принимать в качестве параметра объект Task и устанавливать его статус в "выполнена". 
//Затем вызовите делегат, чтобы уведомить всех подписчиков о том, что задача была выполнена.

//В другом классе, напишите метод с именем "TaskCompletedNotification", 
//который будет вызываться при событии изменения статуса задачи. 
//Этот метод должен принимать объект Task в качестве параметра и выводить сообщение о том, что задача выполнена.

//Наконец, создайте экземпляр класса TaskManager и добавьте несколько задач в список. 
//Затем подпишитесь на событие изменения статуса задачи, вызывая метод "TaskCompletedNotification" и добавьте его в делегат. 
//Затем вызовите метод "CompleteTask" для одной из задач и убедитесь, что сообщение о выполнении задачи выводится на консоль.


using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization.Configuration;
using static Program_2.TaskManager;

namespace Program_2
{

    public class Task
    {
        private string name;
        private string description;
        private bool status;

        public string Name
        {
            get => name;
        }

        public Task(string _name, string _description, bool _status = false)
        {
            name = _name;
            description = _description;
            status = _status;
        }

        public void ChangeOfStatus(bool _status) => status = _status;

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Task))
                return false;
            Task t = (Task)obj;
            return t.name == name && t.description == description && t.status == status;
        }
    }

    delegate void Message(Task t);
    public class TaskManager
    {
        public List<Task> task;

        public TaskManager() => task = new List<Task>();

        public void CreateTask(string _name, string _description)
        {
            task.Add(new Task(_name, _description));
            Console.WriteLine("Задача добавлена!");
            Console.WriteLine();
        }

        public void DeleteTask(string n, string d, bool s)
        {
            Task find = new Task(n, d, s);
            Task tmpsTasks = task.Find(x => x.Equals(find));
            if (tmpsTasks != null)
            {
                task.Remove(tmpsTasks);
                Console.WriteLine("Задача удалена!");
            }
            else
            {
                Console.WriteLine("Такая задача не найдена!");
            }
        }

        public void CompleteTask(Task tasksComplite)
        {
            Task tmpTask = task.Find(x => x.Equals(tasksComplite));
            tmpTask.ChangeOfStatus(true);
            Message msg = _TaskCompletedNotification.TaskCompletedNotification;
            msg(tmpTask);
        }
    }

        public class _TaskCompletedNotification
        {
            public static void TaskCompletedNotification(Task task)
            {
                Console.WriteLine($"Задача \"{task.Name}\" выполнена!");
            }
        }

        public class Program
        {
            static void Main(string[] args)
            {
                TaskManager taskManager = new TaskManager();
                string stringInfo = "";
                string stringTask = "";
                sbyte x = -1;
                do
                {
                    do
                    {
                        Console.WriteLine("Выбирети режим работы:");
                        Console.WriteLine("1. Добавить задачу");
                        Console.WriteLine("2. Пометить задачу выполненной");
                        Console.WriteLine("3. Удалить задачу");
                        Console.WriteLine("0. Завершение работы");
                        try
                        {
                            x = Convert.ToSByte(Console.ReadLine());
                            while (x < 0)
                            {
                                Console.WriteLine("Такой команды нет!");
                                Console.WriteLine();
                                x = Convert.ToSByte(Console.ReadLine());
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Некорректный ввод! Попробуйте еще раз!");
                            x = -1;
                        }
                        Console.WriteLine();
                    } while (x < 0);

                    switch (x)
                    {
                        case 1:
                            Console.Write("Введите задачу: ");
                            stringTask = Console.ReadLine();
                            Console.WriteLine();
                            Console.Write("Введите описание задачи: ");
                            stringInfo = Console.ReadLine();
                            Console.WriteLine();

                            taskManager.CreateTask(stringTask, stringInfo);
                            break;
                        case 2:
                            Console.Write("Введите задачу, которую надо поменить выполненной: ");
                            stringTask = Console.ReadLine();
                            Console.WriteLine();
                            Console.Write("Введите описание этой задачи: ");
                            stringInfo = Console.ReadLine();
                            Console.WriteLine();
                            taskManager.CompleteTask(new Task(stringTask, stringInfo));
                            break;
                        case 3:
                            bool boolStatus;
                            Console.Write("Введите задачу для удаления: ");
                            stringTask = Console.ReadLine();
                            Console.WriteLine();
                            Console.Write("Введите описание этой задачи: ");
                            stringInfo = Console.ReadLine();
                            Console.WriteLine();
                            Console.WriteLine("Введите статус этой задачи(true - выполнена, false - невыполнена): ");
                            boolStatus = Convert.ToBoolean(Console.ReadLine());
                            taskManager.DeleteTask(stringTask, stringInfo, boolStatus);
                            break;
                        default:
                            Console.WriteLine("Такой команды нет!");
                            Console.WriteLine();
                            break;
                    }
                } while (x != 0);
            }
        }
    
}