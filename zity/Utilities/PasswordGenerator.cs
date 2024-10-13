namespace zity.Utilities
{
    public static class PasswordGenerator
    {
        private static readonly Random random = new();

        public static string GeneratePassword(int length = 12)
        {
            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; 
            const string lowerChars = "abcdefghijklmnopqrstuvwxyz"; 
            const string digits = "0123456789";
            const string specialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";

            string allChars = upperChars + lowerChars + digits + specialChars;

            char[] passwordChars = new char[length];

            passwordChars[0] = upperChars[random.Next(upperChars.Length)];
            passwordChars[1] = lowerChars[random.Next(lowerChars.Length)];
            passwordChars[2] = digits[random.Next(digits.Length)];
            passwordChars[3] = specialChars[random.Next(specialChars.Length)];

            for (int i = 4; i < length; i++)
            {
                passwordChars[i] = allChars[random.Next(allChars.Length)];
            }

            return new string(passwordChars.OrderBy(c => random.Next()).ToArray());
        }
    }
}
