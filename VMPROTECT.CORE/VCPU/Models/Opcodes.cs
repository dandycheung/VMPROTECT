using VMPROTECT.CORE.VCPU.Exceptions;

namespace VMPROTECT.CORE.VCPU.Models
{
    public class Opcodes
    {
        private string _opcode = "NOP";
        private readonly Dictionary<string, byte> _codes = new()
        {
            { "NOP", 0x00 },
            { "EE", 0xEE },
            { "MOV", 0x01 },
            { "MOVMB", 0x02 },
            { "MOVMW", 0x03 },
            { "MOVB", 0x04 },
            { "MOVW", 0x05 },
            { "MOVBM", 0x06 },
            { "MOVWM", 0x07 },
            { "MOVMRB", 0x08 },
            { "MOVMRW", 0x09 },
            { "MOVMD", 0x0A },
            { "MOVD", 0x0B },
            { "MOVDM", 0x0C },
            { "MOVMRD", 0x0D },
            { "JMP", 0x20 },
            { "JZ", 0x21 },
            { "JNZ", 0x22 },
            { "JAE", 0x23 },
            { "JBE", 0x24 },
            { "JB", 0x25 },
            { "JA", 0x26 },
            { "ADVR", 0x30 },
            { "ADRR", 0x31 },
            { "ADRRL", 0x32 },
            { "SUBVR", 0x33 },
            { "SUBRR", 0x34 },
            { "SUBRRL", 0x35 },
            { "XOR", 0x36 },
            { "XORL", 0x37 },
            { "NOT", 0x38 },
            { "NOTB", 0x39 },
            { "ADVRD", 0x3A },
            { "SUBVRD", 0x3B },
            { "SHR", 0x3C },
            { "SHL", 0x3D },
            { "CMP", 0x50 },
            { "CMPL", 0x51 },
            { "VMSYSBUS", 0x60 },
            { "PUSH", 0x90 },
            { "POP", 0x91 },
            { "CLST", 0x92 },
            { "SETSP", 0x93 },
            { "POC", 0xA0 },
            { "POCN", 0xA1 },
            { "TIB", 0xA2 },
            { "GIC", 0xA3 },
            { "PIC", 0xA4 },
            { "PICN", 0xA5 },
            { "PXV", 0xA6 },
            { "PXVN", 0xA7 }
        };
        private List<string>? _allOpcodesName;
        public byte Code
        {
            get
            {
                if (_codes.ContainsKey(_opcode))
                {
                    return _codes[_opcode];
                }
                else
                {
                    throw new InvalidOpcode(String.Format("{0}", _opcode));
                }
            }
        }
        public string Opcode
        {
            get
            {
                return _opcode;
            }
            set
            {
                _opcode = value.ToUpper();
            }
        }
        public List<string> AllOpcodes
        {
            get => _allOpcodesName == null ? _allOpcodesName = new(_codes.Keys) : _allOpcodesName;
        }
    }
}
