using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using openx12.Models.Transaction;

namespace openx12.tests.Transaction
{
    [TestClass]
    public class TransactionSetTrailerTests
    {
        private const string _SegmentString = "SE*39*0001";
        private TransactionSetTrailer _Trailer;

        [TestInitialize]
        public void Setup()
        {
            _Trailer = new TransactionSetTrailer(_SegmentString);
        }

        [TestMethod, TestCategory("Unit")]
        public void should_parse_transaction_set_trailer()
        {
            _Trailer.Name.Should().Be("SE");
            _Trailer.Elements.Should().HaveCount(2);
            _Trailer.Value.Should().Be(_SegmentString);
        }

        [TestMethod, TestCategory("Unit")]
        public void should_parse_provide_correct_derived_properties()
        {
            _Trailer.NumberOfIncludedSegments.Should().Be("39");
            _Trailer.TransactionSetControlNumber.Should().Be("0001");
            

        }
    }
}
