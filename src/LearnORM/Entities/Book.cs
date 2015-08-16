using System;

namespace Entities
{
    public class Book : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual string TitleForSorting { get; set; }
        public virtual string OriginalTitle { get; set; }

        public virtual Publisher Publisher { get; set; }
    }

    public class SensorReading : BaseEntity
    {
        public virtual string SensorName { get; set; }
        public virtual double Value { get; set; }
        public virtual DateTimeOffset ReadingDate { get; set; }
    }
}