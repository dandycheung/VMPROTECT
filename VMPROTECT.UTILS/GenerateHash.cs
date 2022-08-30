using System.Security.Cryptography;
using System.Text;

namespace VMPROTECT.UTILS
{
    public static class GenerateHash
    {
        public static string ComputeSha256Hash(byte[] bytesData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(bytesData);
                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
