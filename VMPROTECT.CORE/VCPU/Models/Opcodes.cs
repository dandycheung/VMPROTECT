using System.Collections;
using VMPROTECT.CORE.VCPU.Exceptions;

namespace VMPROTECT.CORE.VCPU.Models
{
    public static class Opcodes
    {
        private static byte _opcode = 0x00;
        private static readonly Dictionary<byte, KeyValuePair<string, int>> _codes = new()
        {
            { 0x00, new KeyValuePair<string, int>("NOP", 0) },
            { 0xEE, new KeyValuePair<string, int>("EE", 0) },
            { 0x01, new KeyValuePair<string, int>("MOV", 2) },
            { 0x02, new KeyValuePair<string, int>("MOVMB", 3) },
            { 0x03, new KeyValuePair<string, int>("MOVMW", 3) },
            { 0x04, new KeyValuePair<string, int>("MOVB", 2) },
            { 0x05, new KeyValuePair<string, int>("MOVW", 3) },
            { 0x06, new KeyValuePair<string, int>("MOVBM", 3) },
            { 0x07, new KeyValuePair<string, int>("MOVWM", 3) },
            { 0x08, new KeyValuePair<string, int>("MOVMRB", 2) },
            { 0x09, new KeyValuePair<string, int>("MOVMRW", 2) },
            { 0x0A, new KeyValuePair<string, int>("MOVMD", 3) },
            { 0x0B, new KeyValuePair<string, int>("MOVD", 5) },
            { 0x0C, new KeyValuePair<string, int>("MOVDM", 3) },
            { 0x0D, new KeyValuePair<string, int>("MOVMRD", 2) },
            { 0x20, new KeyValuePair<string, int>("JMP", 2) },
            { 0x21, new KeyValuePair<string, int>("JZ", 2) },
            { 0x22, new KeyValuePair<string, int>("JNZ", 2) },
            { 0x23, new KeyValuePair<string, int>("JAE", 2) },
            { 0x24, new KeyValuePair<string, int>("JBE", 2) },
            { 0x25, new KeyValuePair<string, int>("JB", 2) },
            { 0x26, new KeyValuePair<string, int>("JA", 2) },
            { 0x30, new KeyValuePair<string, int>("ADVR", 3) },
            { 0x31, new KeyValuePair<string, int>("ADRR", 2) },
            { 0x32, new KeyValuePair<string, int>("ADRRL", 2) },
            { 0x33, new KeyValuePair<string, int>("SUBVR", 3) },
            { 0x34, new KeyValuePair<string, int>("SUBRR", 2) },
            { 0x35, new KeyValuePair<string, int>("SUBRRL", 2) },
            { 0x36, new KeyValuePair<string, int>("XOR", 2) },
            { 0x37, new KeyValuePair<string, int>("XORL", 2) },
            { 0x38, new KeyValuePair<string, int>("NOT", 1) },
            { 0x39, new KeyValuePair<string, int>("NOTB", 1) },
            { 0x3A, new KeyValuePair<string, int>("ADVRD", 5) },
            { 0x3B, new KeyValuePair<string, int>("SUBVRD", 5) },
            { 0x3C, new KeyValuePair<string, int>("SHR", 2) },
            { 0x3D, new KeyValuePair<string, int>("SHL", 2) },
            { 0x50, new KeyValuePair<string, int>("CMP", 2) },
            { 0x51, new KeyValuePair<string, int>("CMPL", 2) },
            { 0x60, new KeyValuePair<string, int>("VMSYSBUS", 1) },
            { 0x90, new KeyValuePair<string, int>("PUSH", 1) },
            { 0x91, new KeyValuePair<string, int>("POP", 1) },
            { 0x92, new KeyValuePair<string, int>("CLST", 0) },
            { 0x93, new KeyValuePair<string, int>("SETSP", 4) },
            { 0xA0, new KeyValuePair<string, int>("POC", 0) },
            { 0xA1, new KeyValuePair<string, int>("POCN", 0) },
            { 0xA2, new KeyValuePair<string, int>("TIB", 0) },
            { 0xA3, new KeyValuePair<string, int>("GIC", 1) },
            { 0xA4, new KeyValuePair<string, int>("PIC", 0) },
            { 0xA5, new KeyValuePair<string, int>("PICN", 0) },
            { 0xA6, new KeyValuePair<string, int>("PXV", 0) },
            { 0xA7, new KeyValuePair<string, int>("PXVN", 0) }
        };
        private static List<string>? _allOpcodesName;
        public static KeyValuePair<string, int> Code
        {
            get => _codes[_opcode];
        }
        public static byte Opcode
        {
            get
            {
                return _opcode;
            }
            set
            {
                if (_codes.ContainsKey(value))
                {
                    _opcode = value;
                }
                else
                {
                    throw new InvalidOpcode(String.Format("{0}", _opcode.ToString()));
                }
            }
        }
        public static List<string> AllOpcodes
        {
            get
            {
                if (_allOpcodesName == null)
                {
                    _allOpcodesName = new List<string>();
                    foreach (var pair in _codes.Values)
                    {
                        _allOpcodesName.Add(pair.Key);
                    }
                }
                return _allOpcodesName;
            }
        }
    }
}
