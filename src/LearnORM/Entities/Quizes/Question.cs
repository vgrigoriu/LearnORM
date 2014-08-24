using System.Collections.Generic;

namespace Entities.Quizes
{
    public class Question : BaseEntity
    {
        public virtual string Text { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
