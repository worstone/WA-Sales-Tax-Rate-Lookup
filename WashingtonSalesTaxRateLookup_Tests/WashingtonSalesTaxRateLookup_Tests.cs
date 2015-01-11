using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using WashingtonSalesTaxRateLookup;

namespace WashingtonSalesTaxRateLookup_Tests
{
    [TestFixture]
    public class WashingtonSalesTaxRateLookup_Tests
    {

        [Test]
        public void Good_Address_Test() {
            var address = new Address("6500 Linderson way", "", "98501");
            var client = new Client();
            var response = client.Get(address);
            Assert.AreEqual(0, response.ResultCode);
            Assert.AreEqual("3406", response.LocationCode);
            Assert.NotNull(response.ResultCodeDefinition);
            Assert.NotNull(response.LocalRate);
            Assert.NotNull(response.LocationCode);
            Assert.NotNull(response.LocationName);
            Assert.NotNull(response.StateRate);
        }

        [Test]
        public void Bad_ZipCode_Test() {
            var address = new Address("6500 Linderson way", "", "980501");
            var client = new Client();
            var response = client.Get(address);
            Assert.AreEqual(3, response.ResultCode);
            Assert.NotNull(response.ResultCodeDefinition);
            Assert.IsNull(response.LocalRate);
            Assert.IsNull(response.LocationCode);
            Assert.IsNull(response.LocationName);
            Assert.IsNull(response.StateRate);
        }

        [Test]
        public void No_Address_Test() {
            var address = new Address("", "", "98501");
            var client = new Client();
            var response = client.Get(address);
            Assert.AreEqual(2, response.ResultCode);
            Assert.NotNull(response.ResultCodeDefinition);
            Assert.NotNull(response.LocalRate);
            Assert.NotNull(response.LocationCode);
            Assert.NotNull(response.LocationName);
            Assert.NotNull(response.StateRate);
        }

        [Test]
        public void Bad_Address_Good_ZipCode_Test() {
            var address = new Address("&&&&&&&&&&&&&", "", "98501");
            var client = new Client();
            var response = client.Get(address);
            Assert.AreEqual(2, response.ResultCode);
            Assert.NotNull(response.ResultCodeDefinition);
            Assert.NotNull(response.LocalRate);
            Assert.NotNull(response.LocationCode);
            Assert.NotNull(response.LocationName);
            Assert.NotNull(response.StateRate);
        }

        [Test]
        public void Good_Address_Bad_ZipCode_Test() {
            var address = new Address("6500 Linderson way", "", "9111118501");
            var client = new Client();
            var response = client.Get(address);
            Assert.AreEqual(4, response.ResultCode);
            Assert.NotNull(response.ResultCodeDefinition);
            Assert.IsNull(response.LocalRate);
            Assert.IsNull(response.LocationCode);
            Assert.IsNull(response.LocationName);
            Assert.IsNull(response.StateRate);
        }

        [Test]
        public void No_Address_Bad_ZipCode_Test() {
            var address = new Address(null, null, "9111118501");
            var client = new Client();
            var response = client.Get(address);
            Assert.AreEqual(4, response.ResultCode);
            Assert.NotNull(response.ResultCodeDefinition);
            Assert.IsNull(response.LocalRate);
            Assert.IsNull(response.LocationCode);
            Assert.IsNull(response.LocationName);
            Assert.IsNull(response.StateRate);
        }
    }
}
