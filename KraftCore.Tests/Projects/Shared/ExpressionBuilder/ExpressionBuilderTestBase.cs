namespace KraftCore.Tests.Projects.Shared.ExpressionBuilder
{
    using System.Collections.Generic;
    using KraftCore.Tests.Utilities;

    /// <summary>
    ///     Base class for expression builder unit tests.
    /// </summary>
    public abstract class ExpressionBuilderTestBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionBuilderTestBase"/> class.
        /// </summary>
        protected ExpressionBuilderTestBase()
        {
            HydraArmy = Utilities.GetFakeHydraCollection();
        }

        /// <summary>
        ///     Gets the hydra army.
        /// </summary>
        protected List<Hydra> HydraArmy { get; }
    }
}
