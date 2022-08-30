namespace VMPROTECT.CORE.VBUS.Interfaces
{
    public interface IMemoryController : IAsyncDisposable
    {
        public void LoadProtectedCode(byte[] codeToLoad, int cpuStorageSize);
        public uint GetCode(uint pc, byte[] storage, int storageSize);
    }
}