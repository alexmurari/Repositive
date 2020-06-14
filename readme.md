<div align="center">
   <a href="https://github.com/alexmurari/Repositive/">
   <img alt="Repositive" width="400" src="https://user-images.githubusercontent.com/11204378/81116400-aab2c700-8efb-11ea-8f8f-2fc3908ea7d7.png">
   </a>
   <p>
      <strong>Advanced repository pattern interfaces and implementations with unit of work support.</strong>
   </p>
   <p>
   <hr/>
   <h4>Stable Channel</h4>
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
      <a href="https://app.codacy.com/manual/alexmurari/Repositive/dashboard?bid=18477476">
      <img alt="Codacy branch grade" src="https://img.shields.io/codacy/grade/ea3688d082f34e5ba78ec9a9fe0714a9/master?style=flat-square">
      </a>
      <a href="https://github.com/alexmurari/Repositive/blob/master/LICENSE">
      <img src="https://img.shields.io/github/license/alexmurari/repositive?style=flat-square">
      </a>
   </p>
   <h4>Development Channel</h4>
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
</div>

---

## What is Repositive?

Repositive is a .NET Standard library that provides interfaces and implementations for setting up data access repositories following the repository pattern.

It provides many advanced methods for creating, reading, updating and deleting entities with great flexibility.

It also provides methods for controlling query pagination, change tracking, including related entities in queries, explicitly loading related entities and advanced querying.

Repositive also supports the unit of work pattern, so you can synchronize the commit operation between multiple repositories in a single operation.

