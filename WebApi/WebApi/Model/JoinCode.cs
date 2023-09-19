namespace WebApi.Model
{
    public class JoinCode
    {
        private readonly int Length;

        public JoinCode(int length)
        {
            Length = length;
        }

        public string CreateNew()
        {
            var random = new Random();
            var code = string.Empty;

            for (int i = 0; i < Length; i++)
                code += random.Next(0, 10);

            return code;
        }
    }
}