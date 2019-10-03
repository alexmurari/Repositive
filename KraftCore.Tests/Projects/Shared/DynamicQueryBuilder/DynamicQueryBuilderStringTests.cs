namespace KraftCore.Tests.Projects.Shared.DynamicQueryBuilder
{
    using System;
    using System.Linq;
    using KraftCore.Shared.DynamicQuery;
    using KraftCore.Shared.Expressions;
    using KraftCore.Tests.Projects.Shared.DynamicQueryBuilder.Base;
    using KraftCore.Tests.Utilities;
    using Xunit;

    // ReSharper disable InconsistentNaming

    /// <summary>
    ///      Tests for the dynamic query builder.
    /// </summary>
    public class DynamicQueryBuilderStringTests : DynamicQueryBuilderTestsBase
    {
        /// <summary>
        /// Asserts that an equality query with single element of string type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Equality_Query_With_Single_Element_Of_String_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.Equal, nameof(Person.FullName), randomPerson.FullName));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FullName == randomPerson.FullName);
            Assert.DoesNotContain(result, t => t.FullName != randomPerson.FullName);
        }

        /// <summary>
        /// Asserts that an non-equality query with single element of string type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Non_Equality_Query_With_Single_Element_Of_String_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.NotEqual, nameof(Person.FullName), randomPerson.FullName));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FullName != randomPerson.FullName);
            Assert.DoesNotContain(result, t => t.FullName == randomPerson.FullName);
        }

        /// <summary>
        /// Asserts that an starts with query with single element of string type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Starts_With_Query_With_Single_Element_Of_String_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.StartsWith, nameof(Person.FullName), randomPerson.FirstName));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FirstName == randomPerson.FirstName);
            Assert.DoesNotContain(result, t => t.FirstName != randomPerson.FirstName);
        }

        /// <summary>
        /// Asserts that an ends with query with single element of string type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Ends_With_Query_With_Single_Element_Of_String_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.EndsWith, nameof(Person.FullName), randomPerson.LastName));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.LastName == randomPerson.LastName);
            Assert.DoesNotContain(result, t => t.LastName != randomPerson.LastName);
        }

        /// <summary>
        /// Asserts that an contains query with single element of string type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Contains_Query_With_Single_Element_Of_String_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.Contains, nameof(Person.FullName), randomPerson.FirstName));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FullName.Contains(randomPerson.FirstName, StringComparison.OrdinalIgnoreCase));
            Assert.DoesNotContain(result, t => t.FullName.Contains(randomPerson.FirstName, StringComparison.OrdinalIgnoreCase) == false);
        }

        /// <summary>
        /// Asserts that an contains on value query with single element of string type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Contains_On_Value_Query_With_Single_Element_Of_String_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomNames = Utilities.GetRandomItems(Persons.Select(t => t.FullName));
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.ContainsOnValue, nameof(Person.FullName), string.Join(',', randomNames.Select(t => $"'{t}'"))));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => randomNames.Contains(t.FullName));
            Assert.DoesNotContain(result, t => randomNames.Contains(t.FullName) == false);
        }
    }
}