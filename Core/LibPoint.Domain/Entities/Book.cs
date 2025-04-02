using LibPoint.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public string ISBN { get; set; } // kitap numarası
        public bool IsAvailable { get; set; } = true;
        public string? Publisher { get; set; }
        public int? PublishedYear { get; set; }


        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Borrowing> Borrowings { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
