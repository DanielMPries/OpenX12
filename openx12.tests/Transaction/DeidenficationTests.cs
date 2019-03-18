using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using openx12.Models.Transaction;
using openx12.Models;
using XTransaction = openx12.Models.Transaction.Transaction;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System;

namespace openx12.tests.Transaction
{
    [TestClass]
    public class DeidenficationTests
    {
        private XTransaction _Transaction;

        [TestInitialize]
        public void Setup() {
            var path = System.IO.Path.Combine(".","TestData","Sample834.txt");
            var doc = X12Document.Read(path);
            _Transaction = doc
                    .Interchanges.First()
                    .FunctionalGroups.First()
                    .TransactionSets.First();
        }


        [TestMethod, TestCategory("Unit")]
        public void should_parse_transaction() {
            var sub = _Transaction.Segments
                .SingleOrDefault( x => x.Name == "NM1" && x.Qualifier == "IL");
            sub.Elements[2].Value.Should().Be("JOHN DOE");
        }

        [TestMethod, TestCategory("Unit")]
        public void should_deidentify_name() {
            var sub = _Transaction.Segments
                .SingleOrDefault( x => x.Name == "NM1" && x.Qualifier == "IL");
            sub.Elements[2].Value = "XXXX XXX";
            //System.IO.File.WriteAllText("output.txt", _Transaction.ToString());
            _Transaction.ToString().Should().Contain("NM1*IL*1*XXXX XXX");
        }
    }
}
