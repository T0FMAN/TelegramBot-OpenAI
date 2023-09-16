namespace TelegramBot_OpenAI.Extensions
{
    public static class FileExtensions
    {
        public static async Task<string> ReadDataFile(string path)
        {
            string data = string.Empty;

            try
            {
                using StreamReader reader = new(path);
                data = await reader.ReadToEndAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return data;
        }
    }
}
