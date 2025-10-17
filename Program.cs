using JobbApplicationTracker;
using System;
using System.Globalization;
using System.Linq;

{
    //Färgad rubrik när programmet startar 
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("================================");
        Console.WriteLine("=== Job Application Tracker ===");
        Console.WriteLine("================================");
        Console.ResetColor();
        Console.WriteLine();//Tom rad innan menyn visas

    CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("sv-SE");

//skapa ett nytt jobmanager klass 
//som ska hantera alla jobbansökningar, lägger till visa, uppdatera och ta bort ansökningar
var manager = new JobbManager();

//Anropar Seed () för att lägga till test ansökningar automatiskt
// Ikea , Volvo och Klarna
manager.Seed();

//En som körs i en loop tills användaren väljer att avsluta programmet
while (true)
{
    //Visa menyn
    Console.WriteLine("\nJob Application Tracker");
    Console.WriteLine("1. Lägg till ansökan");
    Console.WriteLine("2. Visa alla ansökan");
    Console.WriteLine("3. Filtrera ansökan");
    Console.WriteLine("4. Sortera ansökningar efter datum(OrderBy)");
    Console.WriteLine("5. Visa Statistik(GroupBy / Average)");
    Console.Write     ("6. Uppdatera status (ange företagsnamn):");
    Console.WriteLine("7.Ta bort ansökan (ange företagsnamn):");
    Console.WriteLine("8. Avsluta");
    Console.Write("Välj ett alternativ (1-8): ");

    string? choice = Console.ReadLine();
    
    //Hantera användarens val
    switch (choice)
    {
        case "1":
            //Lägg till en ny jobbansökan
           AddFlow (manager);
            break;

            case "2":
            //Visa alla jobbansökningar
            manager.ShowAllApplications();
            break;

            case "3":
            //Filtrera ansökningar baserat på status
            var s = AskStatus("Välj status att filtrera på");
            manager.ShowByStatus(s);
            break;

            case "4":
            //Sortera ansökningar efter datum
            Console.WriteLine("\n--- Sorterade efter datum (äldst först) ---");
            foreach (var app in manager.applications.OrderBy(a => a.ApplicationDate))
             Console.WriteLine(app.GetSummary());
            break;

            case "5":
            //Visa statistik om ansökningarna
            manager.ShowStatistics();
            break;

            case "6":
            //Uppdatera status för en ansökan baserat på företagsnamn
            Console.Write("Ange företagsnamn för att uppdatera status: ");
            var cname = Console.ReadLine() ?? "";
            var newStatus = AskStatus("Ny status");
            manager.UpdateApplicationStatus(cname, newStatus);
            break;

            case "7":
            //Ta bort en ansökan baserat på företagsnamn
            Console.Write("Ange företagsnamn för att ta bort ansökan: ");
            var rname = Console.ReadLine() ?? "";
            manager.RemoveApplication(rname);
            break;


            case "8":
            //Avsluta programmet
            Console.WriteLine("Exiting...");
            return;

            default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;

    }
} //Stänger while loopen

// === Hjälpmetoder (ska ligga UTANFÖR while ) ===

static Status AskStatus (string prompt)
{
    Console.WriteLine($"{prompt} (1. Applied, 2. Interviewing, 3. Offered, 4. Accepted, 5. Rejected): ");
    while (true)
    {
        if (int.TryParse(Console.ReadLine(), out int n) && n >= 1 && n <= 5)
            return (Status)n;
        
           Console.WriteLine("Fel input. Skriv 1-5: ");

    }
}

static DateTime AskDate (string prompt)
{
    Console.WriteLine(prompt);
    while (true)
    {
        if (DateTime.TryParse(Console.ReadLine(), out var dt))
            return dt.Date;
        Console.Write("Fel datumformat. Använd YYYY-MM-DD. Försök igen: ");
    }
}

static int AskInt (string prompt)
{
    Console.WriteLine($"{prompt}:");
    while (true)
    {
        if (int.TryParse(Console.ReadLine(), out int n) && n >= 0)
            return n;

        Console.Write ("Måste vara ett heltal >= 0. Försök igen: ");
    }
}

void AddFlow (JobbManager manager)
    {
        Console.Write("Företag: ");
        string companyName = Console.ReadLine() ?? string.Empty;

        Console.Write("Tjänst: ");
        string position = Console.ReadLine() ?? string.Empty;

        Status status = AskStatus("Status");
        DateTime appDate = AskDate("Ansökan skickad");
        int salary = AskInt("Önskad lön (0 om okänt) ");

        string company = null;
        //skapa objekt - Egenskapsnamnen måste matcha exakt med min klass
        manager.AddJob(new JobbApplication
        {
            CompanyName = company,
            Position = position,
            ApplicationStatus = status,
            ApplicationDate = appDate,
            SalaryExpectation = salary
        });
        Console.WriteLine("Ansökan tillagd.");
    }
} 



