using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WashingtonSalesTaxRateLookup
{
    public class Response
    {
        private int? _ResultCode;
        Dictionary<int,string> _ResultCodeDefinition = new Dictionary<int,string>();

        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public decimal? LocalRate { get; set; }
        public decimal? StateRate { get; set; }
        public decimal? TotalRate { get; set; }
        public int? ResultCode {
            get {
                return _ResultCode;
            }
            set {
                _ResultCode = value;
                SetResultCodeDefinition(_ResultCode);
            }
        }
        public string ResultCodeDefinition { get; set; }

        public string XmlRaw { get; set; }
        public Exception Exception { get; set; }
        
        public Response(){

            _ResultCodeDefinition.Add(0, "The address was found.");
            _ResultCodeDefinition.Add(1, "The address was not found, but the ZIP+4 was  located.");
            _ResultCodeDefinition.Add(2, "Neither the address or ZIP+4 was found, but  the 5-digit ZIP was located.");
            _ResultCodeDefinition.Add(3, "The address, ZIP+4, and ZIP could not be  found.");
            _ResultCodeDefinition.Add(4, "Invalid arguements.");
            _ResultCodeDefinition.Add(5, "Internal error.");
        }

        void SetResultCodeDefinition(int? resultCode)
        {
            if (resultCode != null) {
                ResultCodeDefinition = _ResultCodeDefinition.Where(x => x.Key == resultCode).FirstOrDefault().Value;
            } else {
                ResultCodeDefinition = null;
            }
        }
    }
    
}
