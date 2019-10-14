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
    public class DynamicQueryBuilderGuidTests : DynamicQueryBuilderTestsBase
    {
        /// <summary>
        ///     Asserts that an equality query with single element of globally unique identifier type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Equality_Query_With_Single_Element_Of_Globally_Unique_Identifier_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.Equal, nameof(Person.PersonGuid), randomPerson.PersonGuid.ToString()));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.PersonGuid == randomPerson.PersonGuid);
            Assert.DoesNotContain(result, t => t.PersonGuid != randomPerson.PersonGuid);
        }

        /// <summary>
        ///     Asserts that an non-equality query with single element of globally unique identifier type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Non_Equality_Query_With_Single_Element_Of_Globally_Unique_Identifier_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.NotEqual, nameof(Person.PersonGuid), randomPerson.PersonGuid.ToString()));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.PersonGuid != randomPerson.PersonGuid);
            Assert.DoesNotContain(result, t => t.PersonGuid == randomPerson.PersonGuid);
        }

        /// <summary>
        ///     Asserts that an contains on value query with single element of string type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Contains_On_Value_Query_With_Single_Element_Of_Globally_Unique_Identifier_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomIds = Utilities.GetRandomItems(Persons.Select(t => t.PersonGuid));
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.ContainsOnValue, nameof(Person.PersonGuid), string.Join(',', randomIds.Select(x => $"'{x}'"))));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => randomIds.Contains(t.PersonGuid));
            Assert.DoesNotContain(result, t => randomIds.Contains(t.PersonGuid) == false);
        }

        /// <summary>
        ///     Asserts that an equality query with single element of globally unique identifier type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Equality_Query_With_Single_Element_Of_Nullable_Globally_Unique_Identifier_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.Equal,
                                                                         nameof(Person.OptionalPersonGuid),
                                                                         randomPerson.OptionalPersonGuid.GetValueOrDefault().ToString()));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.OptionalPersonGuid == randomPerson.OptionalPersonGuid);
            Assert.DoesNotContain(result, t => t.OptionalPersonGuid != randomPerson.OptionalPersonGuid);
        }

        /// <summary>
        ///     Asserts that an non-equality query with single element of globally unique identifier type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Non_Equality_Query_With_Single_Element_Of_Nullable_Globally_Unique_Identifier_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.NotEqual,
                                                                         nameof(Person.OptionalPersonGuid),
                                                                         randomPerson.OptionalPersonGuid.GetValueOrDefault().ToString()));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.OptionalPersonGuid != randomPerson.OptionalPersonGuid);
            Assert.DoesNotContain(result, t => t.OptionalPersonGuid == randomPerson.OptionalPersonGuid);
        }

        /// <summary>
        ///     Asserts that an contains on value query with single element of string type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Contains_On_Value_Query_With_Single_Element_Of_Nullable_Globally_Unique_Identifier_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomIds = Utilities.GetRandomItems(Persons.Select(t => t.OptionalPersonGuid));
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.ContainsOnValue,
                                                                         nameof(Person.OptionalPersonGuid),
                                                                         string.Join(',', randomIds.Select(x => $"'{x}'"))));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => randomIds.Contains(t.OptionalPersonGuid));
            Assert.DoesNotContain(result, t => randomIds.Contains(t.OptionalPersonGuid) == false);
        }
    }
}