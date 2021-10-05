// Copyright (c) 2021, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Markup;
using System.Xaml;
using Moryx.Identity;

namespace Moryx.ClientFramework.Principals
{
    /// <summary>
    /// Base class for permission based value determination
    /// </summary>
    public abstract class PermissionExtensionBase : MarkupExtension
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
        protected PermissionExtensionBase()
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
            if (string.IsNullOrEmpty(Resource) && serviceProvider.GetService(typeof(IRootObjectProvider)) is IRootObjectProvider root)
            {
                // Try to read from root attached property
                var rootElement = root.RootObject as DependencyObject;
                var defaultResource = rootElement?.GetValue(PermissionProvider.DefaultResourceProperty);
                if (defaultResource != null)
                {
                    Resource = (string) defaultResource;
                }

                if (string.IsNullOrEmpty(Resource))
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
            if (IdentityConfiguration.CurrentContext != null)
                return IdentityConfiguration.CurrentContext.CheckAccess(Resource, Action);

            return true;
        }

        /// <summary>
        /// Get the permission based value
        /// </summary>
        protected abstract object ProvidePermissionBasedValue(bool hasPermission);
    }
}