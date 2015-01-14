#region License
//   Copyright 2015 Ken Worst - R.C. Worst & Company Inc.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License. 
#endregion

using NUnit.Framework;
using WaTaxRateLookup;

namespace WaTaxRateLookup_Test
{
    [TestFixture]
    public class Wa_Tax_Rate_Lookup_Tests
    {

        [Test]
        public void Good_Address_Test() {
            var address = new Address("6500 Linderson way", "Olympia", "98501");
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
