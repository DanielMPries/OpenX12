using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using openx12.Models;
using openx12.tests.Transaction;

namespace openx12.tests.FunctionalGroup
{
    [TestClass]
    public class FunctionalGroupTests {
        private Models.FunctionalGroup.FunctionalGroup _FunctionalGroup;
        private string _X12String;

        [TestInitialize]
        public void Setup() {
            _X12String = GetTestString();
            _FunctionalGroup = new Models.FunctionalGroup.FunctionalGroup(_X12String);
        }

        [TestMethod, TestCategory("Unit")]
        public void should_parse_functional_group() {
            _FunctionalGroup.TransactionSets.Should().HaveCount(1);
            _FunctionalGroup.Value.Should().Be(_X12String);
        }

        
        public static string GetTestString() {
            return FunctionalGroupHeaderTests.GetTestString() + FormattingOptions.DefaultOptions.SegmentTerminator +
                   TransactionTests.GetTestString() +
                   FunctionalGroupTrailerTests.GetTestString();
        }
    }
}
