using System;
using Marvin.Modules;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Notification for a workspace which is waiting to be pushed to front
    /// </summary>
    public class PendingWorkspaceNotification : IModuleNotification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PendingWorkspaceNotification"/> class.
        /// </summary>
        public PendingWorkspaceNotification()
        {
            Timestamp = DateTime.Now;
        }

        /// <inheritdoc />
        /// <summary>
        /// Confirm acknowledgement of this notification
        /// </summary>
        /// <returns>
        /// True of message could be confirmed
        /// </returns>
        public bool Confirm() => false;

        /// <inheritdoc />
        /// <summary>
        /// Type of this notification
        /// </summary>
        public NotificationType Type => NotificationType.Info;

        /// <inheritdoc />
        /// <summary>
        /// Time stamp of occurence
        /// </summary>
        public DateTime Timestamp { get; }

        /// <inheritdoc />
        /// <summary>
        /// Notification message
        /// </summary>
        public string Message { get; protected set; }

        /// <inheritdoc />
        /// <summary>
        /// Optional exception as cause of this message
        /// </summary>
        public Exception Exception { get; protected set; }
    }
}