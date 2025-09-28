using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PublicTransit.Common.Data;
using PublicTransit.Common.Events;
using PublicTransit.Common.Extensions;
using PublicTransit.Common.Models;
using PublicTransit.Common.Persons;

namespace PublicTransit.Console
{
    public static class EntryPoint
    {
        public static async Task Main()
        {
            ICrudService<Person> personService = new CrudList<Person>();

            var knight = Knight.Create(new Info("Arthur", "Pendragon", 1500), new Stats(35, 120, 25));
            var berserker = Berserker.Create(new Info("Grog", "Strongjaw", 800), new Stats(30, 180, 20));
            var mage = Mage.Create(new Info("Gale", "Dekarios", 2000), new Stats(40, 80, 15));

            knight.OnAction += Person_OnAction;
            berserker.OnAction += Person_OnAction;
            mage.OnAction += Person_OnAction;

            personService.Create(knight);
            personService.Create(berserker);
            personService.Create(mage);

            System.Console.WriteLine("\n--- Initial list of persons ---");
            PrintAllPersons(personService);

            System.Console.WriteLine($"\nTotal persons created so far: {Person.PersonCount}");

            System.Console.WriteLine("\n--- Battle interactions ---");
            berserker.GetDamage(knight.PersonStats.Damage);
            knight.HealYourself(10);
            await mage.CastDebuffSpell(5);

            System.Console.WriteLine($"\nIs the Mage still alive after casting the spell? {mage.IsAlive()}");

            System.Console.WriteLine("\n--- Updating Berserker's salary ---");
            var berserkerEntry = ((CrudList<Person>)personService).Map.First(kvp => kvp.Value == berserker);
            var updatedBerserker = Berserker.Create(
                new Info(berserker.PersonInfo.FirstName, berserker.PersonInfo.LastName, 950), 
                berserker.PersonStats
            );
            personService.Update(berserkerEntry.Key, updatedBerserker);
            
            System.Console.WriteLine("\n--- List after update ---");
            PrintAllPersons(personService);

            string filePath = "persons.json";
            System.Console.WriteLine($"\n--- Saving persons to {filePath} ---");
            personService.Save(filePath);

            System.Console.WriteLine($"\n--- Loading persons from {filePath} ---");
            ICrudService<Person> loadedPersonService = new CrudList<Person>().Load(filePath);
            System.Console.WriteLine("\n--- Loaded list of persons ---");
            PrintAllPersons(loadedPersonService);

            System.Console.WriteLine("\n--- Removing the Knight ---");
            personService.Remove(knight);
            System.Console.WriteLine("\n--- Final list of persons ---");
            PrintAllPersons(personService);
        }

        private static void Person_OnAction(object sender, ActionEventArgs e)
        {
            System.Console.WriteLine($"[LOG] {e.Message}");
        }

        private static void PrintAllPersons(ICrudService<Person> service)
        {
            var persons = service.ReadAll();
            if (!persons.Any())
            {
                System.Console.WriteLine("The list is empty.");
                return;
            }
            foreach (var person in persons)
            {
                System.Console.WriteLine(person.ToString());
            }
        }
    }
}
