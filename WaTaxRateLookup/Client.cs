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
