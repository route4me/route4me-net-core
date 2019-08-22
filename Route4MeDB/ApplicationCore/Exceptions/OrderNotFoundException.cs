using System;

namespace Route4MeDB.ApplicationCore.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(int orderId) : base($"No order found with id {orderId}")
        {
        }

        protected OrderNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public OrderNotFoundException(string message) : base(message)
        {
        }

        public OrderNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
