using NUnit.Framework;
using OEDify;

namespace OedifyTests
{
    [TestFixture]
    public class OEDClientTests
    {
        private OEDClient client;

        [OneTimeSetUp]
        public void Setup()
        {
            client = new OEDClient();
        }

        [TestCase]
        public void TestMethod1()
        {
            var result = client.Do();

            Assert.True(result != null);
        }
    }
}
