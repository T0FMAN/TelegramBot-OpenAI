namespace TelegramBot_OpenAI.Helpers
{
    /// <summary>
    /// Represents operations with environment variables
    /// </summary>
    public static class EnvVariablesHelper
    {
        private static void SetEnvVariable(string variable, string value)
        {
            try
            {
                Environment.SetEnvironmentVariable(variable, value);
            }
            catch (ArgumentNullException)
            {
                throw new Exception("Value of setted env variable is null");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void SetEnvOpenAiToken(string token)
        {
            SetEnvVariable("OpenAiToken", token);
        }

        public static string GetEnvOpenAiToken()
        {
            return Environment.GetEnvironmentVariable("OpenAiToken")!;
        }
    }
}
