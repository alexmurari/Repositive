namespace KraftCore.Tests.Projects.Shared.DynamicQueryBuilder
{
    using System.Linq;
    using KraftCore.Shared.DynamicQuery;
    using KraftCore.Shared.Expressions;
    using KraftCore.Tests.Projects.Shared.DynamicQueryBuilder.Base;
    using KraftCore.Tests.Utilities;
    using Xunit;

    // ReSharper disable InconsistentNaming

    /// <summary>
    ///     Tests for the dynamic query builder.
    /// </summary>
    public class DynamicQueryBuilderBooleanTests : DynamicQueryBuilderTestsBase
    {
        /// <summary>
        ///     Asserts that an equality query with single element of boolean type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Equality_Query_With_Single_Element_Of_Boolean_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Hydra>(BuildQueryText(ExpressionOperator.Equal, nameof(Hydra.HasPet), randomPerson.HasPet));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.HasPet == randomPerson.HasPet);
            Assert.DoesNotContain(result, t => t.HasPet != randomPerson.HasPet);
        }

        /// <summary>
        ///     Asserts that an non-equality query with single element of boolean type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Non_Equality_Query_With_Single_Element_Of_Boolean_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Hydra>(BuildQueryText(ExpressionOperator.NotEqual, nameof(Hydra.HasPet), randomPerson.HasPet));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.HasPet != randomPerson.HasPet);
            Assert.DoesNotContain(result, t => t.HasPet == randomPerson.HasPet);
        }

        /// <summary>
        ///     Asserts that an contains on value query with single element of boolean type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Contains_On_Value_Query_With_Single_Element_Of_Boolean_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomHasPet = Utilities.GetRandomItems(Persons.Select(t => t.HasPet));
            var query = DynamicQueryBuilder.Build<Hydra>(BuildQueryText(ExpressionOperator.ContainsOnValue, nameof(Hydra.HasPet), string.Join(',', randomHasPet.Select(x => $"'{x}'"))));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => randomHasPet.Contains(t.HasPet));
            Assert.DoesNotContain(result, t => randomHasPet.Contains(t.HasPet) == false);
        }
    }
}