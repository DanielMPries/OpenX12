using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using openx12.Models.Transaction;
using XTransaction = openx12.Models.Transaction.Transaction;

namespace openx12.tests.Transaction
{
    [TestClass]
    public class TransactionTests {
        private const string _TransactionSetString = "ST*834*0001*005010X220~DTP*007*D8*19961001~SE*3*0001";
        private XTransaction _Transaction;

        [TestInitialize]
        public void Setup() {
            _Transaction = new XTransaction(_TransactionSetString);
        }

        
        [TestMethod, TestCategory("Unit")]
        public void should_parse_transaction() {
            _Transaction.Segments.Should().HaveCount(1);
            _Transaction.Value.Should().Be(_TransactionSetString);
        }

        [TestMethod, TestCategory("Unit")]
        public void should_parse_provide_correct_derived_properties() {
            _Transaction.Trailer.NumberOfIncludedSegments.Should().Be("3");

        }

        public static string GetTestString() {
            return _TransactionSetString + "~";
        }
    }
}
