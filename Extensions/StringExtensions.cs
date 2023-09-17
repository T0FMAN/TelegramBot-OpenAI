namespace TelegramBot_OpenAI.Extensions
{
    public static class StringExtensions
    {
        public static string ToRightNameOfTable(this string tableName)
        {
            var chars = tableName.ToCharArray();
            var rightName = tableName.UpperFirstChar()!;

            if (chars[^1] != 's')
                rightName += 's';

            return rightName;
        }

        public static string? UpperFirstChar(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            return char.ToUpper(input[0]) + input[1..];
        }
    }
}
