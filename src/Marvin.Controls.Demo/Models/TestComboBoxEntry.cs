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
