namespace OsheroveCalculator
{
    public static class Calculator
    {
        private const string DefaultDelimiter = ",";

        public static int Add(string numbersString)
        {
            var delimiter = ExtractDelimiter(numbersString);
            var normalizedString = NormalizeString(numbersString, delimiter);
            var parts = normalizedString.Split(DefaultDelimiter);
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

        private static string ExtractDelimiter(string numbersString)
        {
            if (!numbersString.StartsWith("//"))
            {
                return DefaultDelimiter;
            }

            var firstLine = numbersString.Split("\n")[0];
            var delimiter = firstLine.Substring(2);
            return delimiter;
        }

        private static string NormalizeString(string numbersString, string delimiter)
        {
            var noNewLines = numbersString.Replace("\n", DefaultDelimiter);
            return noNewLines.Replace(delimiter, DefaultDelimiter);
        }
    }
}