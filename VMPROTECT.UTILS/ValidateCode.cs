using VMPROTECT.CORE.Security.Exceptions;
using VMPROTECT.CORE.Security.Models;

namespace VMPROTECT.UTILS
{
    public static class ValidateCode
    {
        public static void ValidateMagicNumber(byte[] code)
        {
            var magicNumber = (uint)((code[0] << 8) | code[1]);
            if (magicNumber != SecDetails.MagicNumber)
            {
                throw new SecurityBreach("Invalid magic number");
            }
        }

        public static void ValidateDataStructure(byte[] code)
        {
            var generatedHash256 = GenerateHash.ComputeSha256Hash(code);
            if(!SecFile.CodeHash.Equals(generatedHash256))
            {
                throw new SecurityBreach("Invalid hash");
            }
        }
    }
}