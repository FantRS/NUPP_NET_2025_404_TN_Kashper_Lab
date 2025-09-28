using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace PublicTransit.Common.Data
{
    public class CrudList<T> : ICrudService<T>
    {
        public readonly Dictionary<Guid, T> Map;

        // === КОНСТРУКТОР ===
        public CrudList()
        {
            this.Map = new Dictionary<Guid, T>();
        }

        // === СТВОРЕННЯ ===
        public void Create(T element)
        {
            this.Map.Add(Guid.NewGuid(), element);
        }

        // === ПОВЕРНЕННЯ ЕЛЕМЕНТУ ===
        public T Read(Guid id)
        {
            return this.Map[id];
        }

        // === ПОВЕРНЕННЯ ЕЛЕМЕНТІВ ===
        public IEnumerable<T> ReadAll()
        {
            return this.Map.Values.ToList();
        }

        // === ВИДАЛЕННЯ ЗА ЕЛЕМЕНТОМ ===
        public void Remove(T element)
        {
            var keyToRemove = Map.FirstOrDefault(kvp => EqualityComparer<T>.Default.Equals(kvp.Value, element)).Key;
            if (keyToRemove != Guid.Empty)
            {
                Map.Remove(keyToRemove);
            }
        }

        // === ОНОВЛЕННЯ ЗА КЛЮЧЕМ ===
        public void Update(Guid id, T element)
        {
            if (Map.ContainsKey(id))
            {
                Map[id] = element;
            }
            else
            {
                throw new KeyNotFoundException($"Елемент з ID {id} не знайдено");
            }
        }

        // === ЗБЕРЕЖЕННЯ ЗА ШЛЯХОМ ===
        public void Save(string path)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true // для читабельності
            };

            string json = JsonSerializer.Serialize(this.Map, options);
            File.WriteAllText(path, json);
        }

        // === ЗАВАНТАЖЕННЯ ЗА ШЛЯХОМ ===
        public ICrudService<T> Load(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"Файл {path} не знайдено");

            string json = File.ReadAllText(path);
            var data = JsonSerializer.Deserialize<Dictionary<Guid, T>>(json);

            var newCrud = new CrudList<T>();
            if (data != null)
            {
                foreach (var kvp in data)
                {
                    newCrud.Map[kvp.Key] = kvp.Value;
                }
            }

            return newCrud;
        }
    }
}
