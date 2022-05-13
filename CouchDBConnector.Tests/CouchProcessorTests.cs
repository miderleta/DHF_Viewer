using NUnit.Framework;

namespace CouchDBConnector.Tests
{
    public class CouchProcessorTests
    {
        private CouchProcessor? couchProcessor;

        [SetUp]
        public void Setup()
        {
            couchProcessor = new CouchProcessor();
        }

        [TearDown]
        public void Teardown()
        {
            couchProcessor = null;
        }

        [Test]
        public void SetEndpointAddress_NoName_BaseAddress()
        {
            couchProcessor.setEndpointAddress("");
            string result = couchProcessor.getEndpointAddress();
            Assert.That(result, Is.EqualTo("http://127.0.0.1:5984/"));
        }

        [TestCase("", "http://127.0.0.1:5984/")]
        [TestCase("databaseName", "http://127.0.0.1:5984/databaseName")]
        public void GetEndpointAddressTest(string value, string result)
        {
            couchProcessor.setEndpointAddress(value);
            Assert.That(result, Is.EqualTo(couchProcessor.getEndpointAddress()));
        }
    }
}