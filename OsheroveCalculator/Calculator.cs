namespace OsheroveCalculator
{
    public static class Calculator
    {
        public static int Add(string numbersString)
        {
            var normalizedString = NormalizeString(numbersString);
            var parts = normalizedString.Split(",");
            var result = 0;
            foreach (var part in parts)
            {
                if (int.TryParse(part, out var parsedInt))
                {
                    result += parsedInt;
                }
            }

            return result;
        }

        private static string NormalizeString(string numbersString)
        {
            return numbersString.Replace("\n", ",");
        }
    }
}