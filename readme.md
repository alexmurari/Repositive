<div align="center">
    <a href="https://github.com/alexmurari/Repositive/">
    <img alt="Repositive" width="400" src="https://user-images.githubusercontent.com/11204378/81116400-aab2c700-8efb-11ea-8f8f-2fc3908ea7d7.png">
  </a>
  <p>
    <strong>A .NET Standard advanced repository pattern with unit of work support.</strong>
  </p>
<p>

<fieldset style="margin-bottom: 15px">
   <legend><b>Stable Channel</b></legend>
   <p style="margin: 10px;">
      <a href="https://www.nuget.org/packages/Repositive.Abstractions">
      <img alt="Nuget" src="https://img.shields.io/nuget/v/Repositive.Abstractions?label=Repositive.Abstractions&style=flat-square">
      </a>
      <a href="https://www.nuget.org/packages/Repositive.EntityFrameworkCore">
      <img alt="Nuget" src="https://img.shields.io/nuget/v/Repositive.EntityFrameworkCore?label=Repositive.EntityFrameworkCore&style=flat-square">
      </a>
   </p>
   <p style="margin: 10px;">
      <a href="https://ci.appveyor.com/project/alexmurari/repositive/branch/master">
      <img src="https://img.shields.io/appveyor/ci/alexmurari/repositive/master?style=flat-square">
      </a>
      <a href="https://ci.appveyor.com/project/alexmurari/repositive/branch/master/tests">
      <img src="https://img.shields.io/appveyor/tests/alexmurari/repositive/master?compact_message&style=flat-square">
      </a>
      <a href="https://app.codacy.com/manual/alexmurari/Repositive/dashboard?bid=18477475">
      <img alt="Codacy branch grade" src="https://img.shields.io/codacy/grade/ea3688d082f34e5ba78ec9a9fe0714a9/master?style=flat-square">
      </a>
      <a href="https://github.com/alexmurari/Repositive/blob/master/LICENSE">
      <img src="https://img.shields.io/github/license/alexmurari/repositive?style=flat-square">
      </a>
   </p>
</fieldset>
<fieldset style="margin-bottom: 25px">
   <legend><b>Development Channel</b></legend>
   <p style="margin: 10px;">
      <a href="https://www.nuget.org/packages/Repositive.Abstractions">
      <img alt="Nuget" src="https://img.shields.io/nuget/vpre/Repositive.Abstractions?label=Repositive.Abstractions&style=flat-square">
      </a>
      <a href="https://www.nuget.org/packages/Repositive.EntityFrameworkCore">
      <img alt="Nuget" src="https://img.shields.io/nuget/vpre/Repositive.EntityFrameworkCore?label=Repositive.EntityFrameworkCore&style=flat-square">
      </a>
   </p>
   <p style="margin: 10px;">
      <a href="https://ci.appveyor.com/project/alexmurari/repositive/branch/dev">
      <img src="https://img.shields.io/appveyor/ci/alexmurari/repositive/dev?style=flat-square">
      </a>
      <a href="https://ci.appveyor.com/project/alexmurari/repositive/branch/dev/tests">
      <img src="https://img.shields.io/appveyor/tests/alexmurari/repositive/dev?compact_message&style=flat-square">
      </a>
      <a href="https://app.codacy.com/manual/alexmurari/Repositive/dashboard?bid=18477475">
      <img alt="Codacy branch grade" src="https://img.shields.io/codacy/grade/ea3688d082f34e5ba78ec9a9fe0714a9/dev?style=flat-square">
      </a>
      <a href="https://github.com/alexmurari/Repositive/blob/dev/LICENSE">
      <img src="https://img.shields.io/github/license/alexmurari/repositive?style=flat-square">
      </a>
   </p>
</fieldset>
</div>

---

## What is Repositive?

Repositive is a .NET Standard library that provides contracts and implementations for setting up data access repositories following the repository pattern.

It provides many advanced methods for creating, reading, updating and deleting entities with great flexibility.

It also provides methods for controlling query pagination, change tracking, including related entities in queries, explicitly loading related entities and advanced querying.

Repositive also supports the unit of work pattern, so you can synchronize the commit operation between multiple repositories in a single operation.