The [contracts](#3-contracts) are splitted in module-like interfaces, allowing to build concise repository classes that expose only the necessary methods.

All methods have it's asynchronous counterparts.

---

1. [Overview](#1-overview)
2. [Supported ORMs](#2-supported-orms)
3. [Contracts](#3-contracts)
4. [Implementations](#4-implementations)
5. [Getting Started](#5-getting-started)
   1. [Basic Setup](#basic-setup)
   2. [Unit Of Work Setup](#unit-of-work-setup)
6. [License](#6-license)

---

## 1. Overview

The objective of this library is to provide ready-to-use repository-pattern interfaces and implementations:

**Example:**

Define an interface representing your repository and let Repositive's interfaces provide all the methods:

```csharp
using Repositive.Abstractions;
// ...

public interface ICarRepository : IRepository<Car>, IRelatedLoadableRepository<Car>, ISaveableRepository
{
    // IRepository<T> provides basic CRUD methods.
    // IRelatedLoadableRepository<T> provides methods for explicitly loading related entities.
    // ISaveableRepository provides methods for saving changes directly from the repository (commit).
}
```

Then define an implementation for that interface and let Repositive do the heavy lifiting:

```csharp
using Repositive.EntityFrameworkCore;
// ...

public class CarRepository : Repository<Car, MyDbContext>, ICarRepository
{
    // Repository<TEntity, TContext> implements all repository interfaces.
    public CarRepository(MyDbContext context) : base(context)
    {
    }
}
```

Now it's ready to use:

```csharp
public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;

    public CarService(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public void AddCar(Car car)
    {
        _carRepository.Add(car);
        _carRepository.Commit(); // Commit also returns the number of affected entries
    }

    public (IEnumerable<Car> Cars, int TotalCount) GetCars(int page, int pageSize)
    {
        // Returns a tuple with the paginated collection of cars and the total count of entities in the database.
        // Includes the Manufacturer navigation property in the query.
        var entitiesToSkip = (page - 1) * pageSize;
        return _carRepository.Get(entitiesToSkip, pageSize, QueryTracking.NoTracking, t => t.Manufacturer);
    }

    public void ProcessServiceOrder(Car car)
    {
        // Explicitly loads 'car.Owner', 'car.Owner.Address' and 
        // 'car.Owner.PaymentInfo.BankCards' navigation properties.
        _carRepository.LoadRelated(car, t => t.Owner, t => t.Address, t => t.PaymentInfo.BankCards);
        
        // Explicitly loads all closed service orders into 'car.ServiceOrders' navigation property.
        _carRepository.LoadRelatedCollection(car, t => t.ServiceOrders, t => t.Status == ServiceOrderStatus.Closed);

        // ...
    }
}
```

---

## 2. Supported ORMs

- List of ORMs supported by Repositive.

| ORM | Version | Namespace/Package Name |
------------------------|-------|--------------------------------|
| Entity Framework Core | 3.1.x | Repositive.EntityFrameworkCore |

---

## 3. Contracts

- All interfaces are in the ```Repositive.Abstractions``` namespace.

- All methods' asynchronous counterparts names have the ```Async``` suffix. Ex.: ```GetSingleAsync```.

- The ```Repositive.Abstractions``` namespace/package only contains interfaces and abstractions, with no implementation or reference to any ORM or data provider whatsoever.

- The interfaces' modular design allows exposing concise repositories with only needed functionality.

#### ```IRepository<T>```

- Defines a repository contract for creating, reading, updating and deleting instances of entities of type ```T```.
- It's a combination of the ```ICreatableRepository<T>```, ```IReadableRepository<T>```, ```IUpdateableRepository<T>``` and ```IDeletableRepository<T>``` interfaces.
- It does not provide methods for committing changes to the database. For that use the ```ISaveableRepository``` or ```IUnitOfWork``` interfaces.

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

#### ```ISaveableRepository```
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

## 4. Implementations

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

---

## 5. Getting Started

### Basic Setup

#### 1. Define your repository contracts, inheriting the desired repository interfaces from the ```Repositive.Abstractions``` package.

```csharp
using Repositive.Abstractions;
// ...

public interface IFooRepository : IRepository<Foo>, ISaveableRepository
{
    // Optional: any specific user defined method can go in here. Ex.: "GetFoosByDate(DateTime date)"
}

public interface IBarRepository : IReadableRepository<Bar>, IRelatedLoadableRepository<Bar>
{
    // Optional: any specific user defined method can go in here. Ex.: "GetBarsByDate(DateTime date)"
}
```

#### 2. Define the contracts' implementations, inheriting the ```Repository<T, C>``` class from the implementation package.

```csharp
using Repositive.EntityFrameworkCore;
// ...

public class FooRepository : Repository<Foo, MyDbContext>, IFooRepository
{
    public FooRepository(MyDbContext context) : base(context)
    {
    }

    // Implement here any user-defined methods in IFooRepository.
}

public class BarRepository : Repository<Bar, MyDbContext>, IBarRepository
{
    public BarRepository(MyDbContext context) : base(context)
    {
    }

    // Implement here any user-defined methods in IBarRepository.
}
```

#### 3. Optional: Bind the interfaces and implementations together using an IoC container.

```csharp
services.AddScoped<IFooRepository, FooRepository>();
services.AddScoped<IBarRepository, BarRepository>();
```

#### 4. Ready to use!

```csharp
public class BazService : IBazService
{
    private readonly IFooRepository _fooRepository;
    private readonly IBarRepository _barRepository;

    public BazService(IFooRepository fooRepository, IBarRepository barRepository)
    {
        _fooRepository = fooRepository;
        _barRepository = barRepository
    }

    public void Baz()
    {
        _fooRepository.Add(foo);
        _fooRepository.Commit();

        var bar = _barRepository.GetSingle(t => t.Name == "John Doe", QueryTracking.TrackAll, includes: t => t.Foo);

        //...

        _barRepository.LoadRelated(bar, t => t.Qur);

        // ...
    }
}
```

### Unit Of Work Setup

- Repositive provides unit of work support for coordinating commit operations between multiple repositories in a single operation.

- The advantage of this approach is data integrity: by using unit of work pattern, changes made to multiple repositories are committed
  in a single transaction, meaning that if something goes wrong in any repository during the operation, the whole transaction is aborted, ensuring data integrity.

#### 1. Define your repository contracts following the [basic setup](#basic-setup).
###### IMPORTANT: Do not inherit the ```ISaveableRepository``` interface when using unit of work, repositories using UoW should not expose commit methods.

#### 2. In the repositories implementations, pass a ```IUnitOfWork``` instance to the class constructor and base constructor.

```csharp
using Repositive.Abstractions;
using Repositive.EntityFrameworkCore;
// ...

public class FooRepository : Repository<Foo, MyDbContext>, IFooRepository
{
    public FooRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}

public class BarRepository : Repository<Bar, MyDbContext>, IBarRepository
{
    public BarRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
```

#### 3. Optional: Bind the interfaces and implementations together using an IoC container.

```csharp
using Repositive.Abstractions;
using Repositive.EntityFrameworkCore;
// ...

services.AddDbContext<MyDbContext>();
services.AddScoped<IUnitOfWork, UnitOfWork<MyDbContext>>();
services.AddScoped<IFooRepository, FooRepository>();
services.AddScoped<IBarRepository, BarRepository>();
```

#### 4. Ready to use! 
###### Inject the ```IUnitOfWork``` instance along with the repositories and use it to commit changes.

```csharp
using Repositive.Abstractions;
// ...

public class BazService : IBazService
{
    private readonly IFooRepository _fooRepository;
    private readonly IBarRepository _barRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BazService(IFooRepository fooRepository, IBarRepository barRepository, IUnitOfWork unitOfWork)
    {
        _fooRepository = fooRepository;
        _barRepository = barRepository
        _unitOfWork = unitOfWork;
    }

    public void Process()
    {
        _fooRepository.Add(foo);
        // ...

        _barRepository.DeleteRange(bars);
        // ...

        // Changes made to foo and bar repositories are committed in a single operation (transaction).
        // If anything goes wrong, no changes are committed at all.
        _unitOfWork.Commit();
    }
}
```
---

## 6. License

[MIT License (MIT)](./LICENSE)