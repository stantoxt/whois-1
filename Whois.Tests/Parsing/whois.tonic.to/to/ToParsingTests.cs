using NUnit.Framework;
using Whois.Models;
using Whois.Parsers;

namespace Whois.Parsing.Whois.Tonic.To.To
{
    [TestFixture]
    public class ToParsingTests : ParsingTests
    {
        private WhoisParser parser;

        [SetUp]
        public void SetUp()
        {
            SerilogConfig.Init();

            parser = new WhoisParser();
        }

        [Test]
        public void Test_not_found()
        {
            var sample = SampleReader.Read("whois.tonic.to", "to", "not_found.txt");
            var response = parser.Parse("whois.tonic.to", "to", sample);

            Assert.Greater(sample.Length, 0);
            Assert.AreEqual(WhoisResponseStatus.NotFound, response.Status);

            Assert.AreEqual(0, response.ParsingErrors);
            Assert.AreEqual("whois.tonic.to/to/NotFound", response.TemplateName);

            Assert.AreEqual("u34jedzcq", response.DomainName);

            Assert.AreEqual(2, response.FieldsParsed);
        }

        [Test]
        public void Test_found()
        {
            var sample = SampleReader.Read("whois.tonic.to", "to", "found.txt");
            var response = parser.Parse("whois.tonic.to", "to", sample);

            Assert.Greater(sample.Length, 0);
            Assert.AreEqual(WhoisResponseStatus.Found, response.Status);

            Assert.AreEqual(0, response.ParsingErrors);
            Assert.AreEqual("whois.tonic.to/to/Found", response.TemplateName);

            // Nameservers
            Assert.AreEqual(2, response.NameServers.Count);
            Assert.AreEqual("ns-1.myphotoalbum.com", response.NameServers[0]);
            Assert.AreEqual("ns-2.myphotoalbum.com", response.NameServers[1]);

            Assert.AreEqual(3, response.FieldsParsed);
        }
    }
}
