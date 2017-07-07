using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using openx12.Models.Transaction;

namespace openx12.tests.Transaction
{
    [TestClass]
    public class TransactionSetHeaderTests {
        private const string _SegmentString = "ST*834*0001*005010X220";
        private TransactionSetHeader _Header;

        [TestInitialize]
        public void Setup() {
            _Header = new TransactionSetHeader(_SegmentString);
        }

        [TestMethod, TestCategory("Unit")]
        public void should_parse_transaction_set_header() {
            _Header.Name.Should().Be("ST");
            _Header.Elements.Should().HaveCount(3);
            _Header.Value.Should().Be(_SegmentString);
        }

        [TestMethod, TestCategory("Unit")]
        public void should_parse_provide_correct_derived_properties()
        {
            _Header.TransactionSetIdentifier.Should().Be("834");
            _Header.TransactionSetControlNumber.Should().Be("0001");
            _Header.ImplementationConventionReference.Should().Be("005010X220");

        }
    }
}
