using System;
namespace Compass.Repositories.Interfaces;

public interface IRepository<T>
{
    void Clear();
    void Delete(T value);
    T Insert(T value);
    T Update(T value);
    T GetById(int id);
    List<T> Get();
}

