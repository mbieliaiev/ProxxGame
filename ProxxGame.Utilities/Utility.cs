using Microsoft.Extensions.Logging;

namespace ProxxGame.Utilities
{
    public static class Utility
    {
        public static int PerformUserIntInput(string inputMessage, ILogger logger)
        {
            Console.WriteLine(inputMessage);
            var inputResultString = Console.ReadLine();
            try
            {
                var inputResult = int.Parse(inputResultString);
                return inputResult;
            }
            catch (Exception)
            {
                logger.LogError("Failed to parse user's input");
                throw;
            }
        }
    }
}
