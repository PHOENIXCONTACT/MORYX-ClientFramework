using Caliburn.Micro;

namespace Marvin.Controls.Demo.ViewModels
{
    public class NavigationBarViewModel : Screen
    {
        private bool _isNavigationBarLocked;

        public override string DisplayName => "NavigationBar";

        public bool IsNavigationBarLocked
        {
            get { return _isNavigationBarLocked; }
            set
            {
                if (_isNavigationBarLocked != value)
                {
                    _isNavigationBarLocked = value;
                    NotifyOfPropertyChange();
                }
            }
        }
    }
}
