namespace VMPROTECT.CORE.VBUS.Exceptions
{
    [Serializable]
    public class InvalidFile : Exception
    {
        public InvalidFile() { }
        public InvalidFile(string message) : base(String.Format("Invalid operation on file: {0}", message)) { }
        public InvalidFile(string message, Exception innerException) : base(String.Format("Invalid operation on file: {0}", message), innerException) { }
    }
}
