using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace openx12.tests.Interchange
{
    [TestClass]
    public class InterchangeTests {
        private Models.Interchange.Interchange _Interchange;

        // http://www.x12.org/examples/005010X230/response-to-functional-group-containing-837s/

        private string _X12String = "ISA*00*          *00*          *ZZ*123456789012345*ZZ*123456789012346*080503*1705*>*00501*000010216*0*T*:~" +
                                    "GS*FA*1234567890*2345678901*20080503*1705*20213*X*005010X230~" +
                                    "ST*997*2870001*005010X230~" +
                                    "AK1*HC*17456*005010~" +
                                    "AK2*837*0001~" +
                                    "AK5*A~" +
                                    "AK2*837*0002~" +
                                    "AK3*CLM*22**8~" +
                                    "AK4*1*1028*1~" +
                                    "AK5*R*5~" +
                                    "AK9*P*2*2*1~" +
                                    "SE*10*2870001~" +
                                    "GE*1*20213~" +
                                    "IEA*1*000010216~";

        [TestInitialize]
        public void Setup()
        {
            _Interchange = new Models.Interchange.Interchange(_X12String);
        }

        [TestMethod, TestCategory("Unit")]
        public void should_parse_interchange() {
            _Interchange.FunctionalGroups.Should().HaveCount(1);
            _Interchange.Value.Should().Be(_X12String);
        }

    }
}
