using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;   //För LINQ funktioner  (Where, FirstOrDefault, GroupBy, Average)

namespace JobbApplicationTracker
{
    //Summary 
    //JobbManager är hjärnan som sköter listan med alla jobbansökningar
    //Metod för att lägga till, visa, uppdatera, ta bort
    //Filtrera (LINQ) och visa statistik (GroupBy, Average)


    public class JobbManager
    {
        //Attribut
        //Styr listan om alla ansökningar jag har gjort
        public List<JobbApplication> applications { get; set; } = new List<JobbApplication>(); //Lista för att spara alla jobbansökningar

        //Metoder
        //Lägger till en ny jobbansökan i listan
        public void AddApplication(JobbApplication application) //Lägger till en ny jobbansökan i listan
        {
            applications.Add(application);
            Console.WriteLine("Ny ansökan tillagd.");
        }
        //Visa alla jobbansökningar som finns i listan
        public void ShowAllApplications()
        {
            if (applications.Count == 0)
            {
                Console.WriteLine("Inga ansökningar finns ännu.");
                return; // Avsluta metoden om listan är tom
            }
            Console.WriteLine("\n--- Alla ansökningar ---");

            //Loopar igenom alla jobbansökningar i listan och skriver ut deras sammanfattning
            foreach (var app in applications)
            {
                Console.WriteLine(app.GetSummary());
            }

        }
        //Uppdateringansökan , ändrar status på en viss ansökan 
        //LINQ för att hitta rätt ansökan via företagsnamn
        //LINQ firstDefault för att hitta den första ansökan som matchar företagsnamnet

        public void UpdateApplicationStatus(string companyName, Status newStatus)
        {
            var application = applications.FirstOrDefault(a =>
            a.CompanyName.Equals(companyName, StringComparison.OrdinalIgnoreCase));
           
            if (application == null)
            {

                application.ApplicationStatus = newStatus;
                Console.WriteLine("Hittade ingen ansökan med det företagnamnet.");
                return;
            }
           
                application.ApplicationStatus = newStatus;
            Console.WriteLine("Ansökans status uppdaterad");
        }
        
        //Ta bort ansökan
        public void RemoveApplication(string companyName)
        {
            var application = applications.FirstOrDefault(a =>
            a.CompanyName.Equals(companyName, StringComparison.OrdinalIgnoreCase));

            if (application == null)
            {
                applications.Remove(application);
                Console.WriteLine("Hittade ingen ansökan med det företagsnamnet.");
                return;
            }
               applications.Remove(application);
            Console.WriteLine("Ansökan borttagen!");
            }


        //LINQ DEL
        //Filtrerar ansökningar baserat på status t.ex. intervju eller rejected
        //LINQ Where för att filtrera listan
        public void ShowByStatus(Status status)
        {
            var filtered = applications.Where(a => a.ApplicationStatus == status).ToList();
            if (filtered.Count == 0)
            {
                Console.WriteLine($"Ingen ansökning med status: {status}");
                return;
            }
            Console.WriteLine($"\n--- Ansökning med status: {status} ---");
            foreach (var app in filtered)
            {
                Console.WriteLine(app.GetSummary());
            }
        }
        //Statistik: total antal ansökningar, antal per status och genomsnittlig svarstid
        //LINQ GroupBy och Average
        public void ShowStatistics()
        {
            Console.WriteLine("\n--- Statistik ---");
            //Totalt antal ansökningar
            Console.WriteLine($"Totalt antal ansökningar: {applications.Count}");

            //Antal ansökningar per status
            var perStatus = applications
                .GroupBy(a => a.ApplicationStatus)
                .Select(g => new { Status = g.Key, Count = g.Count() });

            foreach (var row in perStatus)
                Console.WriteLine($"{row.Status}: {row.Count}");

            //Genomsnittlig svarstid (endast för de som har responsDate)
            var respondedDays = applications
                               .Where(a => a.ResponseDate.HasValue)
                .Select(a => (a.ResponseDate.Value - a.ApplicationDate).Days);

            if (respondedDays.Any())
                Console.WriteLine($"Genomsnittlig svarstid: {respondedDays.Average():0.0} dagar");
            else
                Console.WriteLine("Ingen ansökan har fått svar än.");
        }

        // lägg till några testansökningar automatiskt
        //lägg till tre ansökningar (klarna, ikea, volvo)
        public void Seed()
        { 
            applications.AddRange(new[]
            {
                new JobbApplication
                {
                    CompanyName = "Klarna AB",
                    Position = "Junior .NET Developer",
                    ApplicationStatus = Status.Applied,
                    ApplicationDate = DateTime.Now.AddDays(-12),
                    SalaryExpectation = 35000
                },
                new JobbApplication
                {
                    CompanyName = "IKEA",
                    Position = "IT Support Specialist",
                    ApplicationStatus = Status.Interviewing,
                    ApplicationDate = DateTime.Now.AddDays(-25),
                    ResponseDate = DateTime.Now.AddDays(-8),
                    SalaryExpectation = 32000
                },
                new JobbApplication
                {
                    CompanyName = "Volvo Cars",
                    Position = "Backend Developer Intern",
                    ApplicationStatus = Status.Offered,
                    ApplicationDate = DateTime.Now.AddDays(-30),
                    ResponseDate = DateTime.Now.AddDays(-5),
                    SalaryExpectation = 36000
                },
            });
        }

        internal void AddJob(JobbApplication jobbApplication)
        {
            throw new NotImplementedException();
        }
    }
   
}
    

