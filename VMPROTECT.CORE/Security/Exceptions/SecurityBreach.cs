namespace VMPROTECT.CORE.Security.Exceptions
{
    [Serializable]
    public class SecurityBreach : Exception
    {
        public SecurityBreach() { }
        public SecurityBreach(string message) : base(String.Format("Security breach: {0}", message)) { }
        public SecurityBreach(string message, Exception innerException) : base(String.Format("Security breach: {0}", message), innerException) { }
    }
}