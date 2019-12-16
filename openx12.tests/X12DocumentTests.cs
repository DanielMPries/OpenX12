using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using openx12.Models;
using openx12.documents;
using System.Text;

namespace openx12.tests
{
    //http://www.x12.org/examples/005010X220/
    [TestClass]
    public class X12DocumentTests
    {
        [TestMethod, TestCategory("Unit")]
        public void should_read_sample_997() {
            var doc = X12Document.Read(@".\TestData\Sample997.txt");
            doc.Interchanges.Should().HaveCount(1);
        }

        [TestMethod, TestCategory("Unit")]
        public void should_read_sample_834() {
            var doc = X12Document.Read(System.IO.Path.Combine(".", "TestData", "834.txt"));
            var tx = doc.Interchanges.FirstOrDefault()
                .FunctionalGroups.FirstOrDefault()
                .TransactionSets.FirstOrDefault();
            var mapper = new openx12.documents.healthcare.X00510_834();
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            var tree = mapper.Map(tx);
            var e = sw.Elapsed;
            var s = new StringBuilder();
            foreach (var node in tree)
            {
                string indent = CreateIndent(node.Level);
                s.AppendLine($"{node.Data.Index.Loop,-5}: {indent}{node}");
            }
            sw.Stop();
            var path = System.IO.Path.Combine(".", "TestData", "834output.txt");
            System.IO.File.WriteAllText(path,s.ToString());
            System.IO.File.AppendAllLines(path, new [] {$"\nElapsed Time: {e:c}"});
            doc.Interchanges.Should().HaveCount(1);
        }

        private static string CreateIndent(int depth)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < depth; i++)
            {
                sb.Append("--");
            }
            return sb.ToString();
        }
    }
}
