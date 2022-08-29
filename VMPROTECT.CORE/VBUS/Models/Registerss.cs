namespace VMPROTECT.CORE.VBUS.Models
{
    public class Registerss
    {
        /* General Purpose Registers */
        public uint[]? Regs { get; set; }
        /* Zero Flag 
            value 1 - flag is set if the result of the last comparison was zero
            value 0 - flag is not set
        */
        public byte ZeroFlag { get; set; } = 0;
        /* Carry Flag 
            value 1 - flag is set the results of the last comparison was moving
            value 0 - flag is not set
        */
        public byte CarryFlag { get; set; } = 0;
        public uint? PC { get; set; }
        public uint? SP { get; set; }
    }
}