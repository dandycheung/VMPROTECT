namespace VMPROTECT.CORE.Security.Models
{
    public class SecDetails
    {
        private static readonly ushort _magicNumber = 0x566d;
        public static ushort MagicNumber
        {
            get { return _magicNumber; }
        }
    }
}