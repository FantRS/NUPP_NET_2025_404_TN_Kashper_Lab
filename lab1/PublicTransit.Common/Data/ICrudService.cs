using System;
using System.Collections.Generic;

namespace PublicTransit.Common.Data
{
    public interface ICrudService<T>
    {
        void Create(T element);
        T Read(Guid id);
        IEnumerable<T> ReadAll();
        void Update(Guid id, T element);
        void Remove(T element);
        ICrudService<T> Load(string path);
        void Save(string path);
    }
}
