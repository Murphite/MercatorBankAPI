namespace MercatorBankAPIProj.Utilities
{
    public static class CardNumberGenerator
    {
        private static Random random = new Random();

        public static string GenerateCardNumber()
        {
            int randomNumber = random.Next(1000000000, 1999999999);
            return " " + randomNumber.ToString();
        }
    }
}
