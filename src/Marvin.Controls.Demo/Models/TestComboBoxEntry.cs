// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

namespace Marvin.Controls.Demo.Models
{
    public class TestComboBoxEntry
    {
        public string Content { get; set; }

        public TestComboBoxEntry(string content)
        {
            Content = content;
        }

        public override string ToString()
        {
            return Content;
        }
    }
}
