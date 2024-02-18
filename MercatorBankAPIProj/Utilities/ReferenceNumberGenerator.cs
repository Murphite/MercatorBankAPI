namespace MercatorBankAPIProj.Utilities
{
    public static class ReferenceNumberGenerator
    {
        private static Random random = new Random();

        public static string GenerateReferenceNumber()
        {
            int randomNumber = random.Next(100000, 999999);
            return "Ref" + randomNumber.ToString();
        }
    }
}
