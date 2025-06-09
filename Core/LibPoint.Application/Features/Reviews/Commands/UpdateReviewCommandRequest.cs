using LibPoint.Domain.Models.Responses;
using MediatR;

namespace LibPoint.Application.Features.Reviews.Commands;

public class UpdateReviewCommandRequest:IRequest<ResponseModel<Guid>>
{
    public Guid Id { get; set; }              // Güncellenecek review ID
    public int Rating { get; set; }           // Yeni puan
    public string Comment { get; set; }       // Yeni yorum

    public Guid AppuUserId { get; set; }       // Kullanıcı ID (isteğe bağlı kontrol için)
    public Guid BookId { get; set; }          // Kitap ID (gerekirse)
}