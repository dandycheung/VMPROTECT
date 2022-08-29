namespace VMPROTECT.CORE.VBUS.Exceptions
{
    [Serializable]
    public class InvalidIpAddress : Exception
    {
        public InvalidIpAddress() { }
        public InvalidIpAddress(string message) : base(String.Format("Invalid IP address: {0}", message)) { }
        public InvalidIpAddress(string message, Exception innerException) : base(String.Format("Invalid IP address: {0}", message), innerException) { }
    }
}