// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;

namespace Moryx.WpfToolkit
{
    /// <summary>
    /// Icon only styled button
    /// </summary>
    public class EddieActionButton : EddieButtonBase
    {
        static EddieActionButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieActionButton), new FrameworkPropertyMetadata(typeof(EddieActionButton)));
        }
    }
}
