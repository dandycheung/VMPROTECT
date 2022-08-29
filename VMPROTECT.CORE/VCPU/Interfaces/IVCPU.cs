namespace VMPROTECT.CORE.VCPU.Interfaces
{
    public interface IVCPU : IAsyncDisposable
    {
        public void LoadProtectedCode(byte[] code);
        public void Run();
        public void Debug();
    }
}
