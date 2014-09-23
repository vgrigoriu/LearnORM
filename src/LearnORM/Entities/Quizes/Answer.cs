namespace Entities.Quizes
{
    public class Answer : BaseEntity
    {
        public virtual string Text { get; set; }
        public virtual bool IsCorrect { get; set; }

        public override string ToString()
        {
            return string.Format("[{0}] {1}", (IsCorrect ? "x" : " "), Text);
        }
    }
}
