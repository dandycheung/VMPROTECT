namespace VMPROTECT.CORE.VBUS.Exceptions
{
    [Serializable]
    public class ErrorInMemoryController : Exception
    {
        public ErrorInMemoryController() { }
        public ErrorInMemoryController(string message) : base(String.Format("Error in memory controller: {0}", message)) { }
        public ErrorInMemoryController(string message, Exception innerException) : base(String.Format("Error in memory controller: {0}", message), innerException) { }
    }
}
