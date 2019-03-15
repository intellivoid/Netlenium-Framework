using System;
using System.Runtime.Serialization;

namespace Netlenium.Manager
{
    [Serializable]
    internal class PermissionsErrorException : Exception
    {
        public PermissionsErrorException()
        {
            
        }

        public PermissionsErrorException(string message) : base(message)
        {
            
        }

        public PermissionsErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            
        }
    }
}