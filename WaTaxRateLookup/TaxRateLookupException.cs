using System;

namespace WaTaxRateLookup
{
    public class TaxRateLookupException : Exception
    {
        public TaxRateLookupException() {
            
        }
        public TaxRateLookupException(string message)
            : base(message) {
            
        }
        public TaxRateLookupException(string message, Exception innerException)
            : base(message, innerException) {
            
        }
        protected TaxRateLookupException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
            
        }

    }
}
