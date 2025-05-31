using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Books.Commands
{
    public class RemoveBookCommand
    {
        public Guid Id { get; set; }

        public RemoveBookCommand(Guid id)
        {
            Id = id;
        }
    }
}
