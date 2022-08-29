namespace VMPROTECT.CORE.Security.Models
{
    public class SecFile
    {
        private static string _codeHash = "";
        private static byte[] _code = Array.Empty<byte>();
        public static string CodeHash { get => _codeHash; set => _codeHash = value; }
        public static byte[] Code { get => _code; }
    }
}
