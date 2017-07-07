
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using openx12.Models;

namespace openx12.tests
{
    [TestClass]
    public class ComponentElementTests
    {
        [TestMethod, TestCategory("Unit")]
        public void should_parse_string_element_as_3_component_elements() {
            var components = ComponentElement.TryParse("01:02:03", FormattingOptions.DefaultOptions);
            components.Should().HaveCount(3);
            components.ElementAt(0).Value.Should().Be("01");
            components.ElementAt(1).Value.Should().Be("02");
            components.ElementAt(2).Value.Should().Be("03");
        }
    }
}
