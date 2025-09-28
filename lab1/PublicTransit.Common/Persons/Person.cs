using PublicTransit.Common.Events;
using PublicTransit.Common.Models;

namespace PublicTransit.Common.Persons
{
    public abstract class Person
    {
        // === СТАТИЧНІ ПОЛЯ І КОНСТРУКТОР ===
        public static int PersonCount { get; private set; }
        static Person()
        {
            PersonCount = 0;
        }

        // === ПОДІЇ ===
        public event PersonActionEventHandler? OnAction;

        public Info PersonInfo { get; }
        public Stats PersonStats { get; protected set; }

        protected Person(Info info, Stats stats)
        {
            PersonInfo = info;
            PersonStats = stats;
            PersonCount++;
        }

        // === МЕТОД ДЛЯ ВИКЛИКУ ПОДІЇ ===
        protected virtual void RaiseActionEvent(string message)
        {
            OnAction?.Invoke(this, new ActionEventArgs(message));
        }

        // === ПОВЕРНЕННЯ human-friendly СТРІЧКИ З ІНФОРМАЦІЄЮ ПРО ПЕРСОНУ ===
        public override string ToString()
        {
            return
                $"Name: {PersonInfo.FirstName} {PersonInfo.LastName}, \n" +
                $"Salary: ${PersonInfo.Salary} \n" +
                $"Age: {PersonStats.Age}years \n" +
                $"Health: {PersonStats.Health} \n" +
                $"Damage: {PersonStats.Damage} \n";
        }

        // === АБСТРАКТНИЙ МЕТОД ДЛЯ ОТРИМАННЯ ПОШКОДЖЕННЯ ===
        public abstract void GetDamage(int damage);

        // === ВІРТУВАЛЬНИЙ МЕТОД ДЛЯ САМОЗЦІЛЕННЯ ===
        public virtual void HealYourself(int hp) { }
    }
}
