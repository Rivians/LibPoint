using LibPoint.Domain.Entities;

namespace LibPoint.Domain.Models.Author;

public class AuthorModel
{
    public Guid Id { get; set; }               // Yazarın benzersiz kimliği
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Bio { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? DateOfDeath { get; set; }

    public ICollection<Book>Books { get; set; }
}

