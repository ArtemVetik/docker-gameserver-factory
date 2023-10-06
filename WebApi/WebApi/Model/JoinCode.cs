using System.Security.Cryptography;
using System.Text;

namespace WebApi.Model
{
    public class JoinCode
    {
        private static readonly char[] Alphanum = "ABCDEFGH1234567890".ToCharArray();

        private readonly int Length;
        
        public JoinCode(int length)
        {
            Length = length;
        }

        public string CreateNew()
        {
            byte[] data = new byte[4 * Length];

            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }

            StringBuilder result = new StringBuilder(Length);
            
            for (int i = 0; i < Length; i++)
            {
                var number = BitConverter.ToUInt32(data, i * 4);
                var index = number % Alphanum.Length;

                result.Append(Alphanum[index]);
            }

            return result.ToString();
        }
    }
}