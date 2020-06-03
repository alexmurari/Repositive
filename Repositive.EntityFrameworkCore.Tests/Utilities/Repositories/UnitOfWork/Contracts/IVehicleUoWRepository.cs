﻿namespace Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Contracts
{
    using Repositive.Abstractions;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Entities;

    /// <summary>
    ///     Provides repository methods for instances of <see cref="Vehicle"/>.
    /// </summary>
    public interface IVehicleUoWRepository : IRepository<Vehicle>
    {
    }
}