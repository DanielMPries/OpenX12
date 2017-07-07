using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using openx12.Models;

namespace openx12.tests
{
    [TestClass]
    public class X12DocumentTests
    {
        [TestMethod, TestCategory("Unit")]
        public void should_read_sample_997() {
            var doc = X12Document.Read(@".\TestData\Sample997.txt");
            doc.Interchanges.Should().HaveCount(1);
        }
    }
}
