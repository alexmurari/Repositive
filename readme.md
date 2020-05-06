<div align="center">
    <a href="https://github.com/alexmurari/Repositive/">
    <img alt="Exprelsior" width="400" src="https://user-images.githubusercontent.com/11204378/81116400-aab2c700-8efb-11ea-8f8f-2fc3908ea7d7.png">
  </a>
  <p>
    <strong>A .NET Standard advanced repository pattern.</strong>
  </p>
</div>

---

## What is Repositive?

Repositive is a .NET Standard library that provides interfaces and implementations for setting up data access repositories following the repository pattern.

It provides many advanced methods for creating, reading, updating, deleting entities with great flexibility.

It also provides methods for controlling query pagination, change tracking, including related entities in queries, explicitly loading related entities and advanced querying.

All methods have it's asynchronous counterparts.

---

1. [Overview](#1-overview)

## 1. Overview

The objective os this library is to provide plug-and-play repository interfaces and implementations:

**Example:**

The contract:
```csharp
public class ICarRepository : IRepository<Car>
{
}
```

The implementation:
```csharp
public class CarRepository : Repository<Car, MyDbContext>, ICarRepository
{
    public CarRepository(MyDbContext context) : base(context)
    {
    }
}
```

The binding (using your favorite IoC container):
```csharp
services.AddScoped<ICarRepository, CarRepository>();
```

The usage:
```csharp
public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;

    public CarRepository(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public IEnumerable<Car> GetCarsInRepair()
    {
        return _carRepository.Get(t => t.Status == CarStatus.InRepair, QueryTracking.NoTracking);
    }
}
```