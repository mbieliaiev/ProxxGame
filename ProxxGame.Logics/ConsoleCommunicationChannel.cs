using Microsoft.Extensions.Logging;
using ProxxGame.Contract;
using ProxxGame.Utilities;

namespace ProxxGame.Logics
{
    public class ConsoleCommunicationChannel : ICommunicationChannel
    {
        private readonly ILogger _logger;

        public ConsoleCommunicationChannel(ILogger<ConsoleCommunicationChannel> logger) {
            _logger = logger;
        }

        public int ReadIntWithMessage(string message)
        {
            return Utility.PerformUserIntInput(message, _logger);
        }

        public string ReadWithMessage(string message)
        {
            Console.WriteLine(message);

            return Console.ReadLine();
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
