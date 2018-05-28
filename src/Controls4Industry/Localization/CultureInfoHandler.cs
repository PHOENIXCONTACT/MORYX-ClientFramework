using System;
using System.Globalization;

namespace C4I
{
    /// <summary>
    /// Handler that changes current culture at runtime and fires an event on culture change
    /// </summary>
    public class CultureInfoHandler
    {
        private static CultureInfoHandler _cultureInfoHandler;

        private CultureInfoHandler()
        { }

        /// <summary>
        /// Changes the current culture
        /// </summary>
        /// <param name="cultureInfo"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void ChangeCulture(CultureInfo cultureInfo)
        {
            if(cultureInfo == null)
                throw new ArgumentNullException(nameof(cultureInfo));

            CultureInfo.CurrentUICulture = cultureInfo;
            CultureInfo.CurrentCulture = cultureInfo;

            RaiseCultureChangedEvent();
        }

        /// <summary>
        /// Singleton
        /// </summary>
        public static CultureInfoHandler Instance => _cultureInfoHandler ?? (_cultureInfoHandler = new CultureInfoHandler());

        /// <summary>
        /// Event gets called when culture has been changed
        /// </summary>
        public event EventHandler CultureChanged;

        private void RaiseCultureChangedEvent()
        {
            CultureChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
