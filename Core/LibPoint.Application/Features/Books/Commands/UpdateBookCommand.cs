using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Books.Commands
{
    public class UpdateBookCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ISBN { get; set; } // kitap numarası
        public bool IsAvailable { get; set; } = true;
        public string? Publisher { get; set; }
        public int? PublishedYear { get; set; }
    }
}
