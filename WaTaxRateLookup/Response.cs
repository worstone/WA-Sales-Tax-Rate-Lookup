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
using System.Collections.Generic;
using System.Linq;

namespace WaTaxRateLookup
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
