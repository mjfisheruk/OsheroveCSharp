namespace OsheroveCalculator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

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

            var matches = Regex.Matches(firstLine, "\\[(.+?)\\]");
            foreach (Match match in matches)
            {
                var groups = match.Groups;
                return groups[1].Value;
            }

            // We don't have any square bracket syntax, so all
            // of the string apart from the leading // is our
            // delimiter:
            return firstLine.Substring(2);
        }

        private static string NormalizeString(string numbersString, string delimiter)
        {
            var noNewLines = numbersString.Replace("\n", DefaultDelimiter);
            return noNewLines.Replace(delimiter, DefaultDelimiter);
        }
    }
}