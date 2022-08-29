namespace VMPROTECT.CORE.VCPU.Exceptions
{
    [Serializable]
    public class InvalidOpcode : Exception
    {
        public InvalidOpcode() { }
        public InvalidOpcode(string message) : base(String.Format("Invalid opcode: {0}", message)) { }
        public InvalidOpcode(string message, Exception innerException) : base(String.Format("Invalid opcode: {0}", message), innerException) { }
    }
}