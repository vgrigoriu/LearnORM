using System.Collections.Generic;

namespace Entities.Quizes
{
    class Question
    {
        public virtual string Text { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
