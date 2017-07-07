using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using openx12.Models.FunctionalGroup;

namespace openx12.tests.FunctionalGroup
{
    [TestClass]
    public class FunctionalGroupHeaderTests {
        private const string _FunctionalGroupString = "GS*BE*SENDER CODE*RECEIVER CODE*19991231*0802*1*X*005010X220";
        private FunctionalGroupHeader _Header;

        public static string GetTestString() {
            return _FunctionalGroupString;
        }

        [TestInitialize]
        public void Setup() {
            _Header = new FunctionalGroupHeader(_FunctionalGroupString);
        }

        [TestMethod, TestCategory("Unit")]
        public void should_parse_functional_group_header()
        {
            _Header.Name.Should().Be("GS");
            _Header.Elements.Should().HaveCount(8);
            _Header.Value.Should().Be(_FunctionalGroupString);
        }

        [TestMethod, TestCategory("Unit")]
        public void should_parse_provide_correct_derived_properties()
        {
            _Header.FunctionalIdentifierCode.Should().Be("BE");
            _Header.ApplicationSendersCode.Should().Be("SENDER CODE");
            _Header.ApplicationReceiversCode.Should().Be("RECEIVER CODE");
            _Header.DateString.Should().Be("19991231");
            _Header.TimeString.Should().Be("0802");
            _Header.GroupControlNumber.Should().Be("1");
            _Header.ResponsibleAgencyCode.Should().Be("X");
            _Header.VersionReleaseIndustryIdentifier.Should().Be("005010X220");
        }

        [TestMethod, TestCategory("Unit")]
        public void should_provide_correct_creation_date_time() {
            _Header.CreationDateTime.Should().Be(new DateTime(1999, 12, 31, 8, 2, 0));
        }

        [TestMethod, TestCategory("Unit")]
        public void should_read_date_string_from_creation_date_time() {
            var header = new FunctionalGroupHeader("GS*BE*SENDER CODE*RECEIVER CODE*19991231*0802*1*X*005010X220");
            header.CreationDateTime = DateTime.Today;
            header.DateString.Should().Be(DateTime.Today.ToString(openx12.Constants.DateFormatting.CCYYMMDD));
        }

        [TestMethod, TestCategory("Unit")]
        public void should_read_time_string_from_creation_date_time()
        {
            var header = new FunctionalGroupHeader("GS*BE*SENDER CODE*RECEIVER CODE*19991231*0802*1*X*005010X220");
            var now = DateTime.Now;
            header.CreationDateTime = now;
            header.TimeString.Should().Be(now.ToString(openx12.Constants.TimeFormatting.HHMM));
        }

        [TestMethod, TestCategory("Unit")]
        public void should_instantiate_funcitonal_group_header_with_properties()
        {
            var header = new FunctionalGroupHeader {
                FunctionalIdentifierCode = "BE",
                ApplicationSendersCode = "SENDER CODE",
                ApplicationReceiversCode = "RECEIVER CODE",
                CreationDateTime = new DateTime(1999,12,31,08,02,00),
                GroupControlNumber = "1",
                ResponsibleAgencyCode = "X",
                VersionReleaseIndustryIdentifier = "005010X220"
            };

            header.Value.Should().Be(_FunctionalGroupString);
        }
    }
}
