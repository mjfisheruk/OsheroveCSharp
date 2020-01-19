namespace OsheroveCalculator
{
    public static class Calculator
    {
        public static int Add(string numbersString)
        {
            var parts = numbersString.Split(",");
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
    }
}