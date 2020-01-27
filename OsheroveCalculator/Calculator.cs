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
            var delimiters = ExtractDelimiters(numbersString);
            var normalizedString = NormalizeString(numbersString, delimiters);
            var numbers = ExtractNumbers(normalizedString);
            return numbers.Sum();
        }

        private static string[] ExtractDelimiters(string numbersString)
        {
            if (!numbersString.StartsWith("//"))
            {
                return new[] { DefaultDelimiter };
            }

            var firstLine = numbersString.Split("\n")[0];

            var matches = Regex.Matches(firstLine, "\\[(.+?)\\]");
            var delimiters = (from match in matches select match.Groups[1].Value).ToArray();
            if (delimiters.Any())
            {
                return delimiters;
            }

            // We don't have any square bracket syntax, so all
            // of the string apart from the leading // is our
            // delimiter:
            return new[] { firstLine.Substring(2) };
        }

        private static string NormalizeString(string numbersString, IEnumerable<string> delimiters)
        {
            var allDelimiters = delimiters.Concat(new[] { "\n" });
            foreach (var delimiter in allDelimiters)
            {
                numbersString = numbersString.Replace(delimiter, DefaultDelimiter);
            }

            return numbersString;
        }

        private static IList<int> ExtractNumbers(string normalizedString)
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

            return validNumbers;
        }
    }
}