All methods have it's asynchronous counterparts.

---

1. [Overview](#1-overview)
2. [Contracts](#2-contracts)
3. [Implementations](#3-implementations)

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

- All interfaces are in the ```Repositive.Abstractions``` namespace.

- All methods' asynchronous counterparts names have the ```Async``` suffix. Ex.: ```GetSingleAsync```.

#### ```IRepository<T>```

- Defines a repository contract for creating, reading, updating and deleting instances of entities of type ```T```.
- It's a combination of the ```ICreatableRepository<T>```, ```IReadableRepository<T>```, ```IUpdateableRepository<T>``` and ```IDeletableRepository<T>``` interfaces.
- It does not provide methods for committing changes to the database. For that use the ```ISaveable``` or ```IUnitOfWork``` interfaces.

#### ```ICreatableRepository<T>```
- Defines a repository contract for creating entities of type ```T```.
- Methods: ```Add```, ```AddRange```.

#### ```IReadableRepository<T>```
- Defines a repository contract for reading entities of type ```T```.
- Methods: ```Any```, ```Count```, ```Find```, ```Get```, ```GetSingle```.

#### ```IUpdateableRepository<T>```
- Defines a repository contract for updating entities of type ```T```.
- Methods: ```Update```, ```UpdateRange```.

#### ```IDeletableRepository<T>```
- Defines a repository contract for deleting entities of type ```T```.
- Methods: ```Delete```, ```DeleteRange```.

#### ```ISaveable```
- Defines a repository contract for saving changes made to a repository.
- Methods: ```Commit```.
- Remarks:
  - Do not expose this interface in repositories that operate with unit of work, as the commit responsibility belongs to the ```IUnitOfWork``` interface.
  - Caution is advised when using this interface, as the database context may be shared between different repositories, changes from
    other repositories may be committed when saving changes from a specific repository, leading to unexpected and/or unintended behavior.

#### ```IQueryableRepository<T>```
- Defines a repository contract for querying instances of type ```T``` 
using the ```IQueryable<T>``` interface and projecting the results to ```TResult``` type.
- The query results are projected to the type defined by ```TResult```.
- Methods: ```Query<TResult>```, ```QuerySingle<TResult>```

#### ```IRelatedLoadableRepository<T>```
- Defines a repository contract for explicitly loading related entities referenced by navigation properties in instances of ```T```.
- The navigation property type is defined by ```TProperty```.
- Methods: ```LoadRelated<TProperty>```, ```LoadRelatedCollection<TProperty>```.

#### ```IUnitOfWork```
- Defines a contract for coordinately committing changes between repositories (unit of work pattern).
- Methods: ```Commit```.
- Events: ```Committing```, ```Committed```.
- Remarks:
  - As the objective of this contract is to coordinate commit operations between different
    repositories, no commit operation should be made directly from a repository that uses the
    unit of work pattern, but rather use the methods provided by this interface for committing.
  - Avoid making the ```ISaveableRepository``` contract available in repositories
    that use unit of work, as all commit operations should be made through this interface.

---

## 3. Implementations

#### ```Repositive.EntityFrameworkCore.Repository<TEntity, TContext>```
- Provides a repository pattern implementation for querying and saving instances of ```TEntity``` with ```Microsoft.EntityFrameworkCore``` as the ORM.
- The database context type is defined by ```TContext```. It must derive from or be of ```Microsoft.EntityFramework.DbContext``` type.
- Implements ```IRepository<TTEntity>```, ```IQueryableRepository<TTEntity>```, ```IRelatedLoadableRepository<TTEntity>```, ```ISaveableRepository``` interfaces.
- Constructor: ```(TContext)``` or ```(IUnitOfWork)```. 

#### ```Repositive.EntityFrameworkCore.UnitOfWork<TContext>```
- Implements the unit of work pattern and provides commit coordination between repositories.
- The database context type is defined by ```TContext```. It must derive from or be of ```Microsoft.EntityFramework.DbContext``` type.
- Implements the ```IUnitOfWork``` interface.
- Centralizes the ```Micosoft.EntityFrameworkCore.DbContext``` instance and shares it between repositories, so changes from multiple repositories
  are contained in a single database context instance.
- Constructor: ```(TContext)```. 