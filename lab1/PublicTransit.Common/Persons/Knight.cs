using PublicTransit.Common.Models;

namespace PublicTransit.Common.Persons
{
    public class Knight : Person
    {
        public int MaxHealth { get; } = 150;

        private Knight(Info personInfo, Stats personStats) : base(personInfo, FixStats(personStats)) { }

        // === ВАЛІДАЦІЯ СТАТІВ ===
        private static Stats FixStats(Stats stats)
        {
            if (stats.Health >= 150)
            {
                stats.Health = 150;
            }
            return stats;
        }

        // === СТВОРЕННЯ ОБ'ЄКТА ЧЕРЕЗ СТАТИЧНИЙ МЕТОД ===
        public static Knight Create(Info personInfo, Stats personStats)
        {
            return new Knight(personInfo, personStats);
        }

        // === МЕТОД ДЛЯ ОТРИМАННЯ ПОШКОДЖЕННЯ ===
        public override void GetDamage(int damage)
        {
            var stats = PersonStats;
            var healthIncr = stats.Health - damage;
            stats.Health = healthIncr > 0 ? healthIncr : 0;
            PersonStats = stats;
            RaiseActionEvent($"Knight {PersonInfo.FirstName} takes {damage} damage. Current health: {PersonStats.Health}");
        }

        public override void HealYourself(int hp)
        {
            var stats = PersonStats;
            if (stats.Health + hp >= MaxHealth)
            {
                stats.Health = MaxHealth;
            }
            else
            {
                stats.Health += hp;
            }
            PersonStats = stats;
            RaiseActionEvent($"Knight {PersonInfo.FirstName} heals for {hp} HP. Current health: {PersonStats.Health}");
        }
    }
}
