namespace FirstMvcApp.Data.Entities
{
    public class Article : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string SourceUrl { get; set; }
        public DateTime CreationDate { get; set; }
        public float PositivityRate { get; set; }

        public Guid SourceId { get; set; }
        public Source Source { get; set; }
        
        public virtual ICollection<Comment> Comments { get; set; }
    }
}