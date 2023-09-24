namespace TelegramBot_OpenAI.Extensions
{
    public static class EnvVariablesExtensions
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
            finally
            {
                Environment.Exit(1);
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
