using System;
using SQLite;

namespace Compass.Models.Entities;

public abstract class BaseEntity
{
    public BaseEntity()
    {
        CreationDate = DateTime.Now.Ticks;
    }

    [PrimaryKey, AutoIncrement]
    public long Id { get; set; }
    public long CreationDate { get; set; }
}

