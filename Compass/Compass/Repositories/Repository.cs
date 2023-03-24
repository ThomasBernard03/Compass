using System;
using Compass.Repositories.Interfaces;
using SQLite;

namespace Compass.Repositories;

public class Repository<T> : IRepository<T> where T : class, new()
{
    private readonly SQLiteConnection _connection;
    public Repository()
    {
        var filename = "CompassDatabase.db";
        var documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        var path = Path.Combine(documentPath, filename);

        _connection = new SQLiteConnection(path);
        _connection.CreateTable<T>();
    }

    public void Clear()
    {
        Type type = typeof(T);
        string table = type.Name;
        _connection.Execute($"DELETE FROM {table}");
    }

    public void Delete(T value)
    {
        _connection.Delete(value);
    }

    public T GetById(int id)
    {
        return _connection.Find<T>(id);
    }

    public List<T> Get()
    {
        return _connection.Table<T>().ToList();
    }

    public T Insert(T value)
    {
        _connection.Insert(value);
        return value;
    }

    public T Update(T value)
    {
        _connection.Update(value);
        return value;
    }
}

