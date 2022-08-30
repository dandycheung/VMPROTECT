using VMPROTECT.CORE.Security.Models;
using VMPROTECT.CORE.VBUS.Exceptions;
using VMPROTECT.CORE.VBUS.Interfaces;
using VMPROTECT.CORE.VBUS.Models;
using VMPROTECT.CORE.VCPU.Models;
using VMPROTECT.UTILS;

namespace VMPROTECT.VMEM
{
    public class MemoryController : IMemoryController
    {
        private bool _disposed = false;
        private byte[]? _data;
        private int _currentFrameNumber;
        private int _frameCounter;
        private Dictionary<int, int>? _frameSize;

        public MemoryController() { }

        private int CheckOpcodeSize(byte opcode, bool isOpcode)
        {
            if (!isOpcode)
            {
                return 0;
            }
            else
            {
                Opcodes.Opcode = opcode;
                return Opcodes.Code.Value;
            }
        }

        public void LoadProtectedCode(byte[] codeToLoad, int cpuStorageSize)
        {
            _data = new byte[codeToLoad.Length - SecDetails.MagicNumberSize];
            Array.Copy(codeToLoad, SecDetails.MagicNumberSize, _data, 0, codeToLoad.Length - SecDetails.MagicNumberSize);

            _frameCounter = 0;
            _frameSize = new Dictionary<int, int>();
            if (_data.Length > cpuStorageSize)
            {
                int countBytes = 0;
                bool isOpcode;
                int opcodeArgCount = 0;
                foreach(var byteInMem in _data)
                {
                    countBytes += 1;
                    if (opcodeArgCount == 0)
                    {
                        isOpcode = true;
                        opcodeArgCount = CheckOpcodeSize(byteInMem, isOpcode);
                    }
                    else
                    {
                        isOpcode = false;
                        opcodeArgCount -= 1;
                    }
                    
                    if((countBytes + CheckOpcodeSize(byteInMem, isOpcode)) > cpuStorageSize)
                    {
                        _frameCounter += 1;
                        _frameSize[_frameCounter - 1] = countBytes - 1;
                        countBytes = 1;
                    }
                }
            }
            else
            {
                _frameCounter = 1;
                _frameSize[_frameCounter] = _data.Length;
            }
            _currentFrameNumber = 0;
        }

        public void LoadProtectedCode(string pathToFile, int cpuStorageSize)
        {
            if(String.IsNullOrEmpty(pathToFile) && !File.Exists(pathToFile))
            {
                throw new InvalidFile("File does not exist or wrong path");
            }
            var readDataFromFile = File.ReadAllBytes(pathToFile);

            ValidateCode.ValidateMagicNumber(readDataFromFile);
            LoadProtectedCode(readDataFromFile, cpuStorageSize);
        }

        public uint GetCode(uint pc, byte[] storage, int storageSize) // to verify!!!!!
        {
            int frameNumber = -1;
            int sumCodeBlocksSize = 0;

            if (_frameSize is not null)
            {
                if (_frameSize[_currentFrameNumber] == pc)
                {
                    frameNumber = _currentFrameNumber + 1;
                    pc = 0;
                    sumCodeBlocksSize = _frameSize[frameNumber];
                }
                else
                {
                    foreach (var fs in _frameSize)
                    {
                        sumCodeBlocksSize += fs.Value;
                        if (sumCodeBlocksSize > (pc + 1))
                        {
                            frameNumber = fs.Key;
                            break;
                        }
                    }
                }
            }
            else
            {
                throw new ErrorInMemoryController("Frames are not set");
            }

            if (_data is not null)
            {
                if (frameNumber == -1)
                {
                    _currentFrameNumber = 0;
                    Array.Copy(_data, 0, storage, 0, storageSize);
                    return pc;
                }

                int countSize = 0;
                _currentFrameNumber = frameNumber;
                for (int i = 0; i < frameNumber; i++)
                {
                    countSize += _frameSize[i];
                }
                Array.Copy(_data, countSize, storage, 0, _frameSize[_currentFrameNumber]);

                return (uint)_frameSize[_currentFrameNumber] - ((uint)sumCodeBlocksSize - pc);
            }
            else
            {
                throw new ErrorInMemoryController("Memory container is not set");
            }
        }

        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    await Task.Run(() =>
                    {
                        _data = null;
                        _frameSize = null;
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