using LibPoint.Domain.Models.Responses;
using MediatR;

namespace LibPoint.Application.Features.Authors.Commands;

public class CreateAuthorCommandRequest:IRequest<ResponseModel<Guid>>
{
public string Name { get; set; }
public string Surname { get; set; }
public string? Bio { get; set; }
public DateTime? DateOfBirth { get; set; }
public DateTime? DateOfDeath { get; set; }
}