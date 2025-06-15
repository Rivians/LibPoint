using LibPoint.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Domain.Models.Books
{
    public class BookModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ISBN { get; set; } // kitap numarası
        public bool IsAvailable { get; set; } = true;
        public string? Publisher { get; set; }
        public int? PublishedYear { get; set; }
        public string AuthorName { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<Category> Categories { get; set; }

    }
}
