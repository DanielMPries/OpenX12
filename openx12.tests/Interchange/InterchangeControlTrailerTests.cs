using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using openx12.Models.Interchange;

namespace openx12.tests.Interchange
{
    [TestClass]
    public class InterchangeControlTrailerTests {
        private const string _InterchangeControlString = "IEA*1*000000905";
        private InterchangeControlTrailer _Trailer;

        public static string GetTestString() {
            return _InterchangeControlString;
        }

        [TestInitialize]
        public void Setup() {
            _Trailer = new InterchangeControlTrailer(_InterchangeControlString);
        }

        [TestMethod, TestCategory("Unit")]
        public void should_parse_interchange_control_trailer() {
            _Trailer.Name.Should().Be("IEA");
            _Trailer.Elements.Should().HaveCount(2);
            _Trailer.Value.Should().Be(_InterchangeControlString);
        }

        [TestMethod, TestCategory("Unit")]
        public void should_parse_provide_correct_derived_properties()
        {
            _Trailer.IncludedFunctionalGroups.Should().Be(1);
            _Trailer.InterchangeControlNumber.Should().Be("000000905");
        }

        [TestMethod, TestCategory("Unit")]
        public void should_instantiate_interchange_control_trailer_with_properties()
        {
            var trailer = new InterchangeControlTrailer() {
                IncludedFunctionalGroups = 10,
                InterchangeControlNumber = "1"
            };

            trailer.Value.Should().Be("IEA*10*1");
        }
    }
}
