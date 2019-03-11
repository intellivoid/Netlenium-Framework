using System;
using System.Runtime.Serialization;

namespace Netlenium.Driver
{
    [Serializable]
    internal class VersionFileNotFoundException : Exception
    {
        public VersionFileNotFoundException()
        {
        }

        public VersionFileNotFoundException(string message) : base(message)
        {
        }

        public VersionFileNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VersionFileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}