// Copyright (c) 2021, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.IdentityModel.Services;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;

namespace Moryx.ClientFramework.Principals
{
    /// <summary>
    /// Base class for permission based value determination
    /// </summary>
    public abstract class PermissionExtension : MarkupExtension
    {
        #region Fields and Properties

        private object _targetObject;

        private object _targetProperty;

        /// <summary>
        /// The requested action which will be validated by the current permissions
        /// </summary>
        [ConstructorArgument("action")]
        public string Action { get; set; }

        #endregion

        /// <summary>
        /// Constructor to prepare the extension to get information about changed principals
        /// </summary>
        protected PermissionExtension()
        {
            ClaimsPrincipalExtension.ClaimsPrincipalChanged += OnClaimsPrincipalChanged;      
        }

        private void OnClaimsPrincipalChanged(object sender, EventArgs args)
        {
            if (!(_targetObject is DependencyObject targetObject))
                return;

            // Current determined value to update
            var value = ProvidePermissionBasedValue(HasPermission());
            if (_targetProperty is DependencyProperty targetProperty)
            {
                // Update directly if can be accessed otherwise invoke the dispatcher
                if (targetObject.CheckAccess())
                    targetObject.SetValue(targetProperty, value);
                else
                    targetObject.Dispatcher.Invoke(() => targetObject.SetValue(targetProperty, value));
            }
            else
            {
                var propertyInfo = _targetProperty as PropertyInfo;
                propertyInfo?.SetValue(targetObject, value, null);
            }
        }

        /// <inheritdoc />
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget target)
            {
                _targetObject = target.TargetObject;
                _targetProperty = target.TargetProperty;
            }

            return ProvidePermissionBasedValue(HasPermission());
        }

       private bool HasPermission()
        {
            try
            {
                // Has permission if no exception will be thrown
                ClaimsPrincipalPermission.CheckAccess("permission", Action);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Get the permission based value
        /// </summary>
        protected abstract object ProvidePermissionBasedValue(bool hasPermission);
    }
}