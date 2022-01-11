﻿using FirstMvcApp.Data.Entities;

namespace FirstMvcApp.Data
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }

        public Guid SourceId { get; set; }
        public Source Source { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}