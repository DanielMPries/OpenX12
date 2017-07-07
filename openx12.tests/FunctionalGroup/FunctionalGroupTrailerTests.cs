using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using openx12.Models.FunctionalGroup;

namespace openx12.tests.FunctionalGroup
{
    [TestClass]
    public class FunctionalGroupTrailerTests
    {
        private const string _FunctionalGroupString = "GE*1*1";
        private FunctionalGroupTrailer _Trailer;

        public static string GetTestString() {
            return _FunctionalGroupString;
        }

        [TestInitialize]
        public void Setup()
        {
            _Trailer = new FunctionalGroupTrailer(_FunctionalGroupString);
        }

        [TestMethod, TestCategory("Unit")]
        public void should_parse_functional_group_trailer()
        {
            _Trailer.Name.Should().Be("GE");
            _Trailer.Elements.Should().HaveCount(2);
            _Trailer.Value.Should().Be(_FunctionalGroupString);
        }

        [TestMethod, TestCategory("Unit")]
        public void should_parse_provide_correct_derived_properties() {
            _Trailer.IncludedTransactionSets.Should().Be("1");
            _Trailer.GroupControlNumber.Should().Be("1");
        }

        [TestMethod, TestCategory("Unit")]
        public void should_instantiate_funcitonal_group_trailer_with_properties() {
            var trailer = new FunctionalGroupTrailer
            {
                IncludedTransactionSets = "10",
                GroupControlNumber = "1"
            };

            trailer.Value.Should().Be("GE*10*1");
        }
    }
}
