namespace MercatorBankAPIProj.Utilities
{
    public static class TerminalIdGenerator
    {
        private static Random random = new Random();

        public static string GenerateTerminalId()
        {
            int randomNumber = random.Next(100000000, 999999999);
            return "#" + randomNumber.ToString();
        }
    }
}
