using System;
using Marvin.Modules;

namespace Marvin.ClientFramework
{
    public class PendingWorkspaceNotification : IModuleNotification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PendingWorkspaceNotification"/> class.
        /// </summary>
        public PendingWorkspaceNotification()
        {
            Timestamp = DateTime.Now;
        }

        /// <summary>
        /// Confirm acknowledgement of this notification
        /// </summary>
        /// <returns>
        /// True of message could be confirmed
        /// </returns>
        public bool Confirm()
        {
            return false;
        }

        /// <summary>
        /// Type of this notification
        /// </summary>
        public NotificationType Type => NotificationType.Info;

        /// <summary>
        /// Time stamp of occurence
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// Notification message
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Optional exception as cause of this message
        /// </summary>
        public Exception Exception { get; private set; }
    }
}