namespace VMPROTECT.CORE.VBUS.Interfaces
{
    public interface IVMEM : IAsyncDisposable
    {
        public void LoadProtectedCode(string pathToFile);
        public bool[] LoadFrame(int pc);
    }
}