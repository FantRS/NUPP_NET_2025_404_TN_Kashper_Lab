using PublicTransit.Common.Models;

namespace PublicTransit.Common.Persons
{
    public class Berserker : Person
    {
        public int BonusDamage { get; } = 10;

        private Berserker(Info personInfo, Stats personStats) : base(personInfo, personStats) { }

        // === СТВОРЕННЯ ОБ'ЄКТА ЧЕРЕЗ СТАТИЧНИЙ МЕТОД ===
        public static Berserker Create(Info personInfo, Stats personStats)
        {
            return new Berserker(personInfo, personStats);
        }

        // === МЕТОД ДЛЯ ОТРИМАННЯ ПОШКОДЖЕННЯ ===
        public override void GetDamage(int damage)
        {
            var stats = PersonStats;
            var totalDamage = damage + BonusDamage;
            var healthIncr = stats.Health - totalDamage;
            stats.Health = healthIncr > 0 ? healthIncr : 0;
            PersonStats = stats;
            RaiseActionEvent($"Berserker {PersonInfo.FirstName} takes {totalDamage} damage (with bonus). Current health: {PersonStats.Health}");
        }

        // === МЕТОД ДЛЯ ОТРИМАННЯ ПОШКОДЖЕННЯ ===
        public override void HealYourself(int hp)
        {
            var stats = PersonStats;
            stats.Health += hp;
            PersonStats = stats;
            RaiseActionEvent($"Berserker {PersonInfo.FirstName} heals for {hp} HP. Current health: {PersonStats.Health}");
        }
    }
}
