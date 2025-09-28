using PublicTransit.Common.Persons;

namespace PublicTransit.Common.Extensions
{
    public static class PersonExtensions
    {
        // === МЕТОД ДЛЯ ПЕРЕВІРКИ ЖИТТІВ ===
        public static bool IsAlive(this Person person)
        {
            return person.PersonStats.Health > 0;
        }
    }
}
