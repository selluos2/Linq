using Linq.Database;
using Linq.Models;
using Microsoft.EntityFrameworkCore;

namespace Linq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Oefenen met Linq");

            // ### Oefening 1: Alle voorbeelden ophalen
            // Haal alle voorbeelden op uit de database en toon de naam en beschrijving.
            Console.WriteLine("Oefening 1 Alle voorbeelden ophalen");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeelden = context.Voorbeelden.ToList();

                foreach (var voorbeeld in voorbeelden)
                {
                    Console.WriteLine($"Naam: {voorbeeld.Name}; Beschrijving: {voorbeeld.Description}");
                }

            }

            // ### Oefening 2: Filteren op rol
            // Haal alle voorbeelden op die de rol `Administrator` hebben.
            Console.WriteLine("\nOefening 2 Filteren op rol");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeelden = context.Voorbeelden
                                    .Where(c => c.Role.Equals(Role.Administrator))
                                    .ToList();
                foreach (var voorbeeld in voorbeelden)
                {
                    Console.WriteLine($"Naam: {voorbeeld.Name} - Role: {voorbeeld.Role}");
                }
            }

            // ### Oefening 3: Haal alle voorbeelden op en sorteer ze op alfabetisch op naam
            Console.WriteLine("\nOefening 3 Sorteren op naam");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeelden = context.Voorbeelden
                                    .OrderBy(c => c.Name)
                                    .ToList();
                foreach (var voorbeeld in voorbeelden)
                {
                    Console.WriteLine($"Naam: {voorbeeld.Name}");
                }
            }

            // ### Oefening 4: Tel hoeveel uitwerkingen er in totaal zijn in de database.
            Console.WriteLine("\nOefening 4 Tellen van uitwerkingen");
            using (var context = new VoorbeeldDBContext())
            {
                var totaalUitwerkingen = context.Uitwerkingen.Count();
                Console.WriteLine($"Totaal aantal uitwerkingen: {totaalUitwerkingen}");
            }

            // ### Oefening 5: Haal alle uitwerkingen op waar het aantal pogingen (Tries) groter is dan 10.
            Console.WriteLine("\nOefening 5 Uitwerkingen met meer dan 10 pogingen");
            using (var context = new VoorbeeldDBContext())
            {
                var uitwerkingen = context.Uitwerkingen
                                    .Where(u => u.Tries > 10)
                                    .ToList();
                foreach (var uitwerking in uitwerkingen)
                {
                    Console.WriteLine($"Uitwerking ID: {uitwerking.Id} - Pogingen: {uitwerking.Tries}");
                }
            }

            // ### Oefening 6: Haal het eerste voorbeeld op met de naam "LINQ Select Query".
            Console.WriteLine("\nOefening 6 Eerste voorbeeld met naam 'LINQ Select Query'");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeeld = context.Voorbeelden
                                    .FirstOrDefault(v => v.Name == "LINQ Select Query");
                if (voorbeeld != null)
                {
                    Console.WriteLine($"Gevonden Voorbeeld - ID: {voorbeeld.Id}, Naam: {voorbeeld.Name}");
                }
            }

            // ### Oefening 7: Bereken het gemiddelde aantal pogingen (Tries) van alle uitwerkingen.
            Console.WriteLine("\nOefening 7: Bereken het gemiddelde aantal pogingen");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeeld = context.Uitwerkingen
                                    .Average(o => o.Tries);
                Console.WriteLine($"Het gemiddelde is: {voorbeeld}");
            }

            // ### Oefening 8: Vind het hoogste en laagste aantal pogingen (Tries) bij uitwerkingen.
            Console.WriteLine("\nOefening 8: vind het hoogste en laagste aantal pogingen");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeeldmin = context.Uitwerkingen
                                    .Min(o => o.Tries);
                var voorbeeldmax = context.Uitwerkingen
                                    .Max(o => o.Tries);
                Console.WriteLine($"Max: {voorbeeldmax} Min: {voorbeeldmin}");
            }

            // ### Oefening 9: Bereken de totale som van het Count veld van alle voorbeelden.
            Console.WriteLine("\nOefening 9: Bereken de totale som van het Count veld");
            using (var context = new VoorbeeldDBContext())
            {
                var totaalCount = context.Voorbeelden
                                    .Sum(o => o.Count);
                Console.WriteLine($"Totale som van Count: {totaalCount}");

            }

            // ### Oefening 10: Groepeer alle voorbeelden op Role en tel hoeveel voorbeelden er per rol zijn.
            Console.WriteLine("\nOefening 10: Groepeer voorbeelden op Role");
            using (var context = new VoorbeeldDBContext())
            {
                var Roles = context.Voorbeelden
                                    .GroupBy(o => o.Role)
                                    .Select(gr => new
                                    {
                                        Role = gr.Key,
                                        Amount = gr.Count()
                                    });
                foreach (var role in Roles)
                {
                    Console.WriteLine($"Aantal = {role.Amount}");
                }
            }

            // Oefening 11: Haal alle voorbeelden op en include de bijbehorende uitwerkingen (navigation property).
            Console.WriteLine("\nOefening 11: Include bijbehorende uitwerkingen");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeelden = context.Voorbeelden
                            .Include(u => u.Uitwerkingen)
                            .ToList();
                foreach (var voorbeeld in voorbeelden)
                {
                    Console.WriteLine($"Voorbeeld = {voorbeeld.Name}");
                    foreach (var uitwerking in voorbeeld.Uitwerkingen)
                    {
                        Console.WriteLine($"Uitwerking: {uitwerking.Id}");
                    }
                }
            }

            // Oefening 12: Haal alle voorbeelden op met Role User en include de bijbehorende uitwerkingen.
            Console.WriteLine("\nOefening 12: Role user met uitwerkingen");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeelden = context.Voorbeelden
                            .Where(r => r.Role.Equals(Role.User))
                            .Include(u => u.Uitwerkingen)
                            .ToList();
                foreach (var voorbeeld in voorbeelden)
                {
                    Console.WriteLine($"Voorbeeld = {voorbeeld.Name} - Role = {voorbeeld.Role}");
                    foreach (var uitwerking in voorbeeld.Uitwerkingen)
                    {
                        Console.WriteLine($"Uitwerking: {uitwerking.Id}");
                    }
                }
            }

            // Oefening 13: Haal alle voorbeelden op maar selecteer alleen de Id en Name (anoniem type of nieuwe klasse).
            Console.WriteLine("\nOefening 13: alleen id en naam");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeelden = context.Voorbeelden
                            .Select(o => new { o.Name, o.Id });
                foreach (var voorbeeld in voorbeelden)
                {
                    Console.WriteLine($"Name: {voorbeeld.Name}, Id: {voorbeeld.Id}");
                }
            }

            // Oefening 14: Haal alle voorbeelden op waar Count groter is dan 50 EN Role is Moderator of Administrator.
            Console.WriteLine("\nOefening 14: Count > 50 en role mod/admin");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeelden = context.Voorbeelden
                            .Where(o => o.Count > 50 && (o.Role == Role.Moderator || o.Role == Role.Administrator))
                            .ToList();
                foreach (var voorbeeld in voorbeelden)
                {
                    Console.WriteLine($"Name: {voorbeeld.Name}, Count: {voorbeeld.Count}, Role: {voorbeeld.Role}");
                }
            }

            // Oefening 15: Sorteer alle voorbeelden eerst op Role en dan op Name.
            Console.WriteLine("\nOefening 15: Sorteer op Role en dan op Name");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeelden = context.Voorbeelden
                            .OrderBy(o => o.Role)
                            .ThenBy(o => o.Name)
                            .ToList();
                foreach (var voorbeeld in voorbeelden)
                {
                    Console.WriteLine($"Name: {voorbeeld.Name}, Role: {voorbeeld.Role}");
                }
            }

            // Oefening 16: Controleer of er uitwerkingen zijn van de eigenaar "John Smith".
            Console.WriteLine("\nOefening 16: Controleer op eigenaar John Smith");
            using (var context = new VoorbeeldDBContext())
            {
                var bestaat = context.Uitwerkingen
                            .Any(o => o.Owner == "John Smith");
                Console.WriteLine($"Zijn er uitwerkingen van John Smith? {bestaat}");
            }

            // Oefening 17: Controleer of alle uitwerkingen minimaal 1 poging (Tries) hebben.
            Console.WriteLine("\nOefening 17: Controleer of alle uitwerkingen minimaal 1 poging hebben");
            using (var context = new VoorbeeldDBContext())
            {
                var alleMinimaalEénPoging = context.Uitwerkingen
                            .All(o => o.Tries >= 1);
                Console.WriteLine($"Hebben alle uitwerkingen minimaal 1 poging? {alleMinimaalEénPoging}");
            }

            //Oefening 18: Haal een lijst op met alle unieke eigenaren (Owner) uit de Uitwerkingen tabel.
            Console.WriteLine("\nOefening 18: Unieke eigenaren uit Uitwerkingen");
            using (var context = new VoorbeeldDBContext())
            {
                var uniekeEigenaren = context.Uitwerkingen
                            .Select(o => o.Owner)
                            .Distinct()
                            .ToList();
                foreach (var eigenaar in uniekeEigenaren)
                {
                    Console.WriteLine($"Eigenaar: {eigenaar}");
                }
            }

            // Oefening 19: Groepeer uitwerkingen op Owner en toon per eigenaar hoeveel uitwerkingen ze hebben gemaakt.
            Console.WriteLine("\nOefening 19: Groepeer uitwerkingen op Owner");
            using (var context = new VoorbeeldDBContext())
            {
                var uitwerkingenPerEigenaar = context.Uitwerkingen
                            .GroupBy(o => o.Owner)
                            .Select(gr => new
                            {
                                Owner = gr.Key,
                                Amount = gr.Count()
                            });
                foreach (var eigenaar in uitwerkingenPerEigenaar)
                {
                    Console.WriteLine($"Eigenaar: {eigenaar.Owner}, Aantal uitwerkingen: {eigenaar.Amount}");
                }
            }

            // Oefening 20: Haal alle voorbeelden op waarvan de Description het woord "query" bevat (case-insensitive).
            Console.WriteLine("\nOefening 20: Voorbeelden met 'query' in de beschrijving");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeelden = context.Voorbeelden
                            .Where(o => o.Description.ToLower().Contains("query"))
                            .ToList();
                foreach (var voorbeeld in voorbeelden)
                {
                    Console.WriteLine($"Name: {voorbeeld.Name}, Description: {voorbeeld.Description}");
                }
            }

            // Oefening 21: Haal alle voorbeelden op met Role Administrator of SuperAdministrator, sorteer op Count (aflopend), en include de uitwerkingen waarvan Tries groter is dan 5.
            Console.WriteLine("\nOefening 21: Admins en SuperAdmins met uitwerkingen > 5 tries");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeelden = context.Voorbeelden
                                  .Where(o => o.Role == Role.Administrator || o.Role == Role.SuperAdministrator)
                                  .OrderByDescending(o => o.Count)
                                  .Include(o => o.Uitwerkingen.Where(o => o.Tries > 5));
                foreach(var voorbeeld in voorbeelden)
                {
                    Console.WriteLine($"Name = {voorbeeld.Name} - Role = {voorbeeld.Role}");
                    foreach(var uitwerking in voorbeeld.Uitwerkingen)
                    {
                        Console.WriteLine($"Id = {uitwerking.Id} - Tries = {uitwerking.Tries}");
                    }
                }     
            }
        }
    }
}