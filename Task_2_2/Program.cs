using System;
using System.Xml.Linq;

namespace Task_2_2
{
    abstract class Worker
    {
        protected string _name = "none";
        protected string _position = "none";
        protected string _workDay = "none";
        public Worker(string name)
        {
            _name = name; //*this
        }
        public void Call()
        {
        }
        public void WriteCode()
        {
        }
        public void Relax()
        {
        }
        public string getName()
        {
            return _name;
        }
        public string getPosition()
        {
            return _position;
        }
        public string getWorkDay()
        {
            return _workDay;
        }
        public void setWorkDay(string workDay)
        {
            _workDay = workDay;
        }
        public abstract void FillWorkDay();
    }

    class Developer : Worker
    {
        public Developer(string name) : base(name)
        {
            _position = "Розробник";
        }
        public override void FillWorkDay()
        {
            WriteCode();
            Call();
            Relax();
            WriteCode();
        }
    }

    class Manager : Worker
    {
        private Random _number = new Random();
        public Manager(string name) : base(name)
        {
            _position = "Менеджер";
        }
        public override void FillWorkDay()
        {
            _number = new Random();
            for (int i = 0; i < _number.Next(1, 11); i++)
            {
                Call();
            }
            Relax();
            for (int i = 0; i < _number.Next(1, 6); i++)
            {
                Call();
            }
        }
    }

    class Team
    {
        private string _nameOfTheTeam;
        private Random _number = new Random();
        public List<Worker> workers = new List<Worker>();
        public Team(string name)
        {
            _nameOfTheTeam = name;
        }
        public void AddWorker(Worker Person)
        {
            workers.Add(Person);
        }
        public void WriteTeamInfo()
        {
            Console.WriteLine($"\nКоманда: {_nameOfTheTeam}");
            int count = workers.Count();
            Console.WriteLine("Працівники:");
            for (int i = 0; i < count; i++)
            {
                
                Console.WriteLine($"{workers[i].getName()}");
            }
        }
        public void WriteDetailedTeamInfo()
        {
            Console.WriteLine($"\nКоманда:{_nameOfTheTeam}");
            int count = workers.Count();
            Console.WriteLine("Працівники:");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"{workers[i].getName()} - {workers[i].getPosition()} - {workers[i].getWorkDay()}");
            }
        }
        public string getTeamName()
        {
            return _nameOfTheTeam;
        }
    }

    class Program
    {
        public Team FindTeam(List<Team> teams)
        {
            Console.WriteLine("Введіть назву команди");
            string teamName = Console.ReadLine()!;
            int number = teams.Count();
            for (int i = 0; i < number; i++)
            {
                if (teams[i].getTeamName() == teamName)
                {
                   return teams[i];
                }

            }
#pragma warning disable CS8603 
            return null;
#pragma warning restore CS8603 
        }
        public Worker FindWorker(Team team)
        {
            Console.WriteLine("Введіть імʼя робітника");
            string nameOfWorker = Console.ReadLine()!;
            int numberOfWorkers = team.workers.Count();
            for (int j = 0; j < numberOfWorkers; j++)
            {
                if (team.workers[j].getName() == nameOfWorker)
                {
                    return team.workers[j];
                }

            }
#pragma warning disable CS8603 
            return null;
#pragma warning restore CS8603 
        }

        public void addWorker(Team team)
        {
            Console.WriteLine("Ким є співробітник? (Розробник/Менеджер)");
            string position = Console.ReadLine()!;
            bool cycle = true;
            do
            {

                if (position == "Розробник")
                {
                    Console.WriteLine("Введіть імʼя співробітника");
                    string workerName = Console.ReadLine()!;
                    team.AddWorker(new Developer(workerName));
                    cycle = false;
                }
                else if (position == "Менеджер")
                {
                    Console.WriteLine("Введіть імʼя співробітника");
                    string workerName = Console.ReadLine()!;
                    team.AddWorker(new Manager(workerName));
                    cycle = false;
                }
                else
                {
                    Console.WriteLine("Такої позиції немає, введіть ще раз");
                }
            }
            while (cycle);
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            List<Team> teams = new List<Team>();
            Team testTeam = new Team("testTeam");
            Console.WriteLine("Перелік дій:\n" +
                "Додати команду\n" +
                "Додати співробітника в існуючу команду\n" +
                "Додати WorkDay співробітника в існуючу команду\n" +
                "Вивести дані про команду\n" +
                "Вивести детальні дані про команду\n");
            bool controller;
            do
            {
 
                Console.WriteLine("Введіть дію з переліку вище");
                string command = Console.ReadLine()!;
                if (command == "Додати команду")
                {
                    Console.WriteLine("Введіть назву команди");
                    string name = Console.ReadLine()!;
                    teams.Add(new Team(name));
                }
                else if (command == "Додати співробітника в існуючу команду")
                {
                    
                    testTeam = program.FindTeam(teams);
                    if (testTeam != null)
                    {
                        program.addWorker(testTeam);
                    }
                    else
                    {
                        Console.WriteLine("Команду не знайдено");
                    }
                }
                else if (command == "Додати WorkDay співробітника в існуючу команду")
                {
                    testTeam = program.FindTeam(teams);
                    if (testTeam != null)
                    {
                        Worker testWorker = program.FindWorker(testTeam);
                        if (testWorker != null)
                        {
                            Console.WriteLine("Введіть WorkDay");
                            string workDay = Console.ReadLine()!;
                            testWorker.setWorkDay(workDay);
                        }
                        else
                        {
                            Console.WriteLine("Працівника не знайдено");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Команду не знайдено");
                    }
                }
                else if (command == "Вивести дані про команду")
                {
                    testTeam = program.FindTeam(teams);
                    if (testTeam != null)
                    {
                        testTeam.WriteTeamInfo();
                    }
                    else
                    {
                        Console.WriteLine("Команду не знайдено");
                    }
                }
                else if (command == "Вивести детальні дані про команду")
                {
                    testTeam = program.FindTeam(teams);
                    if (testTeam != null)
                    {
                        testTeam.WriteDetailedTeamInfo();
                    }
                    else
                    {
                        Console.WriteLine("Команду не знайдено");
                    }
                }
            else
                {
                    Console.WriteLine("Такої дії немає");
                }
                Console.WriteLine("Ввести дію? (true - якщо так, false - якщо ні)");
                controller = Convert.ToBoolean(Console.ReadLine());
            }
            while (controller);
        }
    }
}