
namespace VMPROTECT.CORE.VBUS.Models
{
    public class MemoryDetails
    {
        private static readonly int _codeDataSize = 512;
        private static readonly int _stackSize = 256;
        public static int CodeDataSize
        {
            get => _codeDataSize;
        }
        public static int StackSize
        {
            get => _stackSize;
        }
    }
}
