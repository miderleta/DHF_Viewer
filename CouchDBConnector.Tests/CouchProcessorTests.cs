using CouchDBConnector.Models;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;

namespace CouchDBConnector.Tests
{
    public class CouchProcessorTests
    {
        private CouchProcessor? couchProcessor;
        private ApiHelper apiHelper;

        [SetUp]
        public void Setup()
        {
            couchProcessor = new CouchProcessor();
            apiHelper = new ApiHelper();
        }

        [TearDown]
        public void Teardown()
        {
            couchProcessor = null;
        }

        [Test]
        public void TestGetEndpointAddress_is_BaseAddress()
        {
            string result = couchProcessor.getEndpointAddress();
            Assert.That(result, Is.EqualTo("http://127.0.0.1:5984/dhf_viewer"));
        }

        [TestCase("http://127.0.0.1:5984/dhf_viewer")]
        public async Task TestLoadCouchDataAsync(string value)
        {
            couchProcessor.DatabaseUrl = value;
            string url = couchProcessor.getEndpointAddress();
            apiHelper.InitializeCouchClient();
            HttpResponseMessage response = await ApiHelper.ApiCouchClient.GetAsync(url);
            var result = response.IsSuccessStatusCode;
            Assert.That(result, Is.True);
        }


    }
}