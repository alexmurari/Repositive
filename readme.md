<div align="center">
    <a href="https://github.com/alexmurari/Repositive/">
    <img alt="Exprelsior" width="400" src="https://user-images.githubusercontent.com/11204378/81116400-aab2c700-8efb-11ea-8f8f-2fc3908ea7d7.png">
  </a>
  <p>
    <strong>A .NET Standard advanced repository pattern with unit of work support.</strong>
  </p>
</div>

---

## What is Repositive?

Repositive is a .NET Standard library that provides contracts and implementations for setting up data access repositories following the repository pattern.

It provides many advanced methods for creating, reading, updating, deleting entities with great flexibility.

It also provides methods for controlling query pagination, change tracking, including related entities in queries, explicitly loading related entities and advanced querying.

Repositive also supports the unit of work pattern, so you can synchronize the commit operation between multiple repositories in a single operation.

All methods have it's asynchronous counterparts.

---

1. [Overview](#1-overview)
2. [Contracts](#2-contracts)

---

## 1. Overview

The objective of this library is to provide plug-and-play repository interfaces and implementations:

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

---

## 2. Contracts

#### ```IRepository<T>```

- Defines a repository contract for creating, reading, updating and deleting instances of entities of type ```T```.
- It's a combination of the ```ICreatableRepository<T>```, ```IReadableRepository<T>```, ```IUpdateableRepository<T>``` and ```IDeletableRepository<T>``` interfaces.
- It does not provide methods for committing changes to the database. For that use the ```ISaveable``` or ```IUnitOfWork``` interface.

#### ```ICreatableRepository<T>```
- Defines a repository contract for creating entities of type ```T```.
- Methods: ```Add<T>```, ```AddRange<T>```.

#### ```IReadableRepository<T>```
- Defines a repository contract for reading entities of type ```T```.
- Methods: ```Any<T>```, ```Count<T>```, ```Find<T>```, ```Get<T>```, ```GetSingle<T>```.

#### ```IUpdateableRepository<T>```
- Defines a repository contract for updating entities of type ```T```.
- Methods: ```Update<T>```, ```UpdateRange<T>```.

#### ```IDeletableRepository<T>```
- Defines a repository contract for deleting entities of type ```T```.
- Methods: ```Delete<T>```, ```DeleteRange<T>```.

#### ```ISaveable```
- Defines a repository contract for saving changes made to a repository.
- Methods: ```Commit```.
- ###### Remarks:
  - Do not expose this interface in repositories that operate with unit of work, as the commit responsibility belongs to the ```IUnitOfWork<T>``` interface.
  - Caution is advised when using this interface, as the database context may be shared between different repositories, changes from
    other repositories may be committed when saving changes from a specific repository, leading to unexpected and/or unintended behavior.

#### ```IQueryableRepository<T>```
- Defines a repository contract for querying instances of type ```T``` 
using the ```IQueryable<T>``` interface and projecting the results to ```TResult``` type.
- The query results are projected to the type defined by ```TResult```.
- Methods: ```Query<TResult>```, ```QuerySingle<TResult>```

#### ```IRelatedLoadableRepository<TEntity>```
- Defines a repository contract for explicitly loading related entities referenced by navigation properties in instances of ```T```.
- The navigation property type is defined by ```TProperty```.
- Methods: ```LoadRelated<TProperty>```, ```LoadRelatedCollection<TProperty>```.

#### ```IUnitOfWork```
- Defines a contract for coordinately committing changes between repositories (unit of work pattern).
- Methods: ```Commit```.
- Events: ```Committing```, ```Committed```.
- ###### Remarks:
  - As the objective of this contract is to coordinate commit operations between different
    repositories, no commit operation should be made directly from a repository that uses the
    unit of work pattern, but rather use the methods provided by this interface for committing.
  - Avoid making the ```ISaveableRepository``` contract available in repositories
    that use unit of work, as all commit operations should be made through this interface.