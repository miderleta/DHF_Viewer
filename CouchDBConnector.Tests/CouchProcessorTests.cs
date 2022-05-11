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
        public void GetEndpointAddress_Null_ReturnsEmpty()
        {
            string result = couchProcessor.getEndpointAddress();
            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GetEndpointAddress_databseName_ReturnsdatabseName()
        {
            couchProcessor.setEndpointAddress("databaseName");
            string result = couchProcessor.getEndpointAddress();
            Assert.That(result, Is.EqualTo("http://127.0.0.1:5984/databaseName"));
        }

        [Test]
        public void SetEndpointAddress_NoName_BaseAddress()
        {
            couchProcessor.setEndpointAddress("");
            string result = couchProcessor.getEndpointAddress();
            Assert.That(result, Is.EqualTo("http://127.0.0.1:5984/"));
        }
    }
}