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

using System;
using RestSharp;
using System.Xml;

namespace WaTaxRateLookup
{
    public class Client
    {
        //http://dor.wa.gov/Content/FindTaxesAndRates/RetailSalesTax/DestinationBased/ClientInterface.aspx

        const string LOOKUP_URI = @"http://dor.wa.gov/AddressRates.aspx";

        public Response Get(Address address) {

            var response = new Response();

            var request = new RestRequest();
            request.Method = Method.GET;
            request.AddParameter("output", "xml", ParameterType.QueryString);
            request.AddParameter("addr", address.Street, ParameterType.QueryString);
            request.AddParameter("city", address.City, ParameterType.QueryString);
            request.AddParameter("zip", address.ZipCode, ParameterType.QueryString);

            var client = new RestClient(LOOKUP_URI);

            IRestResponse rest_response = client.Execute(request);
            
            if (rest_response.StatusCode == System.Net.HttpStatusCode.OK) {

                try {
                    var xml_doc = new XmlDocument();
                    xml_doc.LoadXml(rest_response.Content);
                    XmlNode node = xml_doc.SelectSingleNode("/response");

                    response.XmlRaw = rest_response.Content;
                    response.ResultCode = int.Parse(node.Attributes.GetNamedItem("code").Value);
                    response.LocalRate = decimal.Parse(node.Attributes.GetNamedItem("localrate").Value);
                    response.TotalRate = decimal.Parse(node.Attributes.GetNamedItem("rate").Value);
                    response.LocationCode = node.Attributes.GetNamedItem("loccode").Value;
                    
                    node = xml_doc.SelectSingleNode("/response/rate");
                    response.StateRate = decimal.Parse(node.Attributes.GetNamedItem("staterate").Value);
                    response.LocationName = node.Attributes.GetNamedItem("name").Value;


                } catch (Exception ex) {
                    response.Exception = ex;
                }
            } else if (rest_response.StatusCode == System.Net.HttpStatusCode.InternalServerError) {
                response.Exception = new TaxRateLookupException("Internal Server Error", rest_response.ErrorException);
            } else {
                response.Exception = new TaxRateLookupException("Unknown Error", rest_response.ErrorException);
            }

            return response;
        }
    }

}
