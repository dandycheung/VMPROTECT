using VMPROTECT.CORE.VBUS.Models;
using VMPROTECT.CORE.VCPU.Exceptions;
using VMPROTECT.CORE.VCPU.Interfaces;
using VMPROTECT.VMEM;

namespace VMPROTECT.VCPU
{
    public partial class OneCoreCpu : IVCPU
    {
        private bool _disposed = false;
        private AddressSpace? _addressSpace;
        private Registerss? _registerss;
        private NetDetails? _netDetails;
        private readonly int _regCount = 8;
        private bool _isProcessingFinished = false;
        private MemoryController? memoryController;

        public OneCoreCpu()
        {
            _registerss = new();
            _registerss.Regs = new uint[_regCount];
            _registerss.ZeroFlag = 0;
            _registerss.CarryFlag = 0;
            _registerss.PC = 0;
            _registerss.SP = (uint) MemoryDetails.StackSize / sizeof(uint);
            _addressSpace = new();
            _addressSpace.Code = new byte[MemoryDetails.CodeDataSize];
            _addressSpace.Stack = new uint[MemoryDetails.StackSize];
        }

        public OneCoreCpu(MemoryController memoryController) : this()
        {
            this.memoryController = memoryController;
            if (_addressSpace is not null && _addressSpace.Code is not null)
            {
                _ = memoryController.GetCode(0, _addressSpace.Code, MemoryDetails.CodeDataSize);
            }
            else
            {
                throw new InvalidLoad("Issue with load from memory controller");
            }
        }

        public void LoadProtectedCode(byte[] code)
        {
            if (code.Length == 0)
            {
                throw new InvalidLoad("No protected code was provided");
            }

            if (_addressSpace is not null && _addressSpace.Code is not null)
            {
                var lengthToRead = MemoryDetails.CodeDataSize;
                if (code.Length < lengthToRead)
                {
                    lengthToRead = code.Length;
                }
                Array.Copy(code, 0, _addressSpace.Code, 0, lengthToRead);
            }
            else
            {
                throw new InvalidLoad("Address space not allocated");
            }
        }

        public void Run()
        {
            if (_registerss is not null && _addressSpace is not null
                && _addressSpace.Code is not null && memoryController is not null)
            {
                while (!_isProcessingFinished)
                {
                    if (_registerss.PC >= _addressSpace.Code.Length)
                    {
                        _registerss.PC = (uint) memoryController.GetCode((uint)_registerss.PC, _addressSpace.Code, MemoryDetails.CodeDataSize);
                    }
                    Execute(_addressSpace.Code[(int)_registerss.PC]);
                    _registerss.PC += 1;
                    _isProcessingFinished = _addressSpace.Code[(int)_registerss.PC] == 0xEE ? true : false;
                }
            }
            else
            {
                throw new InvalidLoad("No all VCPU components are set");
            }
        }

        public void Debug()
        {
            throw new NotImplementedException();
        }

        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if(!_disposed)
            {
                if(disposing)
                {
                    await Task.Run(() =>
                    {
                        if (_addressSpace is not null)
                        {
                            _addressSpace.Code = null;
                            _addressSpace.Stack = null;
                        }
                        _addressSpace = null;
                        _registerss = null;
                        _netDetails = null;
                    });
                }
                _disposed = true;
            }
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }
    }
}