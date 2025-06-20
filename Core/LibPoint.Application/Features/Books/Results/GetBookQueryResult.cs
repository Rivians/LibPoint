using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Books.Results
{
    public class GetBookQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ISBN { get; set; } // kitap numarası
        public bool IsAvailable { get; set; } = true;
        public string? Publisher { get; set; }
        public int? PublishedYear { get; set; }
        // update ---
        public string Description { get; set; }
        public string Genre { get; set; }
        // ---
        public string ImageUrl { get; set; }
    }
}
