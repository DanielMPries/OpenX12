using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using openx12.Models.Interchange;
using openx12.Models.Interchange.Qualifiers;
using openx12.tests.Interchange;
namespace openx12.tests.Interchange
{
    [TestClass]
    public class InterchangeControlHeaderTests
    {
        private const string _InterchangeControlHeaderString = "ISA*00*..........*01*SECRET....*ZZ*SUBMITTERS.ID..*ZZ*RECEIVERS.ID...*030101*1253*^*00501*000000905*1*T*:~";
        private InterchangeControlHeader _Header;

        public static string GetTestString()
        {
            return _InterchangeControlHeaderString;
        }

        [TestInitialize]
        public void Setup()
        {
            _Header = new InterchangeControlHeader(_InterchangeControlHeaderString);
        }

        [TestMethod, TestCategory("Unit")]
        public void should_parse_interchange_control_header()
        {
            _Header.Name.Should().Be("ISA");
            _Header.Elements.Should().HaveCount(16);
            (_Header.Value + _Header.FormattingOptions.SegmentTerminator).Should().Be(_InterchangeControlHeaderString);
        }

        [TestMethod, TestCategory("Unit")]
        public void should_parse_provide_correct_derived_properties()
        {
            _Header.AuthortizationInformationType.Should().Be(AuthortizationInformationType.NoAuthorizationInformationPresent);
            _Header.AuthortizationInformation.Should().Be("..........");
            _Header.SecurityInformationType.Should().Be(SecurityInformationType.Password);
            _Header.SecurityInformation.Should().Be("SECRET....");
            _Header.SenderInterchangeIdType.Should().Be(InterchangeIdType.MutuallyDefined);
            _Header.InterchangeSenderId.Should().Be("SUBMITTERS.ID..");
            _Header.RecieverInterchangeIdType.Should().Be(InterchangeIdType.MutuallyDefined);
            _Header.InterchangeReceiverId.Should().Be("RECEIVERS.ID...");
            //_Header.InterchangeDateTime
            //_Header.InterchangeDateTime
            _Header.RepetitionSeparator.Should().Be("^");
            _Header.InterchangeControlVersionNumber.Should().Be("00501");
            _Header.InterchangeControlNumber.Should().Be("000000905");
            _Header.AcknowledgementRequestedType.Should().Be(AcknowledgementRequestedType.InterchangeAcknowledgmentRequested);
            _Header.InterchangeUsageIndicatorType.Should().Be(InterchangeUsageIndicatorType.TestData);
            _Header.ComponentElementSeperator.Should().Be(":");
        }
    }
}

