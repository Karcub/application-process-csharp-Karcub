using System;
using System.Collections.Generic;
using System.Linq;
using Codecool.ApplicationProcess.Data;
using Codecool.ApplicationProcess.Entities;

namespace Codecool.ApplicationProcess
{
    /// <summary>
    /// Main application process program.
    /// </summary>
    public class Program
    {
        private static readonly Dictionary<string, City> _cities = new Dictionary<string, City>()
        {
            { "1", City.Miskolc },
            { "2", City.Budapest },
            { "3", City.Krakow },
            { "4", City.Warsaw },
            { "5", City.Bucharest },
        };

        private static IApplicationRepository _repo;

        /// <summary>
        /// The main entry point of ApplicationProcess.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            if (args.Length > 0 && args[0].Equals("xml"))
            {
                _repo = new XmlRepository();
            }
            else
            {
                _repo = new InMemoryRepository();
            }

            PrintMenu();
            SelectMenuItem();
        }

        private static void SelectMenuItem()
        {
            var chosenMenu = Console.ReadLine();
            switch (chosenMenu)
            {
                case "1":
                    GetAllMentorFromACity();
                    break;
                case "2":
                    GetAmountOfApplicants();
                    break;
                case "3":
                    GetMentorWithFavouriteLanuage();
                    break;
                case "4":
                    GetApplicantsOfMentor();
                    break;
                case "5":
                    GetStudentEmailList();
                    break;
                case "e":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid menu option, choose again.");
                    PrintMenu();
                    SelectMenuItem();
                    break;
            }
        }

        private static void GetStudentEmailList()
        {
            var emailList = _repo.GetAppliedStudentEmailList();
            foreach (var email in emailList)
            {
                Console.WriteLine(email);
            }
        }

        private static void GetApplicantsOfMentor()
        {
            Console.WriteLine("Please give mentor's first and last name: ");
            var mentor = Console.ReadLine();
            var applicants = _repo.GetApplicantsOf(mentor);
            foreach (var applicant in applicants)
            {
                Console.WriteLine(applicant);
            }
        }

        private static void GetMentorWithFavouriteLanuage()
        {
            Console.WriteLine("Please give language: ");
            var answer = Console.ReadLine();
            var mentors = _repo.GetAllMentorWhomFavoriteLanguage(answer);
            foreach (var mentor in mentors)
            {
                Console.WriteLine(mentor);
            }
        }

        private static void GetAmountOfApplicants()
        {
            Console.WriteLine("Please give start date:");
            var answer = Console.ReadLine();
            DateTime startDate;

            while (!DateTime.TryParse(answer, out startDate))
            {
                Console.WriteLine("Please give start date:");
                answer = Console.ReadLine();
            }

            var amount = _repo.AmountOfApplicationAfter(startDate);

            Console.WriteLine("Applications after this date: " + amount);
        }

        /// <summary>
        /// This method is asking for a City via console and based on that
        /// prints all the <see cref="Mentor"/>s who is working at that location.
        /// </summary>
        private static void GetAllMentorFromACity()
        {
            Console.WriteLine(@"Which city are you interested in:
1 - Miskolc
2 - Budapest
3 - Krakow
4 - Warsaw
5 - Bucharest");
            var city = Console.ReadLine();

            var mentors = _repo.GetAllMentorFrom(_cities[city]);

            foreach (var mentor in mentors)
            {
                Console.WriteLine(mentor);
            }
        }

        private static void PrintMenu()
        {
            Console.WriteLine(@"Welcome to the Codecool application system
---------------------
Please choose a menu item:
    1 - Get all mentors
    2 - Amount of applications
    3 - Whose favourite language is...
    4 - Get mentor's applicants
    5 - Get applied students email list
---------------------
If you want to exit press 'e'.");
        }
    }
}
