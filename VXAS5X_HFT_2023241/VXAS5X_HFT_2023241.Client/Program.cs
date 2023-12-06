using System;
using System.Threading;
using VXAS5X_HFT_2023241.Models;

namespace VXAS5X_HFT_2023241.Client
{
    internal class Program
    {
        static RestService restService = new RestService("http://localhost:60462");
        static void Main(string[] args)
        {

            Thread.Sleep(8000);
            bool stopIt = false;
            while (!stopIt)
            {
                Menu(ref stopIt);
            }
        }
        public static void Menu(ref bool stopIt)
        {
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("1. Stageplays");
            Console.WriteLine("2. Dramaturgs");
            Console.WriteLine("3. Actors List");
            Console.WriteLine("4. Queries");
            Console.WriteLine("5. Post");
            Console.WriteLine("6. Put");
            Console.WriteLine("7. Delete");
            Console.WriteLine("0. Quit");

            var stagePlay = restService.Get<StagePlay>("StagePlay");
            var dramaturgs = restService.Get<Dramaturg>("Dramaturg");
            var actors = restService.Get<Actor>("Actor");

            int menuInput = Convert.ToInt32(Console.ReadLine());
            var q1 = restService.Get<StagePlay>("query/One");
            var q2 = restService.Get<StagePlay>("query/Two");
            var q3 = restService.Get<StagePlay>("query/Three");
            var q4 = restService.Get<StagePlay>("query/Four");
            var q5 = restService.Get<StagePlay>("query/Five");
            var q6 = restService.GetSingle<int>("query/Six");
            var q7 = restService.GetSingle<double>("query/Seven");

            switch (menuInput)
            {
                case 1:
                    foreach (var item in stagePlay)
                    {
                        Console.WriteLine("Stageplay ID: " + item.Id);
                        Console.WriteLine("Stageplay Profit: " + item.Profit);
                        Console.WriteLine("Stageplay Premier: " + item.Premier);
                        Console.WriteLine("Stageplay Rating: " + item.Rating);
                        Console.WriteLine("Stageplay Title: " + item.Title);
                        Console.WriteLine("Stageplay Dramaturg ID: " + item.DramaturgId);
                    }
                    break;
                case 2:
                    foreach (var item in dramaturgs)
                    {
                        Console.WriteLine("Dramaturg ID: " + item.Id);
                        Console.WriteLine("Dramaturg Name: " + item.Name);
                        Console.WriteLine("Dramaturg Gender: " + item.Gender);
                        Console.WriteLine("Dramaturg Age: " + item.Age);
                    }
                    break;
                case 3:
                    foreach (var item in actors)
                    {
                        Console.WriteLine("Actor ID: " + item.Id);
                        Console.WriteLine("Actor Name: " + item.Name);
                        Console.WriteLine("Actor Age: " + item.Age);
                        Console.WriteLine("Actor Gender: " + item.Gender);
                        Console.WriteLine("Actor Role: " + item.Role);
                    }
                    break;
                case 4:
                    Console.WriteLine("Balla Bela as Draamaturg: ");
                    foreach (var item in q1)
                    {
                        Console.WriteLine("Stageplay title: " + item.Title);
                        Console.WriteLine("Stageplay Rating: " + item.Rating);
                        Console.WriteLine("Stageplay Profit " + item.Profit);
                        Console.WriteLine("Stageplay Premier " + item.Premier);
                        Console.WriteLine("Stageplay Dramaturg ID " + item.DramaturgId);
                        Console.WriteLine();
                    }
                    Console.WriteLine("Exceptional stagePlays");
                    foreach (var item in q2)
                    {
                        Console.WriteLine("Stageplay title: " + item.Title);
                        Console.WriteLine("Stageplay Rating: " + item.Rating);
                        Console.WriteLine("Stageplay Profit: " + item.Profit);
                        Console.WriteLine("Stageplay Premier: " + item.Premier);
                        Console.WriteLine("Stageplay Dramaturg ID: " + item.DramaturgId);
                        Console.WriteLine();
                    }
                    Console.WriteLine("Premiered After 2010");
                    foreach (var item in q3)
                    {
                        Console.WriteLine("Stageplay title: " + item.Title);
                        Console.WriteLine("Stageplay Rating: " + item.Rating);
                        Console.WriteLine("Stageplay Profit: " + item.Profit);
                        Console.WriteLine("Stageplay Premier: " + item.Premier);
                        Console.WriteLine("Stageplay Dramaturg ID: " + item.DramaturgId);
                        Console.WriteLine();
                    }
                    Console.WriteLine("Sorrible Success Stage Plays");
                    foreach (var item in q4)
                    {
                        Console.WriteLine("Stageplay title: " + item.Title);
                        Console.WriteLine("Stageplay Rating: " + item.Rating);
                        Console.WriteLine("Stageplay Profit: " + item.Profit);
                        Console.WriteLine("Stageplay Premier: " + item.Premier);
                        Console.WriteLine("Stageplay Dramaturg ID: " + item.DramaturgId);
                        Console.WriteLine();
                    }
                    Console.WriteLine("Not Horrible Tükör a Tóban Stageplays");
                    foreach (var item in q5)
                    {
                        Console.WriteLine("Stageplay title: " + item.Title);
                        Console.WriteLine("Stageplay Rating: " + item.Rating);
                        Console.WriteLine("Stageplay Profit: " + item.Profit);
                        Console.WriteLine("Stageplay Premier: " + item.Premier);
                        Console.WriteLine("Stageplay Dramaturg ID: " + item.DramaturgId);
                        Console.WriteLine();
                    }

                    Console.WriteLine("Good stagePlay " + q6);
                    Console.WriteLine("Avarage Profit: " + q7);
                    break;
                case 5:
                    Console.WriteLine("Select an actor to post!");
                    Console.WriteLine("Name: ");
                    string actorName = Console.ReadLine();
                    Console.WriteLine("Age:");
                    int actorAge = Convert.ToInt32(Console.ReadLine());
                    restService.Post(new Actor()
                    {
                        Name = actorName,
                        Age = actorAge
                    }, "Actor");
                    break;
                case 6:
                    Console.WriteLine("Actor to update?");
                    Console.WriteLine("Id:");
                    int aId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Name:");
                    string toUpdateName = Console.ReadLine();
                    Console.WriteLine("Age:");
                    int toUpdateAge = Convert.ToInt32(Console.ReadLine());
                    restService.Put(new Actor()
                    {
                        Id = aId,
                        Name = toUpdateName,
                        Age = toUpdateAge
                    }, "Actor");
                    break;
                case 7:
                    Console.WriteLine("Actor to delete?");
                    int toDelete = Convert.ToInt32(Console.ReadLine());
                    restService.Delete<Actor>(toDelete, "Actor");
                    break;
                case 0:
                    stopIt = true;
                    break;
                default:
                    break;
            }

        }
    }
}
