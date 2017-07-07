using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using openx12.Models;

namespace openx12.tests
{
    [TestClass]
    public class SegmentTests {
        private const string _SegmentString = "DTP*007*D8*19961001";
        private Segment _Segment;

        [TestInitialize]
        public void Setup() {
            _Segment = new Segment(_SegmentString);
        }

        [TestMethod, TestCategory("Unit")]
        public void should_parse_segment_with_3_elements() {
            _Segment.Name.Should().Be("DTP");
            _Segment.Elements.Should().HaveCount(3);
            _Segment.Elements.ElementAt(0).Value.Should().Be("007");
            _Segment.Elements.ElementAt(1).Value.Should().Be("D8");
            _Segment.Elements.ElementAt(2).Value.Should().Be("19961001");
        }

        [TestMethod, TestCategory("Unit")]
        public void should_return_value_matching_input() {
            _Segment.Value.Should().Be(_SegmentString);
        }

        [TestMethod, TestCategory("Unit")]
        public void should_return_qualifier_of_007() {
            _Segment.Qualifier.Should().Be("007");
        }
    }
}
