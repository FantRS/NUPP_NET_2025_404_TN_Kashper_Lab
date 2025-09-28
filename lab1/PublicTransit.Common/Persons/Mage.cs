using System.Threading.Tasks;
using PublicTransit.Common.Models;

namespace PublicTransit.Common.Persons
{
    public class Mage : Person
    {
        public int DebuffSecs { get; } = 5;
        public int DebuffValue { get; } = 1;

        private Mage(Info personInfo, Stats personStats) : base(personInfo, personStats) { }

        // === СТВОРЕННЯ ОБ'ЄКТА ЧЕРЕЗ СТАТИЧНИЙ МЕТОД ===
        public static Mage Create(Info personInfo, Stats personStats)
        {
            return new Mage(personInfo, personStats);
        }

        // === МЕТОД ДЛЯ ОТРИМАННЯ ПОШКОДЖЕННЯ ===
        public override void GetDamage(int damage)
        {
            var stats = PersonStats;
            var healthIncr = stats.Health - damage;
            stats.Health = healthIncr > 0 ? healthIncr : 0;
            PersonStats = stats;
            RaiseActionEvent($"Mage {PersonInfo.FirstName} takes {damage} damage. Current health: {PersonStats.Health}");
        }

        // === МЕТОД ДЛЯ САМОПОШКОДЖЕННЯ ===
        public async Task CastDebuffSpell(int value)
        {
            RaiseActionEvent($"Mage {PersonInfo.FirstName} started casting a debuff spell...");
            var stats = PersonStats;
            int counter = 0;
            while (counter != DebuffSecs)
            {
                stats.Health -= value * 2;
                counter++;
                PersonStats = stats;
                await Task.Delay(1000);
                RaiseActionEvent($"...spell continues, health is now {PersonStats.Health} hp...");
            }
            RaiseActionEvent($"...spell finished.");
        }
    }
}
