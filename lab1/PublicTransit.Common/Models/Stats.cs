namespace PublicTransit.Common.Models
{
    public struct Stats(uint age, int health, int damage)
    {
        public uint Age { get; set; } = age;
        public int Health { get; set; } = health;
        public int Damage { get; set; } = damage;
    }
}
