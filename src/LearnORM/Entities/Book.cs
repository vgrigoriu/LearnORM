namespace Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string TitleForSorting { get; set; }
        public string OriginalTitle { get; set; }
    }
}