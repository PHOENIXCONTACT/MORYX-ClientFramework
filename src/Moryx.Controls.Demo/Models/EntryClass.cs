// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Moryx.Configuration;

namespace Moryx.Controls.Demo.Models
{
    public class EntrySubClass
    {
        public byte AByte { get; set; }
    }

    public class EntryClass
    {
        [Description("Represents a string"), DefaultValue("Some default")]
        public string ChainOfChars { get; set; }

        [Description("This is a very description to reach the max value for the description part to test the ui behavior")]
        public EntrySubClass SubClass { get; set; }

        public List<EntrySubClass> ListSubClass { get; set; }

        public List<EntrySubClass> ListReadOnly { get; } = new List<EntrySubClass>
        {
            new EntrySubClass
            {
                AByte = 1
            }
        };

        public List<Visibility> ListEnum { get; set; }

        public List<string> ListString { get; set; }

        public List<bool> ListBool { get; set; }

        public List<int> ListInt { get; set; }

        public List<long> ListLong { get; set; }

        public List<short> ListShort { get; set; }

        public byte[] ArrayOfByte { get; set; }

        [FileSystemPath(FileSystemPathType.File)]
        public string File { get; set; }

        [FileSystemPath(FileSystemPathType.Directory)]
        public string Directory { get; set; }

        [Password]
        public string Password { get; set; }

        public MemoryStream Stream { get; set; }

        public string ExceptionEntry => throw new InvalidOperationException("This is ver long Exception text to test if the exception editor is readable. Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim.");
    }
}
