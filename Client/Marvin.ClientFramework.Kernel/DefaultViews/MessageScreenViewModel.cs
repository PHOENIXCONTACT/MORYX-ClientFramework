using Caliburn.Micro;

namespace Marvin.ClientFramework.Kernel
{
    public class MessageScreenViewModel : Screen
    {
        public MessageScreenViewModel(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Message to show
        /// </summary>
        public string Message { get; }
    }
}
