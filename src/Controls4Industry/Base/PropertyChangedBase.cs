using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace C4I
{
    /// <summary>
    /// Basic implementation of <see cref="INotifyPropertyChanged" /> interface
    /// </summary>
    public class PropertyChangedBase : INotifyPropertyChanged
    {
        /// <summary>
        /// PropertyChanged handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Helper function called by properties
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
