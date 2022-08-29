namespace VMPROTECT.CORE.VCPU.Exceptions
{
    [Serializable]
    public class InvalidLoad : Exception
    {
        public InvalidLoad() { }
        public InvalidLoad(string message) : base(String.Format("Invalid load: {0}", message)) { }
        public InvalidLoad(string message, Exception innerException) : base(String.Format("Invalid load: {0}", message), innerException) { }
    }
}