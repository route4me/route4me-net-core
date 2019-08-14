using System;

namespace Route4MeDB.ApplicationCore.Exceptions
{
    public class ContactNotFoundException : Exception
    {
        public ContactNotFoundException(int addressId) : base($"No address book contact found with id {addressId}")
        {
        }

        protected ContactNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public ContactNotFoundException(string message) : base(message)
        {
        }

        public ContactNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

