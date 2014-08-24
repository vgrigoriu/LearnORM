namespace Entities.Quizes
{
    public class Answer : BaseEntity
    {
        public virtual string Text { get; set; }
        public virtual bool IsCorrect { get; set; }
    }
}
