namespace Entities
{
    public class Book : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual string TitleForSorting { get; set; }
        public virtual string OriginalTitle { get; set; }

        public virtual Publisher Publisher { get; set; }
    }
}