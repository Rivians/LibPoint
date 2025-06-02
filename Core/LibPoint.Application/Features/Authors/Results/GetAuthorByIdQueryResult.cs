namespace LibPoint.Application.Features.Authors.Results;

public class GetAuthorByIdQueryResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Bio { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? DateOfDeath { get; set; }
}