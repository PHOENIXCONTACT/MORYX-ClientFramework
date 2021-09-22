// Copyright (c) 2021, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.IdentityModel.Services;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xaml;

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
        /// Resource within the action requires permissions
        /// </summary>
        public string Resource { get; set; }

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
            ClaimsPrincipalSync.PrincipalChanged += OnPrincipalChanged;      
        }

        private void OnPrincipalChanged(object sender, EventArgs args)
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
            // If resource was not specified, tried to determine from host control
            if (Resource == null && serviceProvider.GetService(typeof(IRootObjectProvider)) is IRootObjectProvider root)
            {
                // Try to read from user control permissions
                if (root.RootObject is UserControl control && control.Resources[UserControlPermissions.Key] is UserControlPermissions controlPermissions)
                {
                    Resource = controlPermissions.Resource;
                }
                else
                {
                    var regex = new Regex(@"^\w+\.\w+");
                    Resource = regex.Match(root.RootObject?.GetType().Namespace ?? "Moryx").Value;
                }
            }

            if (serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget target)
            {
                _targetObject = target.TargetObject;
                _targetProperty = target.TargetProperty;
            }

            var hasPermission = HasPermission();
            return ProvidePermissionBasedValue(hasPermission);
        }

       private bool HasPermission()
        {
            try
            {
                // Has permission if no exception will be thrown
                ClaimsPrincipalPermission.CheckAccess(Resource, Action);
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