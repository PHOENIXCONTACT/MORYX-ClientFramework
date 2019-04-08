namespace Marvin.Controls.Demo.Models
{
    public class TestListViewEntry
    {
        public long Id { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string Text3 { get; set; }
        public string Text4 { get; set; }
        public string Text5 { get; set; }

        public TestListViewEntry(long id, string text1, string text2, string text3, string text4, string text5)
        {
            Id = id;
            Text1 = text1;
            Text2 = text2;
            Text3 = text3;
            Text4 = text4;
            Text5 = text5;
        }
    }
}
