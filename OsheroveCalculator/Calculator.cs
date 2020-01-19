namespace OsheroveCalculator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Calculator
    {
        private const string DefaultDelimiter = ",";

        public static int Add(string numbersString)
        {
            var delimiter = ExtractDelimiter(numbersString);
            var normalizedString = NormalizeString(numbersString, delimiter);
            var numbers = ExtractNumbers(normalizedString);
            return numbers.Sum();
        }

        private static int[] ExtractNumbers(string normalizedString)
        {
            var parts = normalizedString.Split(DefaultDelimiter);
            var invalidNumbers = new List<int>();
            var validNumbers = new List<int>();
            foreach (var part in parts)
            {
                if (!int.TryParse(part, out var parsedInt))
                {
                    continue;
                }

                if (parsedInt > 1000)
                {
                    continue;
                }

                if (parsedInt < 0)
                {
                    invalidNumbers.Add(parsedInt);
                }
                else
                {
                    validNumbers.Add(parsedInt);
                }
            }

            if (invalidNumbers.Count != 0)
            {
                var message = "Negative numbers present:" + string.Join(", ", invalidNumbers);
                throw new ArgumentOutOfRangeException(nameof(normalizedString), message);
            }

            return validNumbers.ToArray();
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