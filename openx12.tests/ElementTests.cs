using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using openx12.Models;

namespace openx12.tests
{
    [TestClass]
    public class ElementTests
    {
        [TestMethod, TestCategory("Unit")]
        public void should_parse_string_elements_with_no_composites() {
            var element = new Element("test", FormattingOptions.DefaultOptions);
            element.Value.Should().Be("test");
            element.ComponentElements.Should().HaveCount(1);
        }

        [TestMethod, TestCategory("Unit")]
        public void should_parse_string_elements_with_composites() {
            var element = new Element("01:02:03", FormattingOptions.DefaultOptions);
            element.Value.Should().Be("01:02:03");
            element.ComponentElements.Count.Should().Be(3);
        }
    }
}
