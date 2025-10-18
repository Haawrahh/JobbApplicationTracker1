using System;

namespace JobbApplicationTracker
{
    public class JobbApplication
    {

        //Attribut
        //beskriver en enda ansökan, om ett jobb jag har sökt
        public string CompanyName { get; set; } = ""; //Företagsnamn
        public string Position { get; set; } = "";   //Arbetsposition
        public Status ApplicationStatus { get; set; } = Status.Applied; //Status på ansökan t.ex . Applied, Interviewing, Offered, Accepted, Rejected
        public DateTime ApplicationDate { get; set; } = DateTime.Now; //Datum för ansökan
        public DateTime? ResponseDate { get; set; } = null; //Datum för svar (om tillämpligt)
        public int SalaryExpectation { get; set; } = 0; //Löneförväntning

        //Metoder 
        public int GetDaysSinceApplied()  //Beräknar antal dagar sedan ansökan skickades
        {
            return (DateTime.Now - ApplicationDate).Days; //datum nu - datum för ansökan
        }

        public string GetSummary() //Ger en sammanfattning av jobbansökan
        {
            return $"Company: {CompanyName}, Position: {Position}, Status: {ApplicationStatus}, Applied On: {ApplicationDate.ToShortDateString()}, Salary Expectation: {SalaryExpectation}"; //Returnerar en sträng med information om ansökan
        }
        //testa commit, första ändringen 
    }

}